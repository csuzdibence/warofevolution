// <copyright file="IGameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that describes the game model.
    /// </summary>
    public interface IGameModel
    {
        /// <summary>
        /// Gets or sets the Player.
        /// </summary>
        Player Player { get; set; }

        /// <summary>
        /// Gets or sets the Collection of enemies.
        /// </summary>
        ICollection<Enemy> Enemies { get; set; }

        /// <summary>
        /// Gets or sets the Level.
        /// </summary>
        Level Level { get; set; }

        /// <summary>
        /// Gets or sets game window's Width.
        /// </summary>
        double GameWidth { get; set; }

        /// <summary>
        /// Gets or sets game window's Height.
        /// </summary>
        double GameHeight { get; set; }
    }
}
