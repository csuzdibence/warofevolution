// <copyright file="GameRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.View
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using WarOfEvolution.Model;

    /// <summary>
    /// This class has the responsible to render every object.
    /// </summary>
    public class GameRenderer
    {
        private GameModel model;
        private string picturesFolder = "WarOfEvolution.View.Pictures.";

        /// <summary>
        /// The general gamemodel drawings.
        /// </summary>
        private Drawing oldBackground;
        private Drawing oldPlayer;
        private Drawing oldPlayerWeaponDrawing;
        private Weapon oldPlayerWeapon;
        private Drawing oldPortal;
        private List<Drawing> oldEnemies;
        private List<Drawing> oldEnemyWeapons;
        private List<Drawing> oldObstacles;
        private Scene oldScene;

        /// <summary>
        /// The coin drawings.
        /// </summary>
        private Drawing oldCoin;
        private FormattedText text;
        private Pen textPen;
        private int oldNumGolds;
        private Drawing goldTextDrawing;

        /// <summary>
        /// The profile drawings.
        /// </summary>
        private FormattedText profileText;
        private Drawing oldProfileTextDrawing;

        /// <summary>
        /// The player's health bar drawings.
        /// </summary>
        private Drawing oldPlayerFullHealthBar;
        private Drawing oldPlayerHealthBar;
        private int oldPlayerHealth;

        private Point oldPlayerPosition;
        private Dictionary<string, Brush> myBrushes = new Dictionary<string, Brush>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRenderer"/> class.
        /// </summary>
        /// <param name="model">The game model.</param>
        public GameRenderer(GameModel model)
        {
            this.model = model;
        }

        private Brush PlayerBrush
        {
            get { return this.GetBrush(this.picturesFolder + "player.png"); }
        }

        private Brush JumpingPlayerBrush
        {
            get { return this.GetBrush(this.picturesFolder + "player_jump.png"); }
        }

        private Brush CoinBrush
        {
            get { return this.GetBrush(this.picturesFolder + "coin.png"); }
        }

        private Brush BackgroundBrush
        {
            get { return this.GetBrush(this.picturesFolder + this.model.Level.BackgroundImage); }
        }

        private Brush PortalBrush
        {
            get { return this.GetBrush(this.picturesFolder + "portal.png"); }
        }

        private Brush ObstacleBrush
        {
            get { return this.GetBrush(this.picturesFolder + this.model.Level.ObstacleImage); }
        }

        private Brush PlayerWeaponBrush
        {
            get { return this.GetBrush(this.picturesFolder + this.model.Player.EquippedWeapon.Image); }
        }

        private Brush EnemyBrush
        {
            get { return this.GetBrush(this.picturesFolder + this.model.Level.EnemyImage); }
        }

        /// <summary>
        /// This method builds the entire drawing.
        /// </summary>
        /// <returns>The builded drawing.</returns>
        public Drawing BuildDrawing()
        {
            DrawingGroup dg = new DrawingGroup();
            if (this.model.Level.BackgroundImage != string.Empty)
            {
                dg.Children.Add(this.GetBackground());
            }

            dg.Children.Add(this.GetGoldText());
            dg.Children.Add(this.GetCoin());
            dg.Children.Add(this.GetProfileText());
            dg.Children.Add(this.GetFullPlayerHealthBar());
            dg.Children.Add(this.GetPlayerHealthBar());
            this.GetObstacles().ForEach(x => dg.Children.Add(x));
            if (this.model.Level.Scenes.IndexOf(this.model.Level.SelectedScene) == this.model.Level.Scenes.Count - 1)
            {
                dg.Children.Add(this.GetPortal());
            }

            this.GetEnemies().ForEach(x => dg.Children.Add(x));
            this.GetEnemyWeapons().ForEach(x => dg.Children.Add(x));
            dg.Children.Add(this.GetPlayer());
            dg.Children.Add(this.GetPlayerWeapon());
            return dg;
        }

        /// <summary>
        /// This method resets the drawings.
        /// </summary>
        public void Reset()
        {
            this.oldBackground = null;
            this.oldCoin = null;
            this.oldPlayerFullHealthBar = null;
            this.oldPlayerHealthBar = null;
            this.oldPlayerWeaponDrawing = null;
            this.oldProfileTextDrawing = null;
            this.oldPlayer = null;
            this.oldEnemies = null;
            this.oldObstacles = null;
            this.text = null;
            this.textPen = null;
            this.oldPlayerPosition = new Point(-10, -10);
            this.myBrushes.Clear();
        }

        private Brush EnemyWeaponBrush(Enemy enemy)
        {
            return this.GetBrush(this.picturesFolder + enemy.Weapon.Image);
        }

        private Brush GetBrush(string fname)
        {
            if (!this.myBrushes.ContainsKey(fname))
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(fname);
                bmp.EndInit();
                ImageBrush ib = new ImageBrush(bmp);
                this.myBrushes[fname] = ib;
            }

            return this.myBrushes[fname];
        }

        private Drawing GetPlayer()
        {
            if (this.oldPlayer == null || this.oldPlayerPosition.X != this.model.Player.Area.X || this.oldPlayerPosition.Y != this.model.Player.Area.Y)
            {
                Geometry g = new RectangleGeometry(this.model.Player.Area);
                if (this.model.Player.PlayerState == PlayerState.Jumping)
                {
                    this.oldPlayer = new GeometryDrawing(this.JumpingPlayerBrush, null, g);
                }
                else
                {
                    this.oldPlayer = new GeometryDrawing(this.PlayerBrush, null, g);
                }

                this.oldPlayerPosition.X = this.model.Player.Area.X;
                this.oldPlayerPosition.Y = this.model.Player.Area.Y;
            }

            return this.oldPlayer;
        }

        private Drawing GetPlayerWeapon()
        {
            this.oldPlayerWeapon = this.model.Player.EquippedWeapon;
            Geometry g = new RectangleGeometry(this.model.Player.EquippedWeapon.Area);
            this.oldPlayerWeaponDrawing = new GeometryDrawing(this.PlayerWeaponBrush, null, g);

            return this.oldPlayerWeaponDrawing;
        }

        private Drawing GetGoldText()
        {
            if (this.textPen is null)
            {
                this.textPen = new Pen(Brushes.Black, 2);
            }

            if (this.text is null || this.oldNumGolds != this.model.Player.Profile.NumOfGolds)
            {
                this.text = new FormattedText(
                this.model.Player.Profile.NumOfGolds.ToString(),
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.RightToLeft,
                new Typeface("Segoe UI Black"),
                50,
                Brushes.Black);
            }

            if (this.goldTextDrawing is null || this.oldNumGolds != this.model.Player.Profile.NumOfGolds)
            {
                this.goldTextDrawing = new GeometryDrawing(Brushes.Azure, this.textPen, this.text.BuildGeometry(new Point(Config.GoldTextX, Config.GoldTextY)));
                this.oldNumGolds = this.model.Player.Profile.NumOfGolds;
            }

            return this.goldTextDrawing;
        }

        private Drawing GetCoin()
        {
            if (this.oldCoin is null)
            {
                Geometry g = new RectangleGeometry(new Rect(Config.CoinX, Config.CoinY, Config.CoinSize, Config.CoinSize));
                this.oldCoin = new GeometryDrawing(this.CoinBrush, null, g);
            }

            return this.oldCoin;
        }

        private Drawing GetProfileText()
        {
            if (this.textPen is null)
            {
                this.textPen = new Pen(Brushes.Black, 2);
            }

            if (this.profileText is null)
            {
                this.profileText = new FormattedText(
                    "Profile: " +
                this.model.Player.Profile.Name.ToString(),
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Segoe UI Black"),
                    30,
                    Brushes.Black);
            }

            if (this.oldProfileTextDrawing is null)
            {
                this.oldProfileTextDrawing = new GeometryDrawing(Brushes.Azure, this.textPen, this.profileText.BuildGeometry(new Point(Config.ProfileNameX, Config.ProfileNameY)));
            }

            return this.oldProfileTextDrawing;
        }

        private Drawing GetPlayerHealthBar()
        {
            if (this.oldPlayerHealthBar is null || this.oldPlayerHealth != this.model.Player.Health)
            {
                Geometry g = new RectangleGeometry(new Rect(Config.PlayerHealthBarX, Config.PlayerHealthBarY, this.model.Player.Health / 2, Config.PlayerHealthBarHeight));
                this.oldPlayerHealthBar = new GeometryDrawing(Brushes.Green, this.textPen, g);
                this.oldPlayerHealth = this.model.Player.Health;
            }

            return this.oldPlayerHealthBar;
        }

        private Drawing GetFullPlayerHealthBar()
        {
            if (this.oldPlayerFullHealthBar is null)
            {
                Geometry g = new RectangleGeometry(new Rect(Config.PlayerHealthBarX, Config.PlayerHealthBarY, Config.PlayerHealth / 2, Config.PlayerHealthBarHeight));
                this.oldPlayerFullHealthBar = new GeometryDrawing(Brushes.Red, this.textPen, g);
            }

            return this.oldPlayerFullHealthBar;
        }

        private Drawing GetBackground()
        {
            if (this.oldBackground is null)
            {
                Geometry g = new RectangleGeometry(new Rect(0, 0, this.model.GameWidth, this.model.GameHeight));
                this.oldBackground = new GeometryDrawing(this.BackgroundBrush, null, g);
            }

            return this.oldBackground;
        }

        private List<Drawing> GetObstacles()
        {
            if (this.oldObstacles is null || this.oldScene != this.model.Level.SelectedScene)
            {
                this.oldObstacles = new List<Drawing>();
                foreach (var obs in this.model.Level.SelectedScene.Obstacles)
                {
                    RectangleGeometry rg = new RectangleGeometry(obs.Area);
                    this.oldObstacles.Add(new GeometryDrawing(this.ObstacleBrush, null, rg));
                }

                this.oldScene = this.model.Level.SelectedScene;
            }

            return this.oldObstacles;
        }

        private List<Drawing> GetEnemies()
        {
            this.oldEnemies = new List<Drawing>();
            foreach (var enemy in this.model.Enemies)
            {
                Geometry en = new RectangleGeometry(enemy.Area);
                Geometry enHB = new RectangleGeometry(new Rect(enemy.Area.TopLeft.X + Config.EnemyHealthBarHeight, enemy.Area.TopLeft.Y - Config.EnemyHealthBarDistance, (double)enemy.Health / (double)this.model.Level.EnemyMaxHealth * Config.EnemyHealthBarWidth, Config.EnemyHealthBarHeight));
                Geometry enFHB = new RectangleGeometry(new Rect(enemy.Area.TopLeft.X + Config.EnemyHealthBarHeight, enemy.Area.TopLeft.Y - Config.EnemyHealthBarDistance, Config.EnemyHealthBarWidth, Config.EnemyHealthBarHeight));

                this.oldEnemies.Add(new GeometryDrawing(this.EnemyBrush, null, en));
                this.oldEnemies.Add(new GeometryDrawing(Brushes.Red, this.textPen, enFHB));
                this.oldEnemies.Add(new GeometryDrawing(Brushes.Green, this.textPen, enHB));
            }

            return this.oldEnemies;
        }

        private List<Drawing> GetEnemyWeapons()
        {
            this.oldEnemyWeapons = new List<Drawing>();
            foreach (var enemy in this.model.Enemies)
            {
                Geometry g = new RectangleGeometry(enemy.Weapon.Area);
                this.oldEnemyWeapons.Add(new GeometryDrawing(this.EnemyWeaponBrush(enemy), null, g));
            }

            return this.oldEnemyWeapons;
        }

        private Drawing GetPortal()
        {
            if (this.oldPortal is null)
            {
                Geometry g = new RectangleGeometry(this.model.Level.Portal.Area);
                this.oldPortal = new GeometryDrawing(this.PortalBrush, null, g);
            }

            return this.oldPortal;
        }
    }
}
