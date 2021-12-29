// <copyright file="Obstacle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Model
{
    /// <summary>
    /// The class of a Obstacle.
    /// </summary>
    public class Obstacle : GameShape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Obstacle"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the Obstacle.</param>
        /// <param name="y">Y coordinate of the Player.</param>
        /// <param name="w">Width of the Obstacle.</param>
        /// <param name="h">Height of the Obstacle.</param>
        public Obstacle(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
        }
    }
}