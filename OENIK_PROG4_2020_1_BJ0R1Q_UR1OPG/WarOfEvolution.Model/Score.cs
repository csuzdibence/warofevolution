// <copyright file="Score.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class handles a profile's score.
    /// </summary>
    public class Score
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Score"/> class.
        /// </summary>
        /// <param name="placing">The placing of a profile.</param>
        /// <param name="name">The name of a profile.</param>
        /// <param name="highscore">The highscore of a profile.</param>
        public Score(byte placing, string name, int highscore)
        {
            this.Name = name;
            this.Placing = placing;
            this.Highscore = highscore;
        }

        /// <summary>
        /// Gets or sets the name of a profile.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the placing of a profile.
        /// </summary>
        public byte Placing { get; set; }

        /// <summary>
        /// Gets or sets the highscore of a profile.
        /// </summary>
        public int Highscore { get; set; }
    }
}
