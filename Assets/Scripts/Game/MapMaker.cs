using System;
using UnityEngine;

namespace Game
{
    public class MapMaker : MonoBehaviour
    {
        public bool drawLine;

        private float m_MapWidth;
        private float m_MapHeight;

        private float m_GridWidth;
        private float m_GridHeight;

        private const int YRow = 8;
        private const int XRow = 12;

        public GameObject gridGo;

        public static MapMaker instance { get; private set; }

        private void Awake()
        {
            instance = this;
            InitMap();
        }
        
        //初始化地图
        private void InitMap()
        {
            CalculateSize();
            for (var x = 0; x < XRow; x++)
            {
                for (var y = 0; y < YRow; y++)
                {
                    var transform1 = transform;
                    var itemGo = Instantiate(gridGo, transform1.position, transform1.rotation);
                    itemGo.transform.position = CorrectPosition(x * m_GridWidth, y * m_GridHeight);
                    itemGo.transform.SetParent(transform);
                }
            }
        }
        
        //纠正位置
        private Vector3 CorrectPosition(float x, float y)
        {
            return new Vector3(x - m_MapWidth / 2 + m_GridWidth / 2,
                y - m_MapHeight / 2 + m_GridHeight / 2);
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
            m_GridWidth = m_MapWidth / XRow;
            m_GridHeight = m_MapHeight / YRow;
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
                var startPos = new Vector3(-m_MapWidth / 2, -m_MapHeight / 2 + m_GridHeight*y);
                var endPos = new Vector3(m_MapWidth / 2, -m_MapHeight / 2 + m_GridHeight*y);
                Gizmos.DrawLine(startPos,endPos);
            }
            //画列
            for (var x = 0; x <= XRow; x++)
            {
                var startPos = new Vector3(-m_MapWidth / 2+m_GridWidth*x, -m_MapHeight / 2 );
                var endPos = new Vector3(-m_MapWidth / 2+m_GridWidth*x, m_MapHeight/2);
                Gizmos.DrawLine(startPos,endPos);
            }
        }
    }
}