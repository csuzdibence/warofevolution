// <copyright file="IGameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WarOfEvolution.Model;

    /// <summary>
    /// This interface describes the Game's Logic.
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>
        /// Decides if the game has ended.
        /// </summary>
        /// <returns>True if yes.</returns>
        bool IsEndOfGame();

        /// <summary>
        /// Decides if the player has more 0 hp.
        /// </summary>
        /// <returns>True if yes.</returns>
        bool PlayerAlive();

        /// <summary>
        /// Decides if any of the enemis is alive.
        /// </summary>
        /// <returns>True if yes.</returns>
        bool AnyoneAlive();

        /// <summary>
        /// Changes the melee weapons X.
        /// </summary>
        void ReleaseMeleeWeapon();

        /// <summary>
        /// Stops the player.
        /// </summary>
        void StopPlayer();

        /// <summary>
        /// Player ticking.
        /// </summary>
        void PlayerTick();

        /// <summary>
        /// Throws the ranged weapon.
        /// </summary>
        void PlayerThrow();

        /// <summary>
        /// Counting the gold, and adds the bonus.
        /// </summary>
        /// <param name="bonusGold">The amount of the bonus gold.</param>
        void AddGold(int bonusGold);

        /// <summary>
        /// Generates the enemies for the level.
        /// </summary>
        void GenerateEnemies();

        /// <summary>
        /// Moves the player to right.
        /// </summary>
        void MoveToRight();

        /// <summary>
        /// Saves the data of the profile.
        /// </summary>
        /// <param name="level">The number of the level.</param>
        void SaveProfileData(int level);

        /// <summary>
        /// Moves the player to left.
        /// </summary>
        void MoveToLeft();

        /// <summary>
        /// Changes the weapon to the next available.
        /// </summary>
        void ChangeWeapon();

        /// <summary>
        /// Helps to decide whether move the enemy to right or left.
        /// </summary>
        /// <param name="enemy">The enemy that wants to be moved.</param>
        /// <returns>Left or right as string.</returns>
        string WidhtHelper(Enemy enemy);

        /// <summary>
        /// Moves the enemy only to right/left.
        /// </summary>
        /// <param name="enemy">The enemy that needs to be moved.</param>
        void MoveEnemy(Enemy enemy);

        /// <summary>
        /// Moves the player to up.
        /// </summary>
        void PlayerMoveVertical();
    }
}
