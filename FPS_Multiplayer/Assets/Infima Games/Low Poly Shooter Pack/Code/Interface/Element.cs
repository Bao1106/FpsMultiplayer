﻿//Copyright 2022, Infima Games. All Rights Reserved.

using Infima_Games.Low_Poly_Shooter_Pack.Code.Character;
using Infima_Games.Low_Poly_Shooter_Pack.Code.Services;
using Infima_Games.Low_Poly_Shooter_Pack.Code.Weapons;
using InfimaGames.LowPolyShooterPack;
using Services;
using UnityEngine;

namespace Infima_Games.Low_Poly_Shooter_Pack.Code.Interface
{
    /// <summary>
    /// Interface Element.
    /// </summary>
    public abstract class Element : MonoBehaviour
    {
        #region FIELDS
        
        /// <summary>
        /// Game Mode Service.
        /// </summary>
        protected IGameModeService gameModeService;
        
        /// <summary>
        /// Player Character.
        /// </summary>
        protected CharacterBehaviour characterBehaviour;
        /// <summary>
        /// Player Character Inventory.
        /// </summary>
        protected InventoryBehaviour inventoryBehaviour;

        /// <summary>
        /// Equipped Weapon.
        /// </summary>
        protected WeaponBehaviour equippedWeaponBehaviour;
        
        #endregion

        #region UNITY

        /// <summary>
        /// Awake.
        /// </summary>
        protected virtual void Awake()
        {
            //Get Game Mode Service. Very useful to get Game Mode references.
            ServiceLocator.For(this).Get(out gameModeService);
            
            //Get Player Character.
            characterBehaviour = gameModeService.GetPlayerCharacter();
            //Get Player Character Inventory.
            inventoryBehaviour = characterBehaviour.GetInventory();
        }
        
        /// <summary>
        /// Update.
        /// </summary>
        private void Update()
        {
            //Ignore if we don't have an Inventory.
            if (Equals(inventoryBehaviour, null))
                return;

            //Get Equipped Weapon.
            equippedWeaponBehaviour = inventoryBehaviour.GetEquipped();
            
            //Tick.
            Tick();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Tick.
        /// </summary>
        protected virtual void Tick() {}

        #endregion
    }
}