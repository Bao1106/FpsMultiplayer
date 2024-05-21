//Copyright 2022, Infima Games. All Rights Reserved.

using System.Globalization;

namespace Infima_Games.Low_Poly_Shooter_Pack.Code.Interface
{
    /// <summary>
    /// Total Ammunition Text.
    /// </summary>
    public class TextAmmunitionTotal : ElementText
    {
        #region METHODS
        
        /// <summary>
        /// Tick.
        /// </summary>
        protected override void Tick()
        {
            //Total Ammunition.
            float ammunitionTotal = equippedWeaponBehaviour.GetAmmunitionTotal();
            
            //Update Text.
            textMesh.text = ammunitionTotal.ToString(CultureInfo.InvariantCulture);
        }
        
        #endregion
    }
}