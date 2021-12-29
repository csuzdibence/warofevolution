// <copyright file="Game.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.View
{
    using System.Windows;
    using System.Windows.Controls;
    using WarOfEvolution.Model;

    /// <summary>
    /// Interaction logic for Game.xaml.
    /// </summary>
    public partial class Game : Page
    {
        private MyProfile profile;
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="profile">The selected profile.</param>
        /// <param name="index">The selected level.</param>
        public Game(MyProfile profile, int index = 1)
            : this()
        {
            this.profile = profile;
            this.index = index;
        }

        /// <summary>
        /// Toggling the side menu's visibility.
        /// </summary>
        public void ToggleSideMenuVisibility()
        {
            this.sideMenu.Visibility = this.sideMenu.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.gameControl = this.gameControl.Init(this.profile, this.index, this);
        }

        private void QuitGameClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.gameControl.ReturnToMainMenu();
        }

        private void ResumeGameClick(object sender, RoutedEventArgs e)
        {
            this.ToggleSideMenuVisibility();
            this.gameControl.ResumeGame();
        }
    }
}
