// <copyright file="IGameModelRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WarOfEvolution.Model;

    /// <summary>
    /// This interface describes the GameModel's Repository.
    /// </summary>
    public interface IGameModelRepository
    {
        /// <summary>
        /// This method builds a gamemodel from a profile and a level.
        /// </summary>
        /// <param name="model">The gamemodel.</param>
        /// <param name="profile">Selected profile.</param>
        /// <param name="levelNumber">Selected level's index.</param>
        /// <returns>A new gamemodel.</returns>
        GameModel LoadGameModel(GameModel model, MyProfile profile, int levelNumber);

        /// <summary>
        /// This method saves the profile.
        /// </summary>
        /// <param name="profile">The Profile that played the game.</param>
        /// <param name="level">The current level's index.</param>
        void SaveProfile(MyProfile profile, int level);

        /// <summary>
        /// Generates a random number of enemies.
        /// </summary>
        /// <param name="gameModel">The GameModel.</param>
        void GenerateEnemies(GameModel gameModel);
    }
}
