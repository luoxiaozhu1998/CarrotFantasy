using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewTest : MonoBehaviour
{
    private ScrollRect _scrollRect;

    private RectTransform _contentRectTransform;
    // Start is called before the first frame update
    private void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _contentRectTransform = _scrollRect.content;
        Debug.Log("shijie" + _contentRectTransform.position);
        Debug.Log("jubu" + _contentRectTransform.localPosition);
        Debug.Log("jubu" + _contentRectTransform.rect.y);
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}