// <copyright file="Shop.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.View
{
    using System.Windows.Controls;
    using WarOfEvolution.Model;
    using WarOfEvolution.View.VM;

    /// <summary>
    /// Interaction logic for Shop.xaml.
    /// </summary>
    public partial class Shop : Page
    {
        private ShopViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="Shop"/> class.
        /// </summary>
        public Shop()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shop"/> class.
        /// </summary>
        /// <param name="selectedProfile">The selected Profile.</param>
        /// <param name="lastPage">The last page.</param>
        public Shop(MyProfile selectedProfile, Page lastPage)
            : this()
        {
            this.vm = this.FindResource("ShopVM") as ShopViewModel;
            this.vm.Profile = selectedProfile;
            foreach (var weapon in this.vm.Profile.Weapons)
            {
                this.vm.WeaponsOfProfile.Add(weapon);
            }

            this.vm.CurrentPage = this;
            this.vm.LastPage = lastPage;
        }
    }
}
