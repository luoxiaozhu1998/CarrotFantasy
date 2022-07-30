using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlideCanCoverScrollView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    //容器长度
    private float _contentLength;

    private float _beginMousePositionX;

    private float _endMousePositionX;

    private ScrollRect _scrollRect;

    //上一个位置比例
    private float _lastProportion;

    public int cellLength;

    public int spacing;

    public int leftOffset;

    private float _upperLimit;

    private float _lowerLimit;

    private float _firstItemLength;

    private float _oneItemLength;

    private float _oneItemProportion;

    public int totalItemNum;

    private int _currentIndex;


    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _contentLength = _scrollRect.content.rect.width;
        _firstItemLength = cellLength / 2 + leftOffset;
        _oneItemLength = cellLength + spacing;
        _oneItemProportion = _oneItemLength / _contentLength;
        _lowerLimit = _firstItemLength / _contentLength;
        _upperLimit = 1 - _lowerLimit;
        _currentIndex = 1;
        _scrollRect.horizontalNormalizedPosition = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _beginMousePositionX = Input.mousePosition.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _endMousePositionX = Input.mousePosition.x;
        var offsetX = (_beginMousePositionX - _endMousePositionX) * 2;
        if (Mathf.Abs(offsetX) > _firstItemLength)
        {
            if (offsetX > 0)
            {
                //右滑
                if (_currentIndex >= totalItemNum)
                {
                    return; 
                }

                var moveCount = (int) ((offsetX - _firstItemLength) / _oneItemLength) + 1; //移动的格子数目
                _currentIndex += moveCount;
                if (_currentIndex >= totalItemNum)
                {
                    _currentIndex = totalItemNum;
                }

                _lastProportion += _oneItemProportion * moveCount;
                if (_lastProportion >= _upperLimit)
                {
                    _lastProportion = 1;
                }
            }
            else
            {
                //左滑
                if (_currentIndex <= 1)
                {
                    return;
                }

                var moveCount = (int) ((offsetX + _firstItemLength) / _oneItemLength) - 1; //移动的格子数目
                _currentIndex += moveCount;
                if (_currentIndex <= 1)
                {
                    _currentIndex = 1;
                }

                _lastProportion += _oneItemProportion * moveCount;
                if (_lastProportion <= _lowerLimit)
                {
                    _lastProportion = 0;
                }
            }
        }

        DOTween.To(() => _scrollRect.horizontalNormalizedPosition,
            lerpValue => _scrollRect.horizontalNormalizedPosition = lerpValue, _lastProportion,
            0.5f).SetEase(Ease.OutQuint);
    }
}