// <copyright file="MenuLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WarOfEvolution.Model;
    using WarOfEvolution.Repository;

    /// <summary>
    /// The logic that handles the menu.
    /// </summary>
    public class MenuLogic : IMenuLogic
    {
        private IWriteableRepository<MyProfile> profileRepo;
        private IRepository<Weapon> weaponRepo;
        private ILevelRepository levelRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuLogic"/> class.
        /// </summary>
        /// <param name="profileRepo">The repository of the profile's.</param>
        public MenuLogic(IWriteableRepository<MyProfile> profileRepo)
        {
            this.profileRepo = profileRepo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuLogic"/> class.
        /// </summary>
        /// <param name="weaponRepo">The repository of the weapon's.</param>
        public MenuLogic(IRepository<Weapon> weaponRepo)
        {
            this.weaponRepo = weaponRepo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuLogic"/> class.
        /// </summary>
        /// <param name="levelRepo">The repository of the levels.</param>
        public MenuLogic(ILevelRepository levelRepo)
        {
            this.levelRepo = levelRepo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuLogic"/> class.
        /// </summary>
        /// <param name="weaponRepo">The repository of the weapon's.</param>
        /// <param name="profileRepo">The repository of the profile's.</param>
        public MenuLogic(IWriteableRepository<MyProfile> profileRepo, IRepository<Weapon> weaponRepo)
        {
            this.profileRepo = profileRepo;
            this.weaponRepo = weaponRepo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuLogic"/> class.
        /// </summary>
        /// <param name="weaponRepo">The repository of the weapon's.</param>
        /// <param name="profileRepo">The repository of the profile's.</param>
        /// /// <param name="levelRepo">The repository of the level's.</param>
        public MenuLogic(IWriteableRepository<MyProfile> profileRepo, IRepository<Weapon> weaponRepo, ILevelRepository levelRepo)
        {
            this.profileRepo = profileRepo;
            this.weaponRepo = weaponRepo;
            this.levelRepo = levelRepo;
        }

        /// <summary>
        /// Creates the logic's dependencies.
        /// </summary>
        /// <returns>The menu logic object.</returns>
        public static MenuLogic CreateLogic()
        {
            ProfileRepository profileRepo = new ProfileRepository();
            WeaponRepository weaponRepo = new WeaponRepository();
            LevelRepository levelRepo = new LevelRepository();
            return new MenuLogic(profileRepo, weaponRepo, levelRepo);
        }

        /// <summary>
        /// Calculate a profile's highscore.
        /// </summary>
        /// <param name="profile">The given profile, that's being played.</param>
        /// <returns>Returns the score.</returns>
        public int CalculateHighScore(MyProfile profile)
        {
            int weaponValues = profile.Weapons.Sum(x => x.Price);
            return weaponValues + profile.NumOfGolds;
        }

        /// <summary>
        /// Gets the top ten profile's highscores.
        /// </summary>
        /// <returns>The scores in a list.</returns>
        public List<Score> ObtainTopTenHighscores()
        {
            byte i = 1;
            List<Score> scores = new List<Score>();
            foreach (var profile in this.ListAllProfiles().OrderByDescending(x => this.CalculateHighScore(x)).Take(10))
            {
                scores.Add(new Score(i++, profile.Name, this.CalculateHighScore(profile)));
            }

            return scores;
        }

        /// <summary>
        /// Defines if the player can buy the weapon.
        /// </summary>
        /// <param name="myProfile">The given profile, that's being played.</param>
        /// <param name="weapon">The selected weapon that the player wants to buy.</param>
        /// <returns>True or false, depends on the weapon's prize.</returns>
        public bool IsBuyable(MyProfile myProfile, Weapon weapon)
        {
            bool isBuyable = true;
            if (myProfile.CompletedLevels >= weapon.LevelRequired && myProfile.NumOfGolds >= weapon.Price)
            {
                foreach (var item in myProfile.Weapons)
                {
                    if (item.Name == weapon.Name)
                    {
                        isBuyable = false;
                    }
                }
            }
            else
            {
                isBuyable = false;
            }

            if (isBuyable)
            {
                myProfile.NumOfGolds -= weapon.Price;
                myProfile.Weapons.Add(weapon);
            }

            return isBuyable;
        }

        /// <summary>
        /// Returns true if the profile can craft the weapon, false if it can't.
        /// </summary>
        /// <param name="profile">The profile that requests the craft.</param>
        /// <param name="weapon">The weapon that will be crafted (or not).</param>
        /// <returns>True/False.</returns>
        public bool IsCraftable(MyProfile profile, Weapon weapon)
        {
            if (weapon.IsCraftable && profile.Weapons.Where(x => x.Name == weapon.Name).Count() == 0 &&
                profile.Weapons.Where(x => x.Name == weapon.CraftItemA).Count() > 0
                && profile.Weapons.Where(x => x.Name == weapon.CraftItemB).Count() > 0)
            {
                profile.Weapons.Remove(profile.Weapons.Where(x => x.Name == weapon.CraftItemA).FirstOrDefault());
                profile.Weapons.Remove(profile.Weapons.Where(x => x.Name == weapon.CraftItemB).FirstOrDefault());
                return true;
            }

            return false;
        }

        /// <summary>
        /// Lists all the profiles providing duplicating.
        /// </summary>
        /// <returns>The list of all available profiles.</returns>
        public List<MyProfile> ListAllProfiles()
        {
            this.profileRepo.LoadData();
            return this.profileRepo.GetItems.ToList();
        }

        /// <summary>
        /// Lists of all the weapons.
        /// </summary>
        /// <returns>The list of all available weapons.</returns>
        public List<Weapon> ListAllWeapons()
        {
            this.weaponRepo.LoadData();
            return this.weaponRepo.GetItems.ToList();
        }

        /// <summary>
        /// Lists of all the standard level informations.
        /// </summary>
        /// <returns>The level informations.</returns>
        public List<LevelInfo> ListAllLevelInfos()
        {
            return this.levelRepo.ObtainLevelInfos();
        }

        /// <summary>
        /// This method saves the game changes.
        /// </summary>
        /// <param name="collection">The collection that we would like to save.</param>
        public void SaveGame(ICollection<MyProfile> collection)
        {
            this.profileRepo.WriteData(collection);
        }
    }
}
