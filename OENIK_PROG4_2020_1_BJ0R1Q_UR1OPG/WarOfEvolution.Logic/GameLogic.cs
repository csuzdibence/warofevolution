// <copyright file="GameLogic.cs" company="PlaceholderCompany">
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
    /// This class handles the entire game's logic.
    /// </summary>
    public class GameLogic : IGameLogic
    {
        private GameModel gameModel;
        private IGameModelRepository gameModelRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="gameModelRepo">The game model's repository.</param>
        public GameLogic(IGameModelRepository gameModelRepo)
        {
            this.gameModelRepo = gameModelRepo;
        }

        /// <summary>
        /// This method creates the game logic.
        /// </summary>
        /// <returns>The game logic.</returns>
        public static GameLogic CreateGameLogic()
        {
            return new GameLogic(new GameModelRepository());
        }

        /// <summary>
        /// This method loads the gameModel.
        /// </summary>
        /// <param name="model">The game model.</param>
        /// <param name="profile">The selected profile.</param>
        /// <param name="levelNumber">Selected level's index.</param>
        public void LoadGameModel(GameModel model, MyProfile profile, int levelNumber)
        {
            this.gameModel = this.gameModelRepo.LoadGameModel(model, profile, levelNumber);
        }

        /// <summary>
        /// Checks that the player has completed the game.
        /// </summary>
        /// <returns>True if the game should be ended.</returns>
        public bool IsEndOfGame()
        {
            int ind = this.gameModel.Level.Scenes.IndexOf(this.gameModel.Level.SelectedScene);
            if (ind == this.gameModel.Level.Scenes.Count - 1)
            {
                return this.gameModel.Player.Area.IntersectsWith(this.gameModel.Level.Portal.Area);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Moves the player to up.
        /// </summary>
        public void PlayerMoveVertical()
        {
            this.gameModel.Player.VelY = -400;
            this.gameModel.Player.PlayerState = PlayerState.Jumping;
            if (this.gameModel.Player.Area.Top > this.gameModel.GameHeight / 2)
            {
                this.gameModel.Player.ChangeY(this.gameModel.Player.VelY);
                foreach (var w in this.gameModel.Player.Weapons)
                {
                    if ((!w.IsThrown && this.gameModel.Player.EquippedWeapon is RangedWeapon) || (this.gameModel.Player.EquippedWeapon.IsThrown && w is MeleeWeapon) || this.gameModel.Player.EquippedWeapon is MeleeWeapon || (!this.gameModel.Player.EquippedWeapon.IsThrown && this.gameModel.Player.EquippedWeapon is RangedWeapon))
                    {
                        w.ChangeY(this.gameModel.Player.VelY);
                    }
                }
            }
        }

        /// <summary>
        /// Moves the enemy only to right/left.
        /// </summary>
        /// <param name="enemy">The enemy that needs to be moved.</param>
        public void MoveEnemy(Enemy enemy)
        {
            if (enemy.Weapon is MeleeWeapon)
            {
                this.MeleeWeaponHepler(enemy);
            }
            else if (enemy.Weapon is RangedWeapon)
            {
                this.RangedWeaponHelper(enemy);
            }
        }

        /// <summary>
        /// Player's melee attack interaction.
        /// </summary>
        public void PlayerMeleeAttack()
        {
            if (this.gameModel.Player.EquippedWeapon is MeleeWeapon)
            {
                this.gameModel.Player.EquippedWeapon.ChangeX(20);
                foreach (var enemy in this.gameModel.Enemies)
                {
                    if (enemy.Area.IntersectsWith(this.gameModel.Player.EquippedWeapon.Area))
                    {
                        enemy.Health -= this.gameModel.Player.EquippedWeapon.Damage;
                        if (enemy.Health <= 0)
                        {
                            this.gameModel.Player.Profile.NumOfGolds += this.gameModel.Level.EnemyValue;
                        }
                    }
                }

                this.gameModel.Enemies = this.gameModel.Enemies.Where(x => x.Health > 0).ToList();
            }
        }

        /// <summary>
        /// Return whether the player's hp is above 0.
        /// </summary>
        /// <returns>True if it has more than 0 hp.</returns>
        public bool PlayerAlive()
        {
            return this.gameModel.Player.Health > 0;
        }

        /// <summary>
        /// Checking if anyone of the enemies is alive.
        /// </summary>
        /// <returns>Bool.</returns>
        public bool AnyoneAlive()
        {
            foreach (Enemy item in this.gameModel.Enemies)
            {
                if (item.Health <= 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Player releases his melee weapon.
        /// </summary>
        public void ReleaseMeleeWeapon()
        {
            if (this.gameModel.Player.EquippedWeapon is MeleeWeapon)
            {
                this.gameModel.Player.EquippedWeapon.ChangeX(-20);
            }
        }

        /// <summary>
        /// Setting the player's velocity to zero.
        /// </summary>
        public void StopPlayer()
        {
            this.gameModel.Player.VelX = 0;
            this.gameModel.Player.VelY = 0;
        }

        /// <summary>
        /// Player interactions in a tick.
        /// </summary>
        public void PlayerTick()
        {
            this.gameModel.Player.ChangeX(this.gameModel.Player.VelX);
            foreach (var w in this.gameModel.Player.Weapons)
            {
                w.ChangeX(this.gameModel.Player.VelX);
            }

            if (this.gameModel.Player.Area.Bottom < this.gameModel.GameHeight - (Config.GroundY - Config.PlayerHeight))
            {
                this.gameModel.Player.ChangeY(Config.Gravity);
                foreach (var w in this.gameModel.Player.Weapons)
                {
                    if (!this.gameModel.Player.EquippedWeapon.IsThrown)
                    {
                        w.ChangeY(Config.Gravity);
                    }
                    else
                    {
                        w.ChangeY(0);
                    }
                }
            }
            else
            {
                this.gameModel.Player.PlayerState = PlayerState.Moving;
            }

            this.CheckPlayerMovement();
        }

        /// <summary>
        /// Throws the players weapon.
        /// </summary>
        public void PlayerThrow()
        {
            foreach (Obstacle item in this.gameModel.Level.SelectedScene.Obstacles)
            {
                if (this.gameModel.Player.EquippedWeapon.Area.IntersectsWith(item.Area))
                {
                    this.gameModel.Player.EquippedWeapon.IsThrown = false;
                    this.gameModel.Player.EquippedWeapon.SetXY(this.gameModel.Player.Area.X + 20, this.gameModel.Player.Area.Y + 100);
                }
            }

            if (this.gameModel.Player.EquippedWeapon is RangedWeapon && this.gameModel.Player.EquippedWeapon.IsThrown)
            {
                Enemy e = this.gameModel.Enemies.Where(x => x.Health > 0).Where(x => x.Area.X > this.gameModel.Player.Area.X).OrderBy(z => z.Area.X).FirstOrDefault();
                if (e != null)
                {
                    if (!this.gameModel.Player.EquippedWeapon.Area.IntersectsWith(e.Area))
                    {
                        this.gameModel.Player.EquippedWeapon.ChangeX(this.gameModel.Player.EquippedWeapon.VelX);
                    }

                    if (this.gameModel.Player.EquippedWeapon.Area.IntersectsWith(e.Area))
                    {
                        this.gameModel.Player.EquippedWeapon.IsThrown = false;
                        e.Health -= this.gameModel.Player.EquippedWeapon.Damage;
                        this.gameModel.Player.EquippedWeapon.SetXY(this.gameModel.Player.Area.X + 20, this.gameModel.Player.Area.Y + 100);
                    }

                    if (e.Health <= 0)
                    {
                        this.gameModel.Player.Profile.NumOfGolds += this.gameModel.Level.EnemyValue;
                    }
                }
                else
                {
                    if (this.gameModel.Player.EquippedWeapon.Area.X < 1400)
                    {
                        this.gameModel.Player.EquippedWeapon.ChangeX(this.gameModel.Player.EquippedWeapon.VelX);
                    }
                    else
                    {
                        this.gameModel.Player.EquippedWeapon.IsThrown = false;
                        this.gameModel.Player.EquippedWeapon.SetXY(this.gameModel.Player.Area.X + 10, this.gameModel.Player.Area.Y + 100);
                    }
                }

                this.gameModel.Enemies = this.gameModel.Enemies.Where(x => x.Health > 0).ToList();
            }
        }

        /// <summary>
        /// Adds a bonus gold amount to the player.
        /// </summary>
        /// <param name="bonusGold">The amount of bonus gold added.</param>
        public void AddGold(int bonusGold)
        {
            this.gameModel.Player.Profile.NumOfGolds += bonusGold;
        }

        /// <summary>
        /// Generates a random number of enemies.
        /// </summary>
        public void GenerateEnemies()
        {
            this.gameModelRepo.GenerateEnemies(this.gameModel);
        }

        /// <summary>
        /// Moves the player to the right direction.
        /// </summary>
        public void MoveToRight()
        {
            if (this.gameModel.Player.VelX < 8)
            {
                this.gameModel.Player.VelX += 4;
            }
        }

        /// <summary>
        /// Saves the recent profile's data.
        /// </summary>
        /// <param name="level">The level's index.</param>
        public void SaveProfileData(int level)
        {
            this.gameModelRepo.SaveProfile(this.gameModel.Player.Profile, level);
        }

        /// <summary>
        /// Moves the player to the left direction, and changes its face.
        /// </summary>
        public void MoveToLeft()
        {
            if (this.gameModel.Player.VelX > -8)
            {
                this.gameModel.Player.VelX -= 4;
            }

            if (this.gameModel.Player.Area.BottomLeft.X < 0)
            {
                this.gameModel.Player.VelX = 0;
            }
        }

        /// <summary>
        /// Changes the weapon whether it was ranged or melee.
        /// </summary>
        public void ChangeWeapon()
        {
            List<Weapon> weapons = this.gameModel.Player.Weapons;
            int index = weapons.IndexOf(this.gameModel.Player.EquippedWeapon);
            if (this.gameModel.Player.EquippedWeapon.IsThrown)
            {
                this.gameModel.Player.EquippedWeapon.SetXY(this.gameModel.Player.Area.X + 20, this.gameModel.Player.Area.Y + 100);
                this.gameModel.Player.EquippedWeapon.IsThrown = false;
            }

            if (index < weapons.Count() - 1)
            {
                this.gameModel.Player.EquippedWeapon = weapons[index + 1];
            }
            else
            {
                this.gameModel.Player.EquippedWeapon = weapons.First();
            }

            if (this.gameModel.Player.EquippedWeapon.Area.Y != this.gameModel.Player.Area.Y + 100)
            {
                this.gameModel.Player.EquippedWeapon.SetXY(this.gameModel.Player.EquippedWeapon.Area.X, this.gameModel.Player.Area.Y + 100);
            }
        }

        /// <summary>
        /// Aasdk.
        /// </summary>
        /// <param name="enemy">Enemy.</param>
        /// <returns>String.</returns>
        public string WidhtHelper(Enemy enemy)
        {
            if (enemy.Area.X > this.gameModel.Player.Area.X)
            {
                return "left";
            }
            else
            {
                return "right";
            }
        }

        private void Throw(Enemy enemy)
        {
            if (this.WidhtHelper(enemy) == "left")
            {
                enemy.Weapon.ChangeX(-8);
            }
            else if (this.WidhtHelper(enemy) == "right")
            {
                enemy.Weapon.ChangeX(8);
            }

            if (enemy.Weapon.Area.IntersectsWith(this.gameModel.Player.Area) || enemy.Weapon.Area.X == 0)
            {
                this.gameModel.Player.Health -= enemy.Weapon.Damage;
                enemy.Weapon.SetXY(enemy.Area.X, enemy.Area.Y + 120);
            }
        }

        /// <summary>
        /// Helps the enemy to get closer to the player.
        /// </summary>
        /// <param name="enemy">The enemy.</param>
        private void MeleeWeaponHepler(Enemy enemy)
        {
            if (this.gameModel.Player.Area.X < enemy.Area.X && !enemy.Area.IntersectsWith(this.gameModel.Player.Area))
            {
                if (Math.Abs(enemy.Area.X - this.gameModel.Player.Area.X) > 10.0)
                {
                    enemy.ChangeX(-4);
                    enemy.Weapon.ChangeX(-4);
                }
            }
            else if (this.gameModel.Player.Area.X > enemy.Area.X && !enemy.Area.IntersectsWith(this.gameModel.Player.Area))
            {
                if (Math.Abs(this.gameModel.Player.Area.X - enemy.Area.X) > 10.0)
                {
                    enemy.ChangeX(4);
                    enemy.Weapon.ChangeX(4);
                }
            }

            int weaponMoveValue = 20;
            if (enemy.Area.IntersectsWith(this.gameModel.Player.Area))
            {
                enemy.Weapon.SetXY(enemy.Area.X - enemy.Weapon.OffsetX - weaponMoveValue, enemy.Weapon.Area.Y);
                this.gameModel.Player.Health -= enemy.Weapon.Damage - 10;
            }
            else
            {
                enemy.Weapon.SetXY(enemy.Area.X - enemy.Weapon.OffsetX, enemy.Weapon.Area.Y);
            }
        }

        /// <summary>
        /// Helps coordinating the enemy.
        /// </summary>
        /// <param name="enemy">The enemy that holds the weapon.</param>
        private void RangedWeaponHelper(Enemy enemy)
        {
            if (this.gameModel.Player.Area.X < enemy.Area.X)
            {
                if (Math.Abs(enemy.Area.X - this.gameModel.Player.Area.X) > 450.0)
                {
                    enemy.VelX -= 4;
                    enemy.ChangeX(-4);
                    enemy.Weapon.ChangeX(-4);
                }
            }
            else if (this.gameModel.Player.Area.X > enemy.Area.X)
            {
                if (Math.Abs(this.gameModel.Player.Area.X - enemy.Area.X) > 450.0)
                {
                    enemy.VelX += 4;
                    enemy.ChangeX(4);
                    enemy.Weapon.ChangeX(4);
                }
            }

            if (Math.Abs(this.gameModel.Player.Area.X - enemy.Area.X) < 450.0 && Math.Abs(enemy.Area.X - this.gameModel.Player.Area.X) < 450.0)
            {
                this.Throw(enemy);
            }
        }

        private void CheckPlayerMovement()
        {
            if (this.gameModel.Player.Area.BottomRight.X > this.gameModel.GameWidth)
            {
                this.gameModel.Player.SetXY(Config.PlayerStartingX, this.gameModel.GameHeight - Config.GroundY);
                foreach (var w in this.gameModel.Player.Weapons)
                {
                    w.SetXY(Config.PlayerStartingX + this.gameModel.Player.EquippedWeapon.OffsetX, (int)this.gameModel.GameHeight + this.gameModel.Player.EquippedWeapon.OffsetY - Config.GroundY - (Config.PlayerHeight / 2));
                }

                int ind = this.gameModel.Level.Scenes.IndexOf(this.gameModel.Level.SelectedScene);
                if (ind < this.gameModel.Level.Scenes.Count())
                {
                    this.gameModel.Level.SelectedScene = this.gameModel.Level.Scenes[++ind];
                    this.GenerateEnemies();
                }
            }
        }

        private List<Enemy> Enemys()
        {
            return this.Enemys();
        }
    }
}
