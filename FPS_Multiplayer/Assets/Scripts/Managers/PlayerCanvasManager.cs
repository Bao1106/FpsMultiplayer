using System;
using Interfaces;
using UI_Components.Component.Image;
using UI_Components.Component.Text;
using UnityEngine;

namespace Managers
{
    public class PlayerCanvasManager : MonoBehaviour
    {
        [SerializeField] private TextPlayerHealth playerHealth;
        [SerializeField] private ImgPlayerGetHit playerGetHit;

        public void CanvasInitialize(IEntity entity, string entityName)
        {
            playerHealth.Initialize(entityName);
            playerGetHit.Initialize(entityName);
        }

        public void CanvasAddListener()
        {
            playerHealth.OnAddListener();
            playerGetHit.OnAddListener();
        }
    }
}
