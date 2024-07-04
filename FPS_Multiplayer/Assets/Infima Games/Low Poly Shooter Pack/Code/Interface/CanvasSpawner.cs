//Copyright 2022, Infima Games. All Rights Reserved.

using Entities.Entity;
using Events;
using Managers;
using UnityEngine;

namespace Infima_Games.Low_Poly_Shooter_Pack.Code.Interface
{
    /// <summary>
    /// Player Interface.
    /// </summary>
    public class CanvasSpawner : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [Title(label: "Settings")]
        
        [Tooltip("Canvas prefab spawned at start. Displays the player's user interface.")]
        [SerializeField]
        private GameObject canvasPrefab;
        
        [Tooltip("Quality settings menu prefab spawned at start. Used for switching between different quality settings in-game.")]
        [SerializeField]
        private GameObject qualitySettingsPrefab;

        [SerializeField] private GamePlayer player;
        #endregion

        #region UNITY

        /// <summary>
        /// Awake.
        /// </summary>

        public void Initialize()
        {
            //Spawn Interface.
            var canvas = Instantiate(canvasPrefab).GetComponent<PlayerCanvasManager>();
            canvas.CanvasInitialize(player, player.PlayerName);
            canvas.CanvasAddListener();
            
            //Spawn Quality Settings Menu.
            Instantiate(qualitySettingsPrefab);
        }
        
        #endregion
    }
}