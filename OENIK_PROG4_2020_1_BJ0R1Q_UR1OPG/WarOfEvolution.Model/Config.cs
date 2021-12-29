// <copyright file="Config.cs" company="PlaceholderCompany">
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
    /// This class holds the configuration data for the game.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// The height of the player.
        /// </summary>
        public const int PlayerHeight = 170;

        /// <summary>
        /// The width of the player.
        /// </summary>
        public const int PlayerWidth = 150;

        /// <summary>
        /// The player's starting x coordinate.
        /// </summary>
        public const int PlayerStartingX = 50;

        /// <summary>
        /// The player's starting y coordinate.
        /// </summary>
        public const int PlayerStartingY = 300;

        /// <summary>
        /// The ground's y coordinate.
        /// </summary>
        public const int GroundY = 250;

        /// <summary>
        /// The height of the ground.
        /// </summary>
        public const int GroundHeight = 100;

        /// <summary>
        /// The player's health.
        /// </summary>
        public const int PlayerHealth = 1000;

        /// <summary>
        /// The folder of datas.
        /// </summary>
        public const string DataFolder = "../../Data/";

        /// <summary>
        /// The folder of pictures.
        /// </summary>
        public const string PictureFolder = "../../Pictures/";

        /// <summary>
        /// The goldtext's x coordinate.
        /// </summary>
        public const int GoldTextX = 1720;

        /// <summary>
        /// The goldtext's y coordinate.
        /// </summary>
        public const int GoldTextY = 65;

        /// <summary>
        /// The coin picture's size.
        /// </summary>
        public const int CoinSize = 100;

        /// <summary>
        /// The coin picture's x coordinate.
        /// </summary>
        public const int CoinX = 1750;

        /// <summary>
        /// The coin picture's y coordinate.
        /// </summary>
        public const int CoinY = 50;

        /// <summary>
        /// The profile name's x coordinate.
        /// </summary>
        public const int ProfileNameX = 50;

        /// <summary>
        /// The profile name's y coordinate.
        /// </summary>
        public const int ProfileNameY = 25;

        /// <summary>
        /// The player's health bar's X coordinate.
        /// </summary>
        public const int PlayerHealthBarX = 50;

        /// <summary>
        /// The player's health bar's Y coordinate.
        /// </summary>
        public const int PlayerHealthBarY = 80;

        /// <summary>
        /// The player's health bar's height.
        /// </summary>
        public const int PlayerHealthBarHeight = 25;

        /// <summary>
        /// The obstacle's Height.
        /// </summary>
        public const int ObstacleHeight = 64;

        /// <summary>
        /// The obstacle's Width.
        /// </summary>
        public const int ObstacleWidth = 64;

        /// <summary>
        /// The min number of obstacles generated to a scene.
        /// </summary>
        public const int MinNumOfObstacles = 3;

        /// <summary>
        /// The max number of obstacles generated to a scene.
        /// </summary>
        public const int MaxNumOfObstacles = 5;

        /// <summary>
        /// The min number of scenes generated to a level.
        /// </summary>
        public const int MinNumOfScenes = 6;

        /// <summary>
        /// The max number of scenes generated to a level.
        /// </summary>
        public const int MaxNumOfScenes = 8;

        /// <summary>
        /// The distance from the enemy to the enemy's health bar.
        /// </summary>
        public const int EnemyHealthBarDistance = 30;

        /// <summary>
        /// The height of the enemy's health bar.
        /// </summary>
        public const int EnemyHealthBarHeight = 10;

        /// <summary>
        /// The width of the enemy's health bar.
        /// </summary>
        public const int EnemyHealthBarWidth = 100;

        /// <summary>
        /// X coordinate offset on the enemy's healthbar.
        /// </summary>
        public const int EnemyHealthBarOffsetX = 25;

        /// <summary>
        /// The min number of enemies generated to a scene.
        /// </summary>
        public const int MinNumOfEnemies = 1;

        /// <summary>
        /// The max number of enemies generated to a scene.
        /// </summary>
        public const int MaxNumOfEnemies = 3;

        /// <summary>
        /// The size of the end portal.
        /// </summary>
        public const int PortalSize = 250;

        /// <summary>
        /// The gravity which pulls down the characters.
        /// </summary>
        public const int Gravity = 15;

        /// <summary>
        /// The time that the player should not cross if she/he want bonus gold.
        /// </summary>
        public const int LevelTime = 300;

        /// <summary>
        /// The base gold that can be added to bonus.
        /// </summary>
        public const int BonusGoldBase = 50;
    }
}
