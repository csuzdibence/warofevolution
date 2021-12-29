// <copyright file="Player.cs" company="PlaceholderCompany">
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
    /// Player state describing enum.
    /// </summary>
    public enum PlayerState
    {
        /// <summary>
        /// Represents the moving state.
        /// </summary>
        Moving,

        /// <summary>
        /// Represents the jumping state
        /// </summary>
        Jumping,

        /// <summary>
        /// Represents the attacking state.
        /// </summary>
        Attacking,
    }

    /// <summary>
    /// Class that contains the Player's data.
    /// </summary>
    public class Player : MovingShape
    {
        private int health;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the Player.</param>
        /// <param name="y">Y coordinate of the Player.</param>
        /// <param name="w">Width of the Player.</param>
        /// <param name="h">Height of the Player.</param>
        public Player(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
        }

        /// <summary>
        /// Gets or sets the Player's health.
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
        /// Gets or sets the weapon which is equipped.
        /// </summary>
        public Weapon EquippedWeapon { get; set; }

        /// <summary>
        /// Gets or sets the available weapons for the player.
        /// </summary>
        public List<Weapon> Weapons { get; set; }

        /// <summary>
        /// Gets or sets the profile which controls the character.
        /// </summary>
        public MyProfile Profile { get; set; }

        /// <summary>
        /// Gets or sets the player's state.
        /// </summary>
        public PlayerState PlayerState { get; set; }
    }
}
