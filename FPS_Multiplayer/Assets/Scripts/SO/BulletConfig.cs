using System;
using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO
{
    [Serializable]
    public class BulletClass
    {
        public BulletType type;
        public int damage;
    }
    
    [CreateAssetMenu(menuName = "Game Configs/Bullet Config", fileName = "Bullet Config", order = 1)]
    public class BulletConfig : SerializedScriptableObject
    {
        [TableList] [SerializeField] private List<BulletClass> lstBullet;

        public int GetBulletDamage(BulletType bulletType)
        {
            return lstBullet.Find(_ => _.type == bulletType).damage;
        }
    }
}
