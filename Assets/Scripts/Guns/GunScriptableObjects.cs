using UnityEngine;

namespace Guns
{
    [CreateAssetMenu(menuName = "New Gun", fileName = "NewGun")] public class GunScriptableObjects : ScriptableObject
    {
        [SerializeField] private string gunName;
        public string GunName => gunName;
        [SerializeField] private int magazineSize;
        public int MagazineSize => magazineSize;
        [SerializeField] private float damagePerBullet;
        public float DamagePerBullet => damagePerBullet;
        [SerializeField] private float fireRate;
        public float FireRate => fireRate;
    }
}