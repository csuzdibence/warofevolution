// <copyright file="MeleeWeapon.cs" company="PlaceholderCompany">
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
    /// This class holds the information about a Melee Weapon.
    /// </summary>
    public class MeleeWeapon : Weapon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeleeWeapon"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the Weapon.</param>
        /// <param name="y">Y coordinate of the Weapon.</param>
        /// <param name="w">Width of the Weapon.</param>
        /// <param name="h">Height of the Weapon.</param>
        public MeleeWeapon(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeleeWeapon"/> class.
        /// </summary>
        public MeleeWeapon()
        {
        }
    }
}
