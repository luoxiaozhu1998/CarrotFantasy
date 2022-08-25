using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.UI
{
    public class SlideScrollView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        private RectTransform m_ContentTrans;
        private float m_BeginMousePositionX;
        private float m_EndMousePositionX;
        private ScrollRect m_ScrollRect;

        public int cellLength;
        public int spacing;
        public int leftOffset;
        private float m_MoveOneItemLength;

        private Vector3 m_CurrentContentLocalPos; //上一次的位置
        private Vector3 m_ContentInitPos; //Content初始位置
        private Vector2 m_ContentTransSize; //Content初始大小

        public int totalItemNum;
        private int m_CurrentIndex;

        public TMP_Text pageText;

        public bool needSendMessage;

        private void Awake()
        {
            m_ScrollRect = GetComponent<ScrollRect>();
            m_ContentTrans = m_ScrollRect.content;
            m_MoveOneItemLength = cellLength + spacing;
            var localPosition = m_ContentTrans.localPosition;
            m_CurrentContentLocalPos = localPosition;
            m_ContentTransSize = m_ContentTrans.sizeDelta;
            m_ContentInitPos = localPosition;
            m_CurrentIndex = 1;
            if (pageText != null)
            {
                pageText.text = m_CurrentIndex + "/" + totalItemNum;
            }
        }

        public void Init()
        {
            m_CurrentIndex = 1;
            if (m_ContentTrans == null) return;
            m_ContentTrans.localPosition = m_ContentInitPos;//重新加载时返回第一页
            m_CurrentContentLocalPos = m_ContentInitPos;
            if (pageText != null)
            {
                pageText.text = m_CurrentIndex + "/" + totalItemNum;
            }
        }

        /// <summary>
        /// 通过拖拽与松开来达成翻页效果
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            m_EndMousePositionX = Input.mousePosition.x;
            float moveDistance; //当次需要滑动的距离
            var offSetX = m_BeginMousePositionX - m_EndMousePositionX;

            if (offSetX > 0) //右滑
            {
                if (m_CurrentIndex >= totalItemNum)
                {
                    return;
                }

                if (needSendMessage)
                {
                    UpdatePanel(true);
                }

                moveDistance = -m_MoveOneItemLength;
                m_CurrentIndex++;
            }
            else //左滑
            {
                if (m_CurrentIndex <= 1)
                {
                    return;
                }

                if (needSendMessage)
                {
                    UpdatePanel(false);
                }

                moveDistance = m_MoveOneItemLength;
                m_CurrentIndex--;
            }

            if (pageText != null)
            {
                pageText.text = m_CurrentIndex + "/" + totalItemNum;
            }

            DOTween.To(() => m_ContentTrans.localPosition,
                lerpValue => m_ContentTrans.localPosition = lerpValue,
                m_CurrentContentLocalPos + new Vector3(moveDistance, 0, 0), 0.5f).SetEase(Ease.OutQuint);
            m_CurrentContentLocalPos += new Vector3(moveDistance, 0, 0);
        }

        /// <summary>
        /// 按钮来控制翻书效果
        /// </summary>
        public void ToNextPage()
        {
            if (m_CurrentIndex >= totalItemNum)
            {
                return;
            }

            var moveDistance = -m_MoveOneItemLength;
            m_CurrentIndex++;
            if (pageText != null)
            {
                pageText.text = m_CurrentIndex + "/" + totalItemNum;
            }

            if (needSendMessage)
            {
                UpdatePanel(true);
            }

            DOTween.To(() => m_ContentTrans.localPosition,
                lerpValue => m_ContentTrans.localPosition = lerpValue,
                m_CurrentContentLocalPos + new Vector3(moveDistance, 0, 0), 0.5f)
                .SetEase(Ease.OutQuint);
            m_CurrentContentLocalPos += new Vector3(moveDistance, 0, 0);
        }

        public void ToLastPage()
        {
            if (m_CurrentIndex <= 1)
            {
                return;
            }

            var moveDistance = m_MoveOneItemLength;
            m_CurrentIndex--;
            if (pageText != null)
            {
                pageText.text = m_CurrentIndex.ToString() + "/" + totalItemNum;
            }

            if (needSendMessage)
            {
                UpdatePanel(false);
            }

            DOTween.To(() => m_ContentTrans.localPosition,
                lerpValue => m_ContentTrans.localPosition = lerpValue,
                m_CurrentContentLocalPos + new Vector3(moveDistance, 0, 0), 0.5f).SetEase(Ease.OutQuint);
            m_CurrentContentLocalPos += new Vector3(moveDistance, 0, 0);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            m_BeginMousePositionX = Input.mousePosition.x;
        }

        //设置Content的大小
        public void SetContentLength(int itemNum)
        {
            var sizeDelta = m_ContentTrans.sizeDelta;
            sizeDelta =
                new Vector2(sizeDelta.x + (cellLength + spacing) * (itemNum - 1),
                    sizeDelta.y);
            m_ContentTrans.sizeDelta = sizeDelta;
            totalItemNum = itemNum;
        }

        //初始化Content的大小
        public void InitScrollLength()
        {
            m_ContentTrans.sizeDelta = m_ContentTransSize;
        }

        //发送翻页信息的方法
        private void UpdatePanel(bool toNext)
        {
            gameObject.SendMessageUpwards(toNext ? "ToNextLevel" : "ToLastLevel");
        }
    }
}