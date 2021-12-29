// <copyright file="GameModel.cs" company="PlaceholderCompany">
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
    /// Class of the Game's model.
    /// </summary>
    public class GameModel : IGameModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// </summary>
        /// <param name="width">The width of the actual game.</param>
        /// <param name="height">The height of the actual game.</param>
        public GameModel(double width, double height)
        {
            this.GameWidth = width;
            this.GameHeight = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// Secondary for mocking.
        /// </summary>
        public GameModel()
        {
        }

        /// <summary>
        /// Gets or sets the Player.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Gets or sets the Collection of enemies.
        /// </summary>
        public ICollection<Enemy> Enemies { get; set; }

        /// <summary>
        /// Gets or sets the Level.
        /// </summary>
        public Level Level { get; set; }

        /// <summary>
        /// Gets or sets game window's Width.
        /// </summary>
        public double GameWidth { get; set; }

        /// <summary>
        /// Gets or sets game window's Height.
        /// </summary>
        public double GameHeight { get; set; }
    }
}
