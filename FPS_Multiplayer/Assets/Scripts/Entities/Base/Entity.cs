using System;
using Entities.Player;
using Interfaces;
using Services;
using UnityEngine;

namespace Entities.Base
{
    public class Entity : MonoBehaviour, IEntity
    {
        public float Health { get; set; } = 1000;
    }
}
