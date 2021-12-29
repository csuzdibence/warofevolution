// <copyright file="HighscoreViewModel.cs" company="PlaceholderCompany">
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
    /// This class handles the highscore's viewmodel.
    /// </summary>
    public class HighscoreViewModel : ViewModelBase
    {
        private MyProfile profile;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighscoreViewModel"/> class.
        /// </summary>
        public HighscoreViewModel()
        {
            this.BackToMainCommand = new RelayCommand(() => this.ReturnToMain());
            this.Highscores = new ObservableCollection<Score>();
            MenuLogic logic = MenuLogic.CreateLogic();
            foreach (var score in logic.ObtainTopTenHighscores())
            {
                this.Highscores.Add(score);
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

        /// <summary>
        /// Gets or sets the command which can use in order to return to the main menu.
        /// </summary>
        public ICommand BackToMainCommand { get; set; }

        /// <summary>
        /// Gets the highscores in a list.
        /// </summary>
        public ObservableCollection<Score> Highscores { get; private set; }

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

        private void ReturnToMain()
        {
            this.CurrentPage.NavigationService.Navigate(this.LastPage);
        }
    }
}
