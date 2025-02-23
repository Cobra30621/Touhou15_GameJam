using Player;
using UnityEngine;

namespace Item
{
    public class UITrackPlayer : MonoBehaviour
    {
        public Transform displayPos; //
        private RectTransform rectTransform;
        private Camera mainCamera;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            mainCamera = Camera.main;
            displayPos = PlayerController.Instance.UsingItemDisplayPos;
        }

        void Update()
        {
            if (displayPos != null)
            {
                Vector3 worldPosition = displayPos.position;
                Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
                rectTransform.position = screenPosition;
            }
        }
    }
}