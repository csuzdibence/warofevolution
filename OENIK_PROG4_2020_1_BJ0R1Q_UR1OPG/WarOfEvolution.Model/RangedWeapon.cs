// <copyright file="RangedWeapon.cs" company="PlaceholderCompany">
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
    /// This class holds the information from a RangedWeapon.
    /// </summary>
    public class RangedWeapon : Weapon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RangedWeapon"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the Weapon.</param>
        /// <param name="y">Y coordinate of the Weapon.</param>
        /// <param name="w">Width of the Weapon.</param>
        /// <param name="h">Height of the Weapon.</param>
        public RangedWeapon(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangedWeapon"/> class.
        /// </summary>
        public RangedWeapon()
        {
        }
    }
}
