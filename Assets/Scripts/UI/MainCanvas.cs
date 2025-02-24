using UnityEngine;

namespace UI
{
    public class MainCanvas : MonoBehaviour
    {
        public static MainCanvas Instance;

        [SerializeField] private Canvas canvas;

        void Awake()
        {
            if (Instance == null)  
                Instance = this;
        }

        public void EnableCanvas(bool enable)
        {
            canvas.enabled = enable;
        }
    }
}