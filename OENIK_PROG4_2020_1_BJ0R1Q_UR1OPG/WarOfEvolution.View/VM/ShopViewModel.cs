// <copyright file="ShopViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.View.VM
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using WarOfEvolution.Logic;
    using WarOfEvolution.Model;

    /// <summary>
    /// Handles the shop's viewmodel.
    /// </summary>
    public class ShopViewModel : ViewModelBase
    {
        private MyProfile profile;

        private string buyMessage;

        private string craftMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopViewModel"/> class.
        /// </summary>
        public ShopViewModel()
        {
            MenuLogic logic = MenuLogic.CreateLogic();
            this.AllWeapons = new ObservableCollection<Weapon>(logic.ListAllWeapons());
            this.WeaponsOfProfile = new ObservableCollection<Weapon>();
            this.CraftableWeapons = new ObservableCollection<Weapon>();
            foreach (var weapon in this.AllWeapons.Where(x => x.IsCraftable))
            {
                this.CraftableWeapons.Add(weapon);
            }

            foreach (var weapon in this.CraftableWeapons)
            {
                this.AllWeapons.Remove(weapon);
            }

            this.BackToMainCommand = new RelayCommand(() => this.ReturnToMain());
            this.BuyWeaponCommand = new RelayCommand(() => this.BuyWeapon());
            this.SellWeaponCommand = new RelayCommand(() => this.SellWeapon());
            this.CraftWeaponCommand = new RelayCommand(() => this.CraftWeapon());
        }

        /// <summary>
        /// Gets or sets the selected profile.
        /// </summary>
        public MyProfile Profile
        {
            get
            {
                return this.profile;
            }

            set
            {
                this.Set(ref this.profile, value);
            }
        }

        /// <summary>
        /// Gets or sets the command which can use in order to return to the main menu.
        /// </summary>
        public ICommand BackToMainCommand { get; set; }

        /// <summary>
        /// Gets or sets the command which sells a weapon.
        /// </summary>
        public ICommand SellWeaponCommand { get; set; }

        /// <summary>
        /// Gets or sets the command which buys a weapon.
        /// </summary>
        public ICommand BuyWeaponCommand { get; set; }

        /// <summary>
        /// Gets the command that crafts a weapon.
        /// </summary>
        public ICommand CraftWeaponCommand { get; private set; }

        /// <summary>
        /// Gets or sets all the weapons available in the shop.
        /// </summary>
        public ObservableCollection<Weapon> AllWeapons { get; set; }

        /// <summary>
        /// Gets or sets all the weapons that can be crafted.
        /// </summary>
        public ObservableCollection<Weapon> CraftableWeapons { get; set; }

        /// <summary>
        /// Gets or sets the selected weapon.
        /// </summary>
        public Weapon SelectedWeapon { get; set; }

        /// <summary>
        /// Gets or sets the selected weapon in the inventory.
        /// </summary>
        public Weapon InventoryWeapon { get; set; }

        /// <summary>
        /// Gets or sets the selected weapon in the crafting table.
        /// </summary>
        public Weapon SelectedCraftableWeapon { get; set; }

        /// <summary>
        /// Gets or sets weapons of the profile.
        /// </summary>
        public ObservableCollection<Weapon> WeaponsOfProfile { get; set; }

        /// <summary>
        /// Gets or sets buy error message.
        /// </summary>
        public string BuyMessage
        {
            get
            {
                return this.buyMessage;
            }

            set
            {
                this.Set(ref this.buyMessage, value);
            }
        }

        /// <summary>
        /// Gets or sets craft error message.
        /// </summary>
        public string CraftMessage
        {
            get
            {
                return this.craftMessage;
            }

            set
            {
                this.Set(ref this.craftMessage, value);
            }
        }

        /// <summary>
        /// Gets or sets the last page.
        /// </summary>
        public Page LastPage { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        public Page CurrentPage { get; set; }

        private void ReturnToMain()
        {
            this.CurrentPage.NavigationService.Navigate(this.LastPage);
        }

        private void SellWeapon()
        {
            if (this.InventoryWeapon != null)
            {
                this.Profile.NumOfGolds += this.InventoryWeapon.Price;
                this.Profile.Weapons.Remove(this.InventoryWeapon);
                this.WeaponsOfProfile.Remove(this.InventoryWeapon);
            }
        }

        private void BuyWeapon()
        {
            if (this.SelectedWeapon != null)
            {
                MenuLogic logic = MenuLogic.CreateLogic();
                if (logic.IsBuyable(this.Profile, this.SelectedWeapon))
                {
                    this.WeaponsOfProfile.Add(this.SelectedWeapon);
                    this.BuyMessage = string.Empty;
                }
                else
                {
                    this.BuyMessage = "You cannot buy that weapon!";
                }
            }
        }

        private void CraftWeapon()
        {
            if (this.SelectedCraftableWeapon != null)
            {
                MenuLogic logic = MenuLogic.CreateLogic();
                if (logic.IsCraftable(this.Profile, this.SelectedCraftableWeapon))
                {
                    this.CraftMessage = string.Empty;
                    this.WeaponsOfProfile.Add(this.SelectedCraftableWeapon);
                    this.Profile.AddWeapon(this.SelectedCraftableWeapon);
                    this.WeaponsOfProfile.Remove(this.WeaponsOfProfile.Where(x => x.Name == this.SelectedCraftableWeapon.CraftItemA).FirstOrDefault());
                    this.WeaponsOfProfile.Remove(this.WeaponsOfProfile.Where(x => x.Name == this.SelectedCraftableWeapon.CraftItemB).FirstOrDefault());
                }
                else
                {
                    this.CraftMessage = "You cannot craft this weapon";
                }
            }
        }
    }
}
