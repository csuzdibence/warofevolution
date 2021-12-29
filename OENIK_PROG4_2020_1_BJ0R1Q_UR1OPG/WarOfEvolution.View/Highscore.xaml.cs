// <copyright file="Highscore.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.View
{
    using System.Windows.Controls;
    using WarOfEvolution.Model;
    using WarOfEvolution.View.VM;

    /// <summary>
    /// Interaction logic for Highscore.xaml.
    /// </summary>
    public partial class Highscore : Page
    {
        private HighscoreViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="Highscore"/> class.
        /// </summary>
        public Highscore()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Highscore"/> class.
        /// </summary>
        /// <param name="profile">The selected profile.</param>
        /// <param name="lastPage">The last page.</param>
        public Highscore(MyProfile profile, Page lastPage)
            : this()
        {
            this.vm = this.FindResource("HighscoreVM") as HighscoreViewModel;
            this.vm.Profile = profile;
            this.vm.CurrentPage = this;
            this.vm.LastPage = lastPage;
        }
    }
}
