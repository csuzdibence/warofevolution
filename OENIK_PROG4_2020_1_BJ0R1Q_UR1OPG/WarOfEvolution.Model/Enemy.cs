// <copyright file="Enemy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Model
{
    /// <summary>
    /// This class holds the information of an enemy.
    /// </summary>
    public class Enemy : MovingShape
    {
        private int health;

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the Enemy.</param>
        /// <param name="y">Y coordinate of the Enemy.</param>
        /// <param name="w">Width of the Enemy.</param>
        /// <param name="h">Height of the Enemy.</param>
        public Enemy(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        public Enemy()
        {
        }

        /// <summary>
        /// Gets or sets the enemy's health.
        /// </summary>
        public int Health
        {
            get
            {
                return this.health;
            }

            set
            {
                if (value < 0)
                {
                    this.health = 0;
                }
                else
                {
                    this.health = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the enemy's weapon.
        /// </summary>
        public Weapon Weapon { get; set; }
    }
}