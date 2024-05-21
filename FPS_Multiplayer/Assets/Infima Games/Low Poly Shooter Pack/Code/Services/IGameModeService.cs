//Copyright 2022, Infima Games. All Rights Reserved.

using Infima_Games.Low_Poly_Shooter_Pack.Code.Character;
using InfimaGames.LowPolyShooterPack;

namespace Infima_Games.Low_Poly_Shooter_Pack.Code.Services
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