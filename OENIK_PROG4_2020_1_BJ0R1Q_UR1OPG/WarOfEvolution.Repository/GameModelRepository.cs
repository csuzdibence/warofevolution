// <copyright file="GameModelRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using WarOfEvolution.Model;

    /// <summary>
    /// This class holds the gamemodel's repository.
    /// </summary>
    public class GameModelRepository : IGameModelRepository
    {
        private static Random rnd = new Random();
        private string levelXml = Config.DataFolder + "levels.xml";
        private string weaponXml = Config.DataFolder + "weapons.xml";
        private string profileXml = Config.DataFolder + "profiles.xml";

        /// <summary>
        /// This method builds a gamemodel from a profile and a level.
        /// </summary>
        /// <param name="model">The gamemodel.</param>
        /// <param name="profile">Selected profile.</param>
        /// <param name="levelNumber">Selected level's index.</param>
        /// <returns>The gamemodel, that we need.</returns>
        public GameModel LoadGameModel(GameModel model, MyProfile profile, int levelNumber)
        {
            this.BuildPlayer(model, profile);
            this.BuildLevel(model, levelNumber);
            return model;
        }

        /// <summary>
        /// This method saves the profile.
        /// </summary>
        /// <param name="profile">The Profile that played the game.</param>
        /// <param name="level">The current level's index.</param>
        public void SaveProfile(MyProfile profile, int level)
        {
            int profileLevel = profile.CompletedLevels <= level ? level + 1 : profile.CompletedLevels;
            XDocument xdoc = XDocument.Load(this.profileXml);
            XElement profileData = new XElement(
                "profile",
                new XElement("name", profile.Name),
                new XElement("gold", profile.NumOfGolds),
                new XElement("profilelevel", profileLevel));
            foreach (Weapon item in profile.Weapons)
            {
                XElement weapon = new XElement(
                    "weapon",
                    new XElement("wname", item.Name),
                    new XElement("damage", item.Damage),
                    new XElement("price", item.Price),
                    new XElement("level", item.LevelRequired));
                if (item is RangedWeapon)
                {
                    weapon.SetAttributeValue("type", "ranged");
                }
                else
                {
                    weapon.SetAttributeValue("type", "melee");
                }

                profileData.Add(weapon);
            }

            xdoc.Descendants("profile")?.Where(x => x.Element("name")?.Value == profile.Name).Remove();
            xdoc.Root.Add(profileData);
            xdoc.Save(this.profileXml);
        }

        /// <summary>
        /// Generating enemies.
        /// </summary>
        /// <param name="gameModel">The GameModel.</param>
        public void GenerateEnemies(GameModel gameModel)
        {
            for (int i = 0; i < rnd.Next(Config.MinNumOfEnemies, Config.MaxNumOfEnemies + 1); i++)
            {
                Enemy e = new Enemy(
                    rnd.Next((int)gameModel.GameWidth / 3, (int)gameModel.GameWidth - gameModel.Level.EnemyWidth),
                    (int)gameModel.GameHeight - Config.GroundY - (gameModel.Level.EnemyHeight - Config.PlayerHeight),
                    gameModel.Level.EnemyWidth,
                    gameModel.Level.EnemyHeight)
                { Health = gameModel.Level.EnemyMaxHealth };
                Random weaponRandom = new Random();
                int wRandomIndex = weaponRandom.Next(0, gameModel.Level.AvailableEnemyWeapons.Count);
                Weapon randWeapon = gameModel.Level.AvailableEnemyWeapons[wRandomIndex];
                if (randWeapon is MeleeWeapon)
                {
                    e.Weapon = new MeleeWeapon((int)e.Area.X - randWeapon.OffsetX, (int)e.Area.Y + randWeapon.OffsetY, (int)randWeapon.Area.Width, (int)randWeapon.Area.Height)
                    {
                        Damage = randWeapon.Damage,
                        Image = randWeapon.Image,
                        OffsetX = randWeapon.OffsetX,
                        OffsetY = randWeapon.OffsetY,
                        Name = randWeapon.Name,
                    };
                }
                else
                {
                    e.Weapon = new RangedWeapon((int)e.Area.X - randWeapon.OffsetX, (int)e.Area.Y + randWeapon.OffsetY, (int)randWeapon.Area.Width, (int)randWeapon.Area.Height)
                    {
                        Damage = randWeapon.Damage,
                        Image = randWeapon.Image,
                        OffsetX = randWeapon.OffsetX,
                        OffsetY = randWeapon.OffsetY,
                        Name = randWeapon.Name,
                    };
                }

                gameModel.Enemies.Add(e);
            }
        }

        private void BuildPlayer(GameModel gameModel, MyProfile profile)
        {
            XDocument xdoc = XDocument.Load(this.weaponXml);
            gameModel.Player = new Player(Config.PlayerStartingX, (int)gameModel.GameHeight - Config.GroundY, Config.PlayerWidth, Config.PlayerHeight);
            gameModel.Player.Profile = profile;
            gameModel.Player.Weapons = new List<Weapon>();
            foreach (var w in gameModel.Player.Profile.Weapons)
            {
                XElement xweapon = xdoc.Descendants("weapon").Where(x => x.Element("wname")?.Value == w.Name).FirstOrDefault();
                int weaponOffsetX = int.Parse(xweapon.Attribute("offsetX")?.Value);
                int weaponOffsetY = int.Parse(xweapon.Attribute("offsetY")?.Value);
                int weaponWidth = int.Parse(xweapon.Attribute("width")?.Value);
                int weaponHeight = int.Parse(xweapon.Attribute("height")?.Value);
                string image = xweapon.Element("image")?.Value;
                if (xweapon.Attribute("type")?.Value == "melee")
                {
                    gameModel.Player.Weapons.Add(new MeleeWeapon(
                    Config.PlayerStartingX + weaponOffsetX,
                    (int)gameModel.GameHeight + weaponOffsetY - Config.GroundY - (Config.PlayerHeight / 2),
                    weaponWidth,
                    weaponHeight)
                    { Damage = w.Damage, Name = w.Name, Price = w.Price, Image = image, OffsetX = weaponOffsetX, OffsetY = weaponOffsetY });
                }
                else
                {
                    gameModel.Player.Weapons.Add(new RangedWeapon(
                    Config.PlayerStartingX + weaponOffsetX,
                    (int)gameModel.GameHeight + weaponOffsetY - Config.GroundY - (Config.PlayerHeight / 2),
                    weaponWidth,
                    weaponHeight)
                    { Damage = w.Damage, Name = w.Name, Price = w.Price, Image = image, OffsetX = weaponOffsetX, OffsetY = weaponOffsetY });
                }
            }

            gameModel.Player.Health = Config.PlayerHealth;
            gameModel.Player.PlayerState = PlayerState.Moving;
            gameModel.Player.EquippedWeapon = gameModel.Player.Weapons.LastOrDefault();
        }

        private void BuildLevel(GameModel gameModel, int levelNumber)
        {
            XDocument xdoc = XDocument.Load(this.levelXml);
            XElement selectedLevel = xdoc.Descendants("level")?.Where(x => int.Parse(x.Element("number")?.Value) == levelNumber).FirstOrDefault();
            Level level = new Level();
            level.BackgroundImage = selectedLevel.Element("backgroundimage")?.Value;
            level.ObstacleImage = selectedLevel.Element("obstacle")?.Value;
            level.LevelTitle = selectedLevel.Element("title")?.Value;
            int randScenes = rnd.Next(Config.MinNumOfScenes, Config.MaxNumOfScenes + 1);
            for (int i = 0; i < randScenes; i++)
            {
                Scene scene = new Scene();
                int randObstacles = rnd.Next(Config.MinNumOfObstacles, Config.MaxNumOfObstacles + 1);

                for (int j = 0; j < randObstacles; j++)
                {
                    int randX = rnd.Next(((int)gameModel.GameWidth / 5) + (j * 50), (int)gameModel.GameWidth - (5 * Config.ObstacleWidth));
                    int randY = rnd.Next(((int)gameModel.GameHeight / 2) + (j * 50), (int)gameModel.GameHeight - (2 * Config.ObstacleHeight));
                    scene.Obstacles.Add(new Obstacle(randX, randY, Config.ObstacleWidth, Config.ObstacleHeight));
                }

                level.Scenes.Add(scene);
            }

            level.Portal = new Portal(
                (int)gameModel.GameWidth - Config.PortalSize - 50,
                (int)gameModel.GameHeight - Config.GroundY - (Config.PortalSize - Config.PlayerHeight),
                Config.PortalSize,
                Config.PortalSize);

            gameModel.Enemies = new List<Enemy>();
            level.AvailableEnemyWeapons = new List<Weapon>();
            foreach (var w in selectedLevel.Descendants("weapon"))
            {
                int weaponOffsetX = int.Parse(w.Attribute("offsetX")?.Value);
                int weaponOffsetY = int.Parse(w.Attribute("offsetY")?.Value);
                int weaponWidth = int.Parse(w.Attribute("width")?.Value);
                int weaponHeight = int.Parse(w.Attribute("height")?.Value);
                string image = w.Element("image")?.Value;
                if (w.Attribute("type")?.Value == "melee")
                {
                    level.AvailableEnemyWeapons.Add(new MeleeWeapon(0, 0, weaponWidth, weaponHeight)
                    {
                        Damage = int.Parse(w.Element("damage")?.Value),
                        Name = w.Element("wname")?.Value,
                        Image = image,
                        OffsetX = weaponOffsetX,
                        OffsetY = weaponOffsetY,
                    });
                }
                else
                {
                    level.AvailableEnemyWeapons.Add(new RangedWeapon(0, 0, weaponWidth, weaponHeight)
                    {
                        Damage = int.Parse(w.Element("damage")?.Value),
                        Name = w.Element("wname")?.Value,
                        Image = image,
                        OffsetX = weaponOffsetX,
                        OffsetY = weaponOffsetY,
                    });
                }
            }

            // Build Enemies and levelInfo
            string enemyImage = selectedLevel.Element("enemyimage")?.Value;
            int enemyHeight = int.Parse(selectedLevel.Element("enemyimage")?.Attribute("height")?.Value);
            int enemyWidth = int.Parse(selectedLevel.Element("enemyimage")?.Attribute("width")?.Value);
            int enemyHealth = int.Parse(selectedLevel.Element("enemyhealth")?.Value);
            level.EnemyValue = int.Parse(selectedLevel.Element("enemyvalue")?.Value);
            level.EnemyMaxHealth = enemyHealth;
            level.EnemyImage = enemyImage;
            level.EnemyHeight = enemyHeight;
            level.EnemyWidth = enemyWidth;
            level.SelectedScene = level.Scenes.FirstOrDefault();
            gameModel.Level = level;

            this.GenerateEnemies(gameModel);
        }
    }
}
