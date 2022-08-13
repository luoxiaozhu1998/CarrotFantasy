using UnityEngine;

namespace UI.UI
{
    public class CanvasDontDestroyOnLoad : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}