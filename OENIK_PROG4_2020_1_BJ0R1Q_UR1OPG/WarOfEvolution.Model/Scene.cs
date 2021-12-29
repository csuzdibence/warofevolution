// <copyright file="Scene.cs" company="PlaceholderCompany">
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
    /// This class holds the information about a Scene.
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        public Scene()
        {
            this.Obstacles = new List<Obstacle>();
        }

        /// <summary>
        /// Gets or sets the Obstacles.
        /// </summary>
        public List<Obstacle> Obstacles { get; set; }
    }
}
