// <copyright file="ProfileRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using WarOfEvolution.Model;

    /// <summary>
    /// A profile's repository.
    /// </summary>
    public class ProfileRepository : IWriteableRepository<MyProfile>
    {
        private readonly string xmlFile = Config.DataFolder + "profiles.xml";

        private ICollection<MyProfile> profiles;

        /// <summary>
        /// Gets the Profiles in a collection.
        /// </summary>
        public ICollection<MyProfile> GetItems
        {
            get { return this.profiles; }
        }

        /// <summary>
        /// Loads the Profiles' data.
        /// </summary>
        public void LoadData()
        {
            this.profiles = new List<MyProfile>();
            XDocument xdoc = XDocument.Load(this.xmlFile);
            foreach (var profile in xdoc.Descendants("profile"))
            {
                MyProfile temp = new MyProfile(profile.Element("name")?.Value, int.Parse(profile.Element("gold").Value));
                temp.CompletedLevels = int.Parse(profile.Element("profilelevel")?.Value);
                foreach (var weapon in profile.Descendants("weapon"))
                {
                    if (weapon.Attribute("type")?.Value == "ranged")
                    {
                        temp.AddWeapon(new RangedWeapon()
                        {
                            Name = weapon.Element("wname")?.Value,
                            Damage = int.Parse(weapon.Element("damage")?.Value), Price = int.Parse(weapon.Element("price")?.Value),
                            LevelRequired = int.Parse(weapon.Element("level")?.Value),
                        });
                    }
                    else
                    {
                        temp.AddWeapon(new MeleeWeapon()
                        {
                            Name = weapon.Element("wname")?.Value,
                            Damage = int.Parse(weapon.Element("damage")?.Value), Price = int.Parse(weapon.Element("price")?.Value),
                            LevelRequired = int.Parse(weapon.Element("level")?.Value),
                        });
                    }
                }

                this.profiles.Add(temp);
            }
        }

        /// <summary>
        /// Writes the ProfileRepos data.
        /// </summary>
        /// <param name="collection">A collection of Profiles.</param>
        public void WriteData(ICollection<MyProfile> collection)
        {
            XDocument output = new XDocument(new XElement("profiles", string.Empty));
            foreach (var item in collection)
            {
                XElement profile = new XElement("profile", string.Empty);
                profile.Add(new XElement("name", item.Name));
                profile.Add(new XElement("gold", item.NumOfGolds));
                profile.Add(new XElement("profilelevel", item.CompletedLevels));
                foreach (var weapon in item.Weapons)
                {
                    XElement w = new XElement("weapon", string.Empty);
                    string wtype = weapon is MeleeWeapon ? "melee" : "ranged";
                    w.SetAttributeValue("type", wtype);
                    w.Add(new XElement("wname", weapon.Name));
                    w.Add(new XElement("damage", weapon.Damage));
                    w.Add(new XElement("price", weapon.Price));
                    w.Add(new XElement("level", weapon.LevelRequired));
                    profile.Add(w);
                }

                output.Root.Add(profile);
            }

            output.Save(this.xmlFile);
        }
    }
}
