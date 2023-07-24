using System;
using Player;
using UnityEngine;

namespace Guns
{
    public class ShootingHelper : MonoBehaviour
    {
        private Shooting _shooting;
        
        public static bool _inAimState;
        public static event Action GunWasReloaded;
        private void Start()
        {
            _shooting = GetComponentInParent<Shooting>();
        }

        /// <summary>
        /// Animation Event, calls the parents reload script and invokes the event to update the Gun/Ammo UI
        /// </summary>
        public void CallParentReloadAnimationEvent()
        {
            _shooting.SetCurrentRoundsToMagazineSize();
            GunWasReloaded?.Invoke();
        }

        public void InAimState()
        {
            _inAimState = true;
        }

        public void ExitAimState()
        {
            _inAimState = false;
        }
    }
}
