// Copyright 2021, Infima Games. All Rights Reserved.

using Infima_Games.Low_Poly_Shooter_Pack___Free_Sample.Code.Character;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Game Mode Service.
    /// </summary>
    public interface IGameModeService : IGameService
    {
        /// <summary>
        /// Returns the Player Character.
        /// </summary>
        CharacterBehaviour GetPlayerCharacter();
    }
}