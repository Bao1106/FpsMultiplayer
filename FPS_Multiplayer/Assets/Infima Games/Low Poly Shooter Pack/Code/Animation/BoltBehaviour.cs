﻿//Copyright 2022, Infima Games. All Rights Reserved.

using Infima_Games.Low_Poly_Shooter_Pack.Code.Character;
using Infima_Games.Low_Poly_Shooter_Pack.Code.Services;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

namespace Infima_Games.Low_Poly_Shooter_Pack.Code.Animation
{
	/// <summary>
	/// Bolt Action Behaviour. Makes sure that the weapon's animator also matches the bolt action animation.
	/// </summary>
	public class BoltBehaviour : StateMachineBehaviour
	{
		#region FIELDS

		/// <summary>
		/// Player Character.
		/// </summary>
		private CharacterBehaviour playerCharacter;

		/// <summary>
		/// Player Inventory.
		/// </summary>
		private InventoryBehaviour playerInventoryBehaviour;

		#endregion

		#region UNITY

		/// <summary>
		/// On State Enter.
		/// </summary>
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			//We need to get the character component.
			playerCharacter ??= ServiceLocator.Current.Get<IGameModeService>().GetPlayerCharacter();

			//Get Inventory.
			playerInventoryBehaviour ??= playerCharacter.GetInventory();

			//Try to get the equipped weapon's Weapon component.
			if (!(playerInventoryBehaviour.GetEquipped() is { } weaponBehaviour))
				return;
			
			//Get the weapon animator.
			var weaponAnimator = weaponBehaviour.gameObject.GetComponent<Animator>();
			//Play Bolt Action Animation.
			weaponAnimator.Play("Bolt Action");
		}

		#endregion
	}
}