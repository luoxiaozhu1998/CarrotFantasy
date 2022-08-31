using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    public class MapMaker : MonoBehaviour
    {
        public bool drawLine;

        private float m_MapWidth;
        private float m_MapHeight;

        public int bigLevelID;
        public int levelID;

        /// <summary>
        /// 全部格子对象
        /// </summary>
        private GridPoint[,] m_GridPoints;

        public float gridWidth;
        public float gridHeight;

        private const int YRow = 8;
        private const int XRow = 12;

        public List<GridPoint.GridIndex> monsterPaths;

        /// <summary>
        /// 怪物路径点具体位置
        /// </summary>
        [HideInInspector]
        public List<Vector3> monsterPathPos;

        private SpriteRenderer m_BgSr;
        private SpriteRenderer m_RoadSr;

        /// <summary>
        /// 每一波次产生的怪物信息列表
        /// </summary>
        public List<Round.RoundInfo> roundInfos;


        public GameObject gridGo;

        public static MapMaker instance { get; private set; }

        private void Awake()
        {
            instance = this;
            InitMapMaker();
        }

        //初始化地图
        private void InitMapMaker()
        {
            CalculateSize();
            m_GridPoints = new GridPoint[XRow, YRow];

            monsterPaths = new List<GridPoint.GridIndex>();
            for (var x = 0; x < XRow; x++)
            {
                for (var y = 0; y < YRow; y++)
                {
                    var transform1 = transform;
                    var itemGo = Instantiate(gridGo, transform1.position, transform1.rotation);
                    itemGo.transform.position = CorrectPosition(x * gridWidth, y * gridHeight);
                    itemGo.transform.SetParent(transform);
                    m_GridPoints[x, y] = itemGo.GetComponent<GridPoint>();
                    m_GridPoints[x, y].mGridIndex.xIndex = x;
                    m_GridPoints[x, y].mGridIndex.yIndex = y;
                }
            }

            m_BgSr = transform.Find("BG").GetComponent<SpriteRenderer>();
            m_RoadSr = transform.Find("Road").GetComponent<SpriteRenderer>();
        }

        //纠正位置
        private Vector3 CorrectPosition(float x, float y)
        {
            return new Vector3(x - m_MapWidth / 2 + gridWidth / 2,
                y - m_MapHeight / 2 + gridHeight / 2);
        }

        private void CalculateSize()
        {
            var leftDown = new Vector3(0, 0);
            var rightUp = new Vector3(1, 1);

            if (Camera.main == null) return;
            var posOne = Camera.main.ViewportToWorldPoint(leftDown);
            var posTow = Camera.main.ViewportToWorldPoint(rightUp);
            m_MapWidth = posTow.x - posOne.x;
            m_MapHeight = posTow.y - posOne.y;
            gridWidth = m_MapWidth / XRow;
            gridHeight = m_MapHeight / YRow;
        }

        /// <summary>
        /// 画格子,每次渲染状态改变调用
        /// </summary>
        private void OnDrawGizmos()
        {
            if (!drawLine) return;
            CalculateSize();
            Gizmos.color = Color.green;

            //画行
            for (var y = 0; y <= YRow; y++)
            {
                var startPos = new Vector3(-m_MapWidth / 2, -m_MapHeight / 2 + gridHeight * y);
                var endPos = new Vector3(m_MapWidth / 2, -m_MapHeight / 2 + gridHeight * y);
                Gizmos.DrawLine(startPos, endPos);
            }

            //画列
            for (var x = 0; x <= XRow; x++)
            {
                var startPos = new Vector3(-m_MapWidth / 2 + gridWidth * x, -m_MapHeight / 2);
                var endPos = new Vector3(-m_MapWidth / 2 + gridWidth * x, m_MapHeight / 2);
                Gizmos.DrawLine(startPos, endPos);
            }
        }

        public void ClearMonsterPath()
        {
            monsterPaths.Clear();
        }

        public void RecoverTowerPoint()
        {
            ClearMonsterPath();
            for (var x = 0; x < XRow; x++)
            {
                for (var y = 0; y < YRow; y++)
                {
                    m_GridPoints[x, y].InitGrid();
                }
            }
        }

        public void InitMap()
        {
            bigLevelID = levelID = 0;
            RecoverTowerPoint();
            roundInfos.Clear();
            m_BgSr.sprite = m_RoadSr.sprite = null;
        }

        private LevelInfo CreatLevelInfoGo()
        {
            var levelInfo = new LevelInfo
            {
                BigLevelID = bigLevelID,
                LevelID = levelID,
                GridStates = new GridPoint.GridState[XRow, YRow],
                MonsterPaths = monsterPaths,
                RoundInfos = new List<Round.RoundInfo>(roundInfos)
            };
            for (var x = 0; x < XRow; x++)
            {
                for (var y = 0; y < YRow; y++)
                {
                    levelInfo.GridStates[x, y] = m_GridPoints[x, y].MGridState;
                }
            }

            Debug.Log("Save Success!");
            return levelInfo;
        }

        //保存为Json
        public void SaveLevelFileByJson()
        {
            var levelInfoGo = CreatLevelInfoGo();
            var filePath = Application.dataPath + "/Resources/Json/Level/" + "Level_" + bigLevelID +
                           "_" + levelID + ".json";
            var saveJsonStr = JsonConvert.SerializeObject(levelInfoGo);
            var sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
        }

        //读取Json
        public static LevelInfo LoadLevelInfoFile(string fileName)
        {
            var levelInfo = new LevelInfo();
            var filePath = Application.dataPath + "/Resources/Json/Level/" + fileName;
            if (File.Exists(filePath))
            {
                var sr = new StreamReader(filePath);
                var jsonStr = sr.ReadToEnd();
                sr.Close();
                levelInfo = JsonConvert.DeserializeObject<LevelInfo>(jsonStr);
            }
            else
            {
                Debug.Log("File Load Failed" + filePath);
            }

            return levelInfo;
        }

        public void LoadLevelFile(LevelInfo levelInfo)
        {
            bigLevelID = levelInfo.BigLevelID;
            levelID = levelInfo.LevelID;
            for (var x = 0; x < XRow; x++)
            {
                for (var y = 0; y < YRow; y++)
                {
                    m_GridPoints[x, y].MGridState = levelInfo.GridStates[x, y];
                    m_GridPoints[x, y].UpdateGrid();
                }
            }

            monsterPaths = levelInfo.MonsterPaths;
            roundInfos = new List<Round.RoundInfo>(levelInfo.RoundInfos);
            m_BgSr.sprite = Resources.Load<Sprite>("Pictures/NormalModel/Game/" +
                                                   bigLevelID + "/" + "BG" +
                                                   levelID / 3);

            m_RoadSr.sprite = Resources.Load<Sprite>("Pictures/NormalModel/Game/" +
                                                     bigLevelID + "/" + "Road" +
                                                     levelID);
        }
    }
}