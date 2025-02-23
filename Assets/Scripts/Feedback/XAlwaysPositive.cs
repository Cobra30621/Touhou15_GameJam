using System;
using Player;
using UnityEngine;

namespace Feedback
{
    public class XAlwaysPositive : MonoBehaviour
    {
        public Transform parent;


        private void Awake()
        {
            parent = PlayerController.Instance.transform;
        }


        void Update()
        {
            if (parent.localScale.x < 0)
            {
                var transform1 = transform;
                var scale = transform1.localScale;
                scale = new Vector3(-1, scale.y, scale.z);
                transform1.localScale = scale;
            }
            else
            {
                var transform1 = transform;
                var scale = transform1.localScale;
                scale = new Vector3(1, scale.y, scale.z);
                transform1.localScale = scale;
            }
        }


    }
}