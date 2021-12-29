// <copyright file="MenuViewModel.cs" company="PlaceholderCompany">
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
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using WarOfEvolution.Logic;
    using WarOfEvolution.Model;

    /// <summary>
    /// The main menu's viewmodel.
    /// </summary>
    public class MenuViewModel : ViewModelBase
    {
        private MyProfile selectedProfile;

        private Visibility isTextBoxVisible = Visibility.Hidden;

        private string addProfileName;

        private string message;

        private string selectLevelMessage;

        private SolidColorBrush messageColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewModel"/> class.
        /// </summary>
        public MenuViewModel()
        {
            this.MoveToShopCommand = new RelayCommand(() => this.MoveToShop());
            this.MoveToHighscoreCommand = new RelayCommand(() => this.MoveToHighscore());
            this.SaveGameCommand = new RelayCommand(() => this.SaveGame());
            this.NewGameCommand = new RelayCommand(() => this.NewGame());
            this.LoadGameCommand = new RelayCommand(() => this.LoadGame());
            this.AddProfileCommand = new RelayCommand(() => this.AddProfile());
            this.DeleteProfileCommand = new RelayCommand(() => this.DeleteProfile());
            this.Profiles = new ObservableCollection<MyProfile>();
            MenuLogic logic = MenuLogic.CreateLogic();
            this.LevelInfos = new ObservableCollection<LevelInfo>(logic.ListAllLevelInfos());
            foreach (var item in logic.ListAllProfiles())
            {
                this.Profiles.Add(item);
            }

            this.SelectedProfile = this.Profiles.FirstOrDefault();
        }

        /// <summary>
        /// Gets or sets the Current page.
        /// </summary>
        public Page CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the selected profile.
        /// </summary>
        public MyProfile SelectedProfile
        {
            get
            {
                return this.selectedProfile;
            }

            set
            {
                this.Set(ref this.selectedProfile, value);
            }
        }

        /// <summary>
        /// Gets or sets the visibility of the textbox.
        /// </summary>
        public Visibility IsTextBoxVisible
        {
            get
            {
                return this.isTextBoxVisible;
            }

            set
            {
                this.Set(ref this.isTextBoxVisible, value);
            }
        }

        /// <summary>
        /// Gets or sets the profile name that can be added.
        /// </summary>
        public string ProfileName
        {
            get
            {
                return this.addProfileName;
            }

            set
            {
                this.Set(ref this.addProfileName, value);
            }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message
        {
            get
            {
                return this.message;
            }

            set
            {
                this.Set(ref this.message, value);
            }
        }

        /// <summary>
        /// Gets or sets the selection error message.
        /// </summary>
        public string SelectLevelMessage
        {
            get
            {
                return this.selectLevelMessage;
            }

            set
            {
                this.Set(ref this.selectLevelMessage, value);
            }
        }

        /// <summary>
        /// Gets or sets the message's color.
        /// </summary>
        public SolidColorBrush MessageColor
        {
            get
            {
                return this.messageColor;
            }

            set
            {
                this.Set(ref this.messageColor, value);
            }
        }

        /// <summary>
        /// Gets the command that moving the frame to the shop page.
        /// </summary>
        public ICommand MoveToShopCommand { get; private set; }

        /// <summary>
        /// Gets the command that moving the frame to the highscore page.
        /// </summary>
        public ICommand MoveToHighscoreCommand { get; private set; }

        /// <summary>
        /// Gets the command that saves the game.
        /// </summary>
        public ICommand SaveGameCommand { get; private set; }

        /// <summary>
        /// Gets the command that creates a new game.
        /// </summary>
        public ICommand NewGameCommand { get; private set; }

        /// <summary>
        /// Gets the command that adds a profile to the collection.
        /// </summary>
        public ICommand AddProfileCommand { get; private set; }

        /// <summary>
        /// Gets the command that deletes a profile from the collection.
        /// </summary>
        public ICommand DeleteProfileCommand { get; private set; }

        /// <summary>
        /// Gets the command that loads the game.
        /// </summary>
        public ICommand LoadGameCommand { get; private set; }

        /// <summary>
        /// Gets or sets the profiles.
        /// </summary>
        public ObservableCollection<MyProfile> Profiles { get; set; }

        /// <summary>
        /// Gets or sets all the level infos.
        /// </summary>
        public ObservableCollection<LevelInfo> LevelInfos { get; set; }

        /// <summary>
        /// Gets or sets the selected levelinfo.
        /// </summary>
        public LevelInfo SelectedLevelInfo { get; set; }

        private void SaveGame()
        {
            MenuLogic logic = MenuLogic.CreateLogic();
            logic.SaveGame(this.Profiles);
        }

        private void NewGame()
        {
            this.SelectedProfile.NumOfGolds = 0;
            this.SelectedProfile.CompletedLevels = 1;
            this.SelectedProfile.Weapons = new List<Weapon>();
            this.SelectedProfile.AddWeapon(new RangedWeapon { Name = "rock", Damage = 10, Price = 20 });
            this.SelectedProfile.AddWeapon(new MeleeWeapon { Name = "stick", Damage = 15, Price = 30 });
            this.CurrentPage.NavigationService.Navigate(new Game(this.SelectedProfile));
        }

        private void MoveToShop()
        {
            this.CurrentPage.NavigationService.Navigate(new Shop(this.SelectedProfile, this.CurrentPage));
        }

        private void MoveToHighscore()
        {
            this.CurrentPage.NavigationService.Navigate(new Highscore(this.SelectedProfile, this.CurrentPage));
        }

        private void DeleteProfile()
        {
            if (this.Profiles.Count > 1)
            {
                this.Profiles.Remove(this.SelectedProfile);
                this.SelectedProfile = this.Profiles.First();
            }
        }

        private void AddProfile()
        {
            if (this.IsTextBoxVisible == Visibility.Hidden)
            {
                this.IsTextBoxVisible = Visibility.Visible;
            }
            else
            {
                if (this.ProfileName == string.Empty || this.Profiles.Where(x => x.Name == this.ProfileName).Count() > 0)
                {
                    this.Message = "There's a profile with that name.";
                    this.MessageColor = Brushes.IndianRed;
                }
                else
                {
                    MyProfile newProfile = new MyProfile(this.ProfileName, 0);
                    newProfile.CompletedLevels = 1;
                    newProfile.AddWeapon(new RangedWeapon { Name = "rock", Damage = 10, Price = 20 });
                    newProfile.AddWeapon(new MeleeWeapon { Name = "stick", Damage = 15, Price = 30 });
                    this.Profiles.Add(newProfile);
                    this.IsTextBoxVisible = Visibility.Hidden;
                    this.Message = "Adding OK";
                    this.MessageColor = Brushes.LightGreen;
                }

                this.ProfileName = string.Empty;
            }
        }

        private void LoadGame()
        {
            if (this.SelectedLevelInfo is null)
            {
                return;
            }

            if (this.SelectedLevelInfo.Index > this.SelectedProfile.CompletedLevels)
            {
                this.SelectLevelMessage = "This level is locked";
            }
            else
            {
                this.CurrentPage.NavigationService.Navigate(new Game(this.SelectedProfile, this.SelectedLevelInfo.Index));
                this.SelectLevelMessage = string.Empty;
            }
        }
    }
}
