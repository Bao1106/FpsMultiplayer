//Copyright 2022, Infima Games. All Rights Reserved.

using UnityEngine;

namespace Infima_Games.Low_Poly_Shooter_Pack.Code.Weapons
{
    /// <summary>
    /// Magazine Behaviour.
    /// </summary>
    public abstract class MagazineBehaviour : MonoBehaviour
    {
        #region GETTERS
        
        /// <summary>
        /// Returns The Total Ammunition.
        /// </summary>
        public abstract int GetAmmunitionTotal();
        /// <summary>
        /// Returns the Sprite used on the Character's Interface.
        /// </summary>
        public abstract Sprite GetSprite();

        #endregion
    }
}