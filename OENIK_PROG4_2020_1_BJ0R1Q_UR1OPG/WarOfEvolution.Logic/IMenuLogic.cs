// <copyright file="IMenuLogic.cs" company="PlaceholderCompany">
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
    /// This interface describes the MenuLogic.
    /// </summary>
    public interface IMenuLogic
    {
        /// <summary>
        /// Calculate a profile's highscore.
        /// </summary>
        /// <param name="profile">The given profile, that's being played.</param>
        /// <returns>Returns the score.</returns>
        int CalculateHighScore(MyProfile profile);

        /// <summary>
        /// Defines if the player can buy the weapon.
        /// </summary>
        /// <param name="myProfile">The given profile, that's being played.</param>
        /// <param name="weapon">The selected weapon that the player wants to buy.</param>
        /// <returns>True or false, depends on the weapon's prize.</returns>
        bool IsBuyable(MyProfile myProfile, Weapon weapon);

        /// <summary>
        /// Lists all the players providing duplicating.
        /// </summary>
        /// <returns>The list of all available players.</returns>
        List<MyProfile> ListAllProfiles();
    }
}
