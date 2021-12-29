// <copyright file="GameShape.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// General class for a Shape in the Game.
    /// </summary>
    public abstract class GameShape
    {
        private Rect area;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameShape"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the GameShape.</param>
        /// <param name="y">Y coordinate of the GameShape.</param>
        /// <param name="w">Width of the GameShape.</param>
        /// <param name="h">Height of the GameShape.</param>
        public GameShape(int x, int y, int w, int h)
        {
            this.area = new Rect(x, y, w, h);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameShape"/> class.
        /// </summary>
        public GameShape()
        {
        }

        /// <summary>
        /// Gets the Area of the rectangle.
        /// </summary>
        public Rect Area
        {
            get { return this.area; }
        }

        /// <summary>
        /// Changes the X coordinate with an amount.
        /// </summary>
        /// <param name="amount">The amount of changing.</param>
        public void ChangeX(double amount)
        {
            this.area.X += amount;
        }

        /// <summary>
        /// Changes the Y coordinate with an amount.
        /// </summary>
        /// <param name="amount">The amount of changing.</param>
        public void ChangeY(double amount)
        {
            this.area.Y += amount;
        }

        /// <summary>
        /// Changes the coordinates to an exact point.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void SetXY(double x, double y)
        {
            this.area.X = x;
            this.area.Y = y;
        }
    }
}
