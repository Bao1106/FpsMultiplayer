//Copyright 2022, Infima Games. All Rights Reserved.

using Infima_Games.Low_Poly_Shooter_Pack.Code.Character;
using InfimaGames.LowPolyShooterPack;

namespace Infima_Games.Low_Poly_Shooter_Pack.Code.Services
{
    /// <summary>
    /// Game Mode Service.
    /// </summary>
    public class GameModeService : IGameModeService
    {
        #region FIELDS
        
        /// <summary>
        /// The Player Character.
        /// </summary>
        private CharacterBehaviour playerCharacter;
        
        #endregion
        
        #region FUNCTIONS
        
        public CharacterBehaviour GetPlayerCharacter()
        {
            //Make sure we have a player character that is good to go!
            if (playerCharacter == null)
                playerCharacter = UnityEngine.Object.FindObjectOfType<CharacterBehaviour>();
            
            //Return.
            return playerCharacter;
        }
        
        #endregion
    }
}