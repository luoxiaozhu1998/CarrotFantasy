using System;
using UnityEngine;

namespace Game
{
    public class GridPoint : MonoBehaviour
    {
        private SpriteRenderer m_SpriteRenderer;
        public GridState MGridState;
        public GridIndex mGridIndex;

        private Sprite m_GridSprite; //grid sprite

        private Sprite m_MonsterPathSprite; //monster road sprite

        public GameObject[] itemPrefabs;

        public GameObject currentItem;

        //grid state

        public struct GridState
        {
            public bool CanBuild;
            public bool IsMonsterPoint;
            public bool HasItem;
            public int ItemID;
        }
        [Serializable]
        public struct GridIndex
        {
            public int xIndex;
            public int yIndex;
        }


        private void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_GridSprite = Resources.Load<Sprite>("Pictures/NormalModel/Game/Grid");
            m_MonsterPathSprite = Resources.Load<Sprite>("Pictures/NormalModel/Game/1/Monster/6-1");
            itemPrefabs = new GameObject[10];
            var prefabsPath = "Prefabs/Game/" + MapMaker.instance.bigLevelID + "/Items/";
            for (var i = 0; i < itemPrefabs.Length; i++)
            {
                itemPrefabs[i] = Resources.Load<GameObject>(prefabsPath + i);
                if (!itemPrefabs[i])
                {
                    Debug.Log("Load Failed" + prefabsPath + i);
                }
            }

            InitGrid();
        }

        public void InitGrid()
        {
            MGridState.CanBuild = true;
            MGridState.IsMonsterPoint = false;
            MGridState.HasItem = false;
            m_SpriteRenderer.enabled = true;
            MGridState.ItemID = -1;
            m_SpriteRenderer.sprite = m_GridSprite;
            Destroy(currentItem);
        }

        private void OnMouseDown()
        {
            //怪物路点
            if (Input.GetKey(KeyCode.P))
            {
                MGridState.CanBuild = false;
                m_SpriteRenderer.enabled = true;
                MGridState.IsMonsterPoint = !MGridState.IsMonsterPoint;
                if (MGridState.IsMonsterPoint)
                {
                    //加入列表，用于怪物寻路
                    MapMaker.instance.monsterPaths.Add(mGridIndex);
                    m_SpriteRenderer.sprite = m_MonsterPathSprite;
                }
                else
                {
                    MapMaker.instance.monsterPaths.Remove(mGridIndex);
                    m_SpriteRenderer.sprite = m_GridSprite;
                    MGridState.CanBuild = true;
                }
            }
            //道具
            else if (Input.GetKey(KeyCode.I))
            {
                MGridState.ItemID++;
                if (MGridState.ItemID == itemPrefabs.Length)
                {
                    MGridState.ItemID = -1;
                    Destroy(currentItem);
                    MGridState.HasItem = false;
                    return;
                }

                if (currentItem == null)
                {
                    CreateItem();
                }
                else
                {
                    Destroy(currentItem);
                    CreateItem();
                }

                MGridState.HasItem = true;
            }
            else if (!MGridState.IsMonsterPoint)
            {
                MGridState.IsMonsterPoint = false;
                MGridState.CanBuild = !MGridState.CanBuild;
                m_SpriteRenderer.enabled = MGridState.CanBuild;
            }
        }

        private void CreateItem()
        {
            var createPos = transform.position;
            switch (MGridState.ItemID)
            {
                case <= 2:
                    createPos +=
                        new Vector3(MapMaker.instance.gridWidth, -MapMaker.instance.gridHeight) / 2;
                    break;
                case <= 4:
                    createPos +=
                        new Vector3(MapMaker.instance.gridWidth, 0) / 2;
                    break;
            }

            var itemGo = Instantiate(itemPrefabs[MGridState.ItemID], createPos,
                Quaternion.identity);
            currentItem = itemGo;
        }

        public void UpdateGrid()
        {
            if (MGridState.CanBuild)
            {
                m_SpriteRenderer.sprite = m_GridSprite;
                m_SpriteRenderer.enabled = true;
                if (MGridState.HasItem)
                {
                    CreateItem();
                }
            }
            else
            {
                if (MGridState.IsMonsterPoint)
                {
                    m_SpriteRenderer.sprite = m_MonsterPathSprite;
                }
                else
                {
                    m_SpriteRenderer.enabled = false;
                }
            }
        }
    }
}