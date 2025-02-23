using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;
using TMPro;
using System.Collections;

namespace Weapon
{
    public class PlayerWeaponUI : MonoBehaviour
    {
        [SerializeField] private GameObject bulletIconPrefab;

        private void Start()
        {
            Debug.Log($"{PlayerController.Instance}");
            PlayerController.Instance.OnBulletClipChanged.AddListener(UpdateBulletUI);
        }


        private void OnDisable()
        {
            PlayerController.Instance.OnBulletClipChanged.RemoveListener(UpdateBulletUI);
        }

        public int bulletCount = 0;
        public TextMeshProUGUI bulletCountText;
        public addbullet_manager addbulletManager;

        private void UpdateBulletUI(List<BulletClip> bulletClips)
        {
            if(bulletCount<bulletClips.Count)
            {
                addbulletManager.CreateFloatingUI(bulletClips[bulletClips.Count - 1].Sprite);
            }
            bulletCount = bulletClips.Count;
            bulletCountText.text = bulletCount.ToString();
        }


        

    }
}