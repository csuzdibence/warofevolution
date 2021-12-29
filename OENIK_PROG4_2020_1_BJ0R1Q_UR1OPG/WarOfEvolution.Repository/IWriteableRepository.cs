// <copyright file="IWriteableRepository.cs" company="PlaceholderCompany">
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
    /// The interface which describes a Writeable Repository.
    /// </summary>
    /// <typeparam name="T">type of T.</typeparam>
    public interface IWriteableRepository<T> : IRepository<T>
        where T : class
    {
        /// <summary>
        /// Writes Data to somewhere.
        /// </summary>
        /// <param name="collection">ICollection, type of T param.</param>
        void WriteData(ICollection<T> collection);
    }
}
