// <copyright file="Weapon.cs" company="PlaceholderCompany">
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
    /// Defines the the weapon that the enemy and the player can carry.
    /// </summary>
    public class Weapon : MovingShape
    {
        /// <summary>
        /// True if the weapon has been thrown.
        /// </summary>
        public bool IsThrown = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Weapon"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the Weapon.</param>
        /// <param name="y">Y coordinate of the Weapon.</param>
        /// <param name="w">Width of the Weapon.</param>
        /// <param name="h">Height of the Weapon.</param>
        public Weapon(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Weapon"/> class.
        /// </summary>
        public Weapon()
        {
        }

        /// <summary>
        /// Gets or sets the name of the weapon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the damage of the weapon.
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// Gets or sets the Price of the weapon.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets the required level that you need to get this weapon.
        /// </summary>
        public int LevelRequired { get; set; }

        /// <summary>
        /// Gets or sets the offset from the player in the x coordinate.
        /// </summary>
        public int OffsetX { get; set; }

        /// <summary>
        /// Gets or sets the offset from the player in the y coordinate.
        /// </summary>
        public int OffsetY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets that the weapon is craftable.
        /// </summary>
        public bool IsCraftable { get; set; }

        /// <summary>
        /// Gets or sets the first craft item if it's craftable.
        /// </summary>
        public string CraftItemA { get; set; }

        /// <summary>
        /// Gets or sets the second craft item if it's craftable.
        /// </summary>
        public string CraftItemB { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public string Image { get; set; }
    }
}
