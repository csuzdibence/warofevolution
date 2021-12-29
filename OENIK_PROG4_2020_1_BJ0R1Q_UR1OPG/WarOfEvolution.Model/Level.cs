// <copyright file="Level.cs" company="PlaceholderCompany">
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
    /// Class that contains the data from a level.
    /// </summary>
    public class Level
    {
        private Rect ground;

        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class.
        /// </summary>
        public Level()
        {
            this.Scenes = new List<Scene>();
        }

        /// <summary>
        /// Gets or sets the Scenes.
        /// </summary>
        public List<Scene> Scenes { get; set; }

        /// <summary>
        /// Gets or sets the selected scene.
        /// </summary>
        public Scene SelectedScene { get; set; }

        /// <summary>
        /// Gets or sets the level's title.
        /// </summary>
        public string LevelTitle { get; set; }

        /// <summary>
        /// Gets or sets the level's background image.
        /// </summary>
        public string BackgroundImage { get; set; }

        /// <summary>
        /// Gets or sets the level's obstacle image.
        /// </summary>
        public string ObstacleImage { get; set; }

        /// <summary>
        /// Gets or sets the portal.
        /// </summary>
        public Portal Portal { get; set; }

        /// <summary>
        /// Gets or sets the width of the enemy.
        /// </summary>
        public int EnemyWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the enemy.
        /// </summary>
        public int EnemyHeight { get; set; }

        /// <summary>
        /// Gets or sets the enemy image which will be rendered.
        /// </summary>
        public string EnemyImage { get; set; }

        /// <summary>
        /// Gets or sets the max health of the enemy.
        /// </summary>
        public int EnemyMaxHealth { get; set; }

        /// <summary>
        /// Gets or sets the enemy's value.
        /// </summary>
        public int EnemyValue { get; set; }

        /// <summary>
        /// Gets or sets the list of the available enemy weapons.
        /// </summary>
        public List<Weapon> AvailableEnemyWeapons { get; set; }

        /// <summary>
        /// Gets or sets the ground rectangle.
        /// </summary>
        public Rect Ground
        {
            get { return this.ground; }
            set { this.ground = value; }
        }
    }
}
