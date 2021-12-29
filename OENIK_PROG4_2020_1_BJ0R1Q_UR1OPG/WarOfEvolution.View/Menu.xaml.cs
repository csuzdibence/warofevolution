// <copyright file="Menu.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.View
{
    using System.Windows.Controls;
    using WarOfEvolution.View.VM;

    /// <summary>
    /// Interaction logic for Menu.xaml.
    /// </summary>
    public partial class Menu : Page
    {
        private MenuViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        public Menu()
        {
            this.InitializeComponent();
            this.vm = this.FindResource("MenuVM") as MenuViewModel;
            this.vm.CurrentPage = this;
        }

        private void LoadSideMenu(object sender, System.Windows.RoutedEventArgs e)
        {
            this.sideMenu.Visibility = System.Windows.Visibility.Visible;
        }

        private void ExitSideMenu(object sender, System.Windows.RoutedEventArgs e)
        {
            this.sideMenu.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
