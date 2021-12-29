// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    ///  Repository Interface.
    /// </summary>
    /// <typeparam name="T">T type Generic Repo.</typeparam>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Gets Items in a collection.
        /// </summary>
        ICollection<T> GetItems { get; }

        /// <summary>
        /// Loads some data.
        /// </summary>
        void LoadData();
    }
}
