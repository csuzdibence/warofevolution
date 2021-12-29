// <copyright file="ILevelRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WarOfEvolution.Model;

    /// <summary>
    /// Represents the interface of the levels' repository.
    /// </summary>
    public interface ILevelRepository
    {
        /// <summary>
        /// Gets all the level infos.
        /// </summary>
        /// <returns>List of Levelinfos.</returns>
        List<LevelInfo> ObtainLevelInfos();
    }
}
