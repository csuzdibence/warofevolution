// <copyright file="MovingShape.cs" company="PlaceholderCompany">
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
    /// This class holds a MovingShape's information.
    /// </summary>
    public class MovingShape : GameShape
    {
        private int velX;
        private int velY;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovingShape"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the MovingShape.</param>
        /// <param name="y">Y coordinate of the MovingShape.</param>
        /// <param name="w">Width of the MovingShape.</param>
        /// <param name="h">Height of the MovingShape.</param>
        public MovingShape(int x, int y, int w, int h)
            : base(x, y, w, h)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MovingShape"/> class.
        /// </summary>
        public MovingShape()
        {
        }

        /// <summary>
        /// Gets or sets the Shape's X component of velocity vector.
        /// </summary>
        public int VelX
        {
            get { return this.velX; }
            set { this.velX = value; }
        }

        /// <summary>
        /// Gets or sets the Shape's Y component of velocity vector.
        /// </summary>
        public int VelY
        {
            get { return this.velY; }
            set { this.velY = value; }
        }
    }
}
