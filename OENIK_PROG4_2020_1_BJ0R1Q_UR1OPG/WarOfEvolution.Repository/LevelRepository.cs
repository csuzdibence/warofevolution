// <copyright file="LevelRepository.cs" company="PlaceholderCompany">
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
    /// This class is the repository for the level.
    /// </summary>
    public class LevelRepository : ILevelRepository
    {
        private string xmlFile = Config.DataFolder + "levels.xml";

        /// <summary>
        /// Gets all the level infos.
        /// </summary>
        /// <returns>List of Levelinfos.</returns>
        public List<LevelInfo> ObtainLevelInfos()
        {
            XDocument xdoc = XDocument.Load(this.xmlFile);
            List<LevelInfo> levelInfos = new List<LevelInfo>();
            foreach (var level in xdoc.Descendants("level"))
            {
                levelInfos.Add(new LevelInfo() { Index = int.Parse(level.Element("number").Value), Title = level.Element("title").Value });
            }

            return levelInfos;
        }
    }
}
