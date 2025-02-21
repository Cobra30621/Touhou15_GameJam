using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

namespace Weapon
{
    public class PlayerWeaponUI : MonoBehaviour
    {
        [SerializeField] private GameObject bulletIconPrefab;
        [SerializeField] private Transform bulletContainer;

        private void Start()
        {
            Debug.Log($"{PlayerController.Instance}");
            PlayerController.Instance.OnBulletClipChanged.AddListener(UpdateBulletUI);
        }


        private void OnDisable()
        {
            PlayerController.Instance.OnBulletClipChanged.RemoveListener(UpdateBulletUI);
        }

        private void UpdateBulletUI(List<BulletClip> bulletClips)
        {
            foreach (Transform child in bulletContainer)
            {
                Destroy(child.gameObject);
            }

            foreach (var clip in bulletClips)
            {
                GameObject bulletIcon = Instantiate(bulletIconPrefab, bulletContainer);
                bulletIcon.GetComponent<Image>().sprite = clip.Sprite;
            }
        }
    }
}