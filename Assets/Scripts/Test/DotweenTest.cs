using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class DotweenTest : MonoBehaviour
    {
        private Image _maskImage;
        private Tween _maskTween;

        // Start is called before the first frame update
        void Start()
        {
            _maskImage = GetComponent<Image>();
            // DOTween.To(() => _maskImage.color, toColor => _maskImage.color = toColor,
            //     new Color(0, 0, 0, 0), 2f);
            // var tween = transform.DOLocalMoveX(100, 0.5f);
            // tween.PlayForward();
            _maskTween = transform.DOLocalMoveX(100, 0.5f);
            _maskTween.OnComplete(CompleteMethod);
            _maskTween.SetAutoKill(false);
            _maskTween.SetEase(Ease.InOutBounce);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _maskTween.Play();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _maskTween.PlayBackwards();
            }
        }

        private void CompleteMethod()
        {
            DOTween.To(() => _maskImage.color, toColor => _maskImage.color = toColor,
                new Color(0, 0, 0, 0), 2f);
        }
    }
}