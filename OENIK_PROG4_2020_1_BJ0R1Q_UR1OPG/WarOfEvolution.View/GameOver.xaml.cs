// <copyright file="GameOver.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.View
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for GameOver.xaml.
    /// </summary>
    public partial class GameOver : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameOver"/> class.
        /// </summary>
        public GameOver()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Menu());
        }
    }
}
