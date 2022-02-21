using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "defaultWeapon", menuName = "Weapons")]
    public class WeaponStats : ScriptableObject
    {
        public float fireRate;
    //lower is figher
        public float accuracy;
        public float zoomLevel;
        public float pelletCount;
        public float pelletSpeed;
        public float magSize;
        public float damage;
        public float recoilAmount;
        public string weaponName;
        public Mesh weaponMesh;
    }

