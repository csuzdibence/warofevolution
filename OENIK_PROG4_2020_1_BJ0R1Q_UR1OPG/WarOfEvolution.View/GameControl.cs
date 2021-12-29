// <copyright file="GameControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.View
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using WarOfEvolution.Logic;
    using WarOfEvolution.Model;

    /// <summary>
    /// This class controls the whole game.
    /// </summary>
    public class GameControl : FrameworkElement
    {
        private GameModel model;
        private GameLogic logic;
        private GameRenderer renderer;
        private DispatcherTimer tickTimer;
        private Stopwatch stw;
        private Game currentPage;
        private int levelIndex;

        /// <summary>
        /// This method initialize the control from a profile and a level index.
        /// </summary>
        /// <param name="profile">The selected profile.</param>
        /// <param name="index">The selected level's index.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns>The gamecontrol for the page.</returns>
        public GameControl Init(MyProfile profile, int index, Game currentPage)
        {
            this.currentPage = currentPage;
            this.stw = new Stopwatch();
            this.levelIndex = index;
            Window win = Application.Current.MainWindow;
            this.model = new GameModel(win.ActualWidth, win.ActualHeight);
            this.logic = GameLogic.CreateGameLogic();
            this.logic.LoadGameModel(this.model, profile, index);
            this.renderer = new GameRenderer(this.model);
            if (win != null)
            {
                this.tickTimer = new DispatcherTimer();
                this.tickTimer.Interval = TimeSpan.FromMilliseconds(20);
                this.tickTimer.Tick += this.TickTimer_Tick;
                this.tickTimer.Start();

                win.KeyDown += this.Win_KeyDown;
                this.MouseLeftButtonDown += this.GameControl_LeftMouseDown;
                this.MouseLeftButtonUp += this.GameControl_MouseLeftButtonUp;
                this.MouseRightButtonDown += this.GameControl_MouseRightButtonDown;
                win.KeyUp += this.Win_KeyUp;
            }

            this.InvalidateVisual();
            this.stw.Start();

            return this;
        }

        /// <summary>
        /// Resumes the game.
        /// </summary>
        public void ResumeGame()
        {
            this.tickTimer.Start();
            this.stw.Start();
        }

        /// <summary>
        /// This method changes the view to the main menu.
        /// </summary>
        public void ReturnToMainMenu()
        {
            this.currentPage.NavigationService.Navigate(new Menu());
            Window win = Application.Current.MainWindow;
            win.KeyDown -= this.Win_KeyDown;
            win.KeyUp -= this.Win_KeyUp;
        }

        /// <summary>
        /// This method calls the renderer.
        /// </summary>
        /// <param name="drawingContext">The drawinContext.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.renderer != null)
            {
                drawingContext.DrawDrawing(this.renderer.BuildDrawing());
            }
        }

        private void Win_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D:
                    this.logic.StopPlayer();
                    break;
                case Key.A:
                    this.logic.StopPlayer();
                    break;
            }
        }

        private void GameControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.logic.ReleaseMeleeWeapon();
        }

        private void GameControl_LeftMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.logic.PlayerMeleeAttack();

            this.InvalidateVisual();
        }

        private void GameControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.model.Player.EquippedWeapon is RangedWeapon)
            {
                this.model.Player.EquippedWeapon.IsThrown = true;
            }

            this.model.Player.EquippedWeapon.VelX = 10;
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    {
                        this.stw.Stop();
                        this.tickTimer.Stop();
                        this.currentPage.ToggleSideMenuVisibility();
                        break;
                    }

                case Key.D:
                    {
                        this.logic.MoveToRight();
                        break;
                    }

                case Key.A:
                    {
                        this.logic.MoveToLeft();
                        break;
                    }

                case Key.E:
                    this.logic.ChangeWeapon();
                    break;

                case Key.Space:
                    {
                        this.logic.PlayerMoveVertical();
                        break;
                    }
            }

            this.InvalidateVisual();
        }

        private void MoveToGameOver()
        {
            this.currentPage.NavigationService.Navigate(new GameOver());
            Window win = Application.Current.MainWindow;
            win.KeyDown -= this.Win_KeyDown;
            win.KeyUp -= this.Win_KeyUp;
            this.tickTimer.Stop();
        }

        private void TickTimer_Tick(object sender, EventArgs e)
        {
            if (this.logic.IsEndOfGame())
            {
                this.MoveToLevelCompleted();
            }

            if (!this.logic.PlayerAlive())
            {
                this.MoveToGameOver();
            }

            if (this.model.Player.EquippedWeapon.IsThrown)
            {
                this.logic.PlayerThrow();
            }

            this.logic.PlayerTick();

            foreach (Enemy item in this.model.Enemies)
            {
                this.logic.MoveEnemy(item);
            }

            this.InvalidateVisual();
        }

        private void MoveToLevelCompleted()
        {
            this.tickTimer.Stop();
            this.stw.Stop();
            double elapsedSeconds = this.stw.ElapsedMilliseconds / 1000;
            this.currentPage.time.Content = "TIME IN SECONDS: " + elapsedSeconds;
            double timeRate = (Config.LevelTime - elapsedSeconds) / 100;
            double bonusGold = this.CalculateBonus(elapsedSeconds, timeRate);
            int gold = (int)Math.Round(bonusGold, 0);
            if (gold > 0)
            {
                this.logic.AddGold(gold);
            }

            this.logic.SaveProfileData(this.levelIndex);
            this.currentPage.bonusGold.Content = "BONUS GOLDS: " + gold;
            this.currentPage.gameCompletedMenu.Visibility = Visibility.Visible;
        }

        private double CalculateBonus(double elapsedSeconds, double timeRate)
        {
           return elapsedSeconds < Config.LevelTime ? timeRate * (double)Config.BonusGoldBase * this.levelIndex : 0;
        }
    }
}
