using System;
using System.Collections;
using System.Collections.Generic;
using Guns;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class GunUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text gunName;
        [SerializeField] private Shooting shooting;
        [SerializeField] private GameObject ammoPanel;
    
        private GunScriptableObjects _currentGun;
        private List<Transform> _bulletImages = new List<Transform>();
        private int _bulletIndex;

        private void OnEnable()
        {
            ShootingHelper.GunWasReloaded += OnGunReload;
            Shooting.GunWasFired += TakeAwayBulletImage;
        }

        private void Start()
        {
            _currentGun = shooting.CurrentGunScriptableObject;
            gunName.text = _currentGun.GunName;
            foreach (Transform bulletImage in ammoPanel.transform)
            {
                _bulletImages.Add(bulletImage);
            }
            SetAllBulletImagesActive();
        }
    
        private void OnDisable()
        {
            ShootingHelper.GunWasReloaded -= OnGunReload;
            Shooting.GunWasFired -= TakeAwayBulletImage;
        }


        private void SetAllBulletImagesActive()
        {
            foreach (var bulletImage in _bulletImages)
            {
                bulletImage.gameObject.SetActive(true);
            }
        }

        private void TakeAwayBulletImage()
        {
            if (_bulletIndex < _bulletImages.Count)
            {
                _bulletImages[_bulletIndex].gameObject.SetActive(false);
                _bulletIndex++;
            }
        }

        private void OnGunReload()
        {
            _bulletIndex = 0;
            SetAllBulletImagesActive();
        }
    }
}