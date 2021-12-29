// <copyright file="MyProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Model
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// The class of profiles.
    /// </summary>
    public class MyProfile : INotifyPropertyChanged
    {
        private string name;

        private int gold;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyProfile"/> class.
        /// </summary>
        /// <param name="name">The name of the profile. This is the key.</param>
        /// <param name="gold">The gold number of the profile.</param>
        public MyProfile(string name, int gold)
        {
            this.Name = name;
            this.NumOfGolds = gold;
            this.Weapons = new List<Weapon>();
        }

        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the profile's name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.OPC();
            }
        }

        /// <summary>
        /// Gets or sets the profile's gold.
        /// </summary>
        public int NumOfGolds
        {
            get
            {
                return this.gold;
            }

            set
            {
                this.gold = value;
                this.OPC();
            }
        }

        /// <summary>
        /// Gets or sets how many levels the profile has completed.
        /// </summary>
        public int CompletedLevels { get; set; }

        /// <summary>
        /// Gets or sets the Weapons.
        /// </summary>
        public List<Weapon> Weapons { get; set; }

        /// <summary>
        /// This method adds a weapon to the weapon list.
        /// </summary>
        /// <param name="weapon">A weapon to be added.</param>
        public void AddWeapon(Weapon weapon)
        {
            this.Weapons.Add(weapon);
        }

        /// <summary>
        /// This method removes a weapon to the weapon list.
        /// </summary>
        /// <param name="weapon">A weapon to be removed.</param>
        public void RemoveWeapon(Weapon weapon)
        {
            this.Weapons.Remove(weapon);
        }

        /// <summary>
        /// This invokes the property changed event.
        /// </summary>
        /// <param name="prop">the property's name.</param>
        public void OPC([CallerMemberName] string prop = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}