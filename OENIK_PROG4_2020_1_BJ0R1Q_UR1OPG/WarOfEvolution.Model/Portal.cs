// <copyright file="Portal.cs" company="PlaceholderCompany">
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
    /// This class handles the exit portal's data.
    /// </summary>
    public class Portal : GameShape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Portal"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the Portal.</param>
        /// <param name="y">Y coordinate of the Portal.</param>
        /// <param name="w">Width of the Portal.</param>
        /// <param name="h">Height of the Portal.</param>
        public Portal(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
        }
    }
}
