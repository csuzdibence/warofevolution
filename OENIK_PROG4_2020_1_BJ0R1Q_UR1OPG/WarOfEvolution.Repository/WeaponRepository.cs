// <copyright file="WeaponRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Repository
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using WarOfEvolution.Model;

    /// <summary>
    /// This class handles the weapons' repository.
    /// </summary>
    public class WeaponRepository : IRepository<Weapon>
    {
        private readonly string xmlFile = Config.DataFolder + "weapons.xml";

        private ICollection<Weapon> collection;

        /// <summary>
        /// Gets the weapons in an ICollection.
        /// </summary>
        public ICollection<Weapon> GetItems
        {
            get { return this.collection; }
        }

        /// <summary>
        /// This method loads the weapons to the collection.
        /// </summary>
        public void LoadData()
        {
            this.collection = new List<Weapon>();
            XDocument xdoc = XDocument.Load(this.xmlFile);
            foreach (var weapon in xdoc.Descendants("weapon"))
            {
                if (weapon.Attribute("type")?.Value == "ranged")
                {
                    this.collection.Add(new RangedWeapon()
                    {
                        Name = weapon.Element("wname")?.Value,
                        Damage = int.Parse(weapon.Element("damage")?.Value),
                        Price = int.Parse(weapon.Element("price")?.Value),
                        LevelRequired = int.Parse(weapon.Element("level")?.Value),
                        IsCraftable = weapon.Attribute("craftable")?.Value == "true",
                        CraftItemA = weapon.Attribute("itemA")?.Value,
                        CraftItemB = weapon.Attribute("itemB")?.Value,
                        Image = Config.PictureFolder + weapon.Element("image")?.Value,
                    });
                }
                else
                {
                    this.collection.Add(new MeleeWeapon()
                    {
                        Name = weapon.Element("wname")?.Value,
                        Damage = int.Parse(weapon.Element("damage")?.Value),
                        Price = int.Parse(weapon.Element("price")?.Value),
                        LevelRequired = int.Parse(weapon.Element("level")?.Value),
                        IsCraftable = weapon.Attribute("craftable")?.Value == "true",
                        CraftItemA = weapon.Attribute("itemA")?.Value,
                        CraftItemB = weapon.Attribute("itemB")?.Value,
                        Image = Config.PictureFolder + weapon.Element("image")?.Value,
                    });
                }
            }
        }
    }
}
