// <copyright file="Test.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WarOfEvolution.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit;
    using NUnit.Framework;
    using WarOfEvolution.Model;
    using WarOfEvolution.Repository;

    /// <summary>
    /// Testing.
    /// </summary>
    [TestFixture]
    internal class Test
    {
        /// <summary>
        /// First test.
        /// </summary>
        [Test]
        public void Test_PlayerIsAlive()
        {
            GameModel test = new GameModel() { Player = new Player() { Health = 10 } };
            Mock<IGameModelRepository> mockRepo2 = new Mock<IGameModelRepository>();
            mockRepo2.Setup(repo => repo.LoadGameModel(It.IsAny<GameModel>(), It.IsAny<MyProfile>(), 1)).Returns(test);
            GameLogic logic = new GameLogic(mockRepo2.Object);
            logic.LoadGameModel(new GameModel(), new MyProfile(" ", 0), 1);
            mockRepo2.Verify(repo => repo.LoadGameModel(It.IsAny<GameModel>(), It.IsAny<MyProfile>(), 1), Times.Once);
            Assert.That(logic.PlayerAlive());
        }

        /// <summary>
        /// StopPlayer method testing.
        /// </summary>
        [Test]
        public void Test_Stopp()
        {
            GameModel test = new GameModel() { Player = new Player(100, 100, 50, 50) };
            Mock<IGameModelRepository> mockRepo2 = new Mock<IGameModelRepository>();
            mockRepo2.Setup(repo => repo.LoadGameModel(It.IsAny<GameModel>(), It.IsAny<MyProfile>(), 1)).Returns(test);
            GameLogic logic = new GameLogic(mockRepo2.Object);
            logic.LoadGameModel(new GameModel(), new MyProfile(" ", 0), 1);
            mockRepo2.Verify(repo => repo.LoadGameModel(It.IsAny<GameModel>(), It.IsAny<MyProfile>(), 1), Times.Once);
            logic.StopPlayer();
            Assert.That(test.Player.VelY == 0);
        }

        /// <summary>
        /// Testing if anyone is alive.
        /// </summary>
        [Test]
        public void Test_EnemyListAnyofThemIsAlive()
        {
            GameModel test = new GameModel() { Enemies = new List<Enemy>() { new Enemy() { Health = 10 } } };
            Mock<IGameModelRepository> mockRepo2 = new Mock<IGameModelRepository>();
            mockRepo2.Setup(repo => repo.LoadGameModel(It.IsAny<GameModel>(), It.IsAny<MyProfile>(), 1)).Returns(test);
            GameLogic logic = new GameLogic(mockRepo2.Object);
            logic.LoadGameModel(new GameModel(), new MyProfile(" ", 0), 1);
            mockRepo2.Verify(repo => repo.LoadGameModel(It.IsAny<GameModel>(), It.IsAny<MyProfile>(), 1), Times.Once);
            Assert.That(logic.AnyoneAlive());
        }

        /// <summary>
        /// Enemy throw testing.
        /// </summary>
        [Test]
        public void Test_GoldAddingToProfile()
        {
            GameModel test = new GameModel() { Player = new Player(10, 10, 10, 10) { Profile = new MyProfile("Tony Startk", 100) } };
            Mock<IGameModelRepository> mockRepo2 = new Mock<IGameModelRepository>();
            mockRepo2.Setup(repo => repo.LoadGameModel(It.IsAny<GameModel>(), It.IsAny<MyProfile>(), 1)).Returns(test);
            GameLogic logic = new GameLogic(mockRepo2.Object);
            logic.LoadGameModel(new GameModel(), new MyProfile(" ", 0), 1);
            mockRepo2.Verify(repo => repo.LoadGameModel(It.IsAny<GameModel>(), It.IsAny<MyProfile>(), 1), Times.Once);
            logic.AddGold(100);
            Assert.IsTrue(test.Player.Profile.NumOfGolds == 200);
        }

        /// <summary>
        /// End of game testing.
        /// </summary>
        [Test]
        public void Test_IsEndOfGame()
        {
            GameModel test = new GameModel() { Player = new Player(10, 10, 10, 10) { Profile = new MyProfile("Tony Startk", 100) }, Level = new Level() { Portal = new Portal(10, 10, 10, 10) } };
            Mock<IGameModelRepository> mockRepo2 = new Mock<IGameModelRepository>();
            mockRepo2.Setup(repo => repo.LoadGameModel(It.IsAny<GameModel>(), It.IsAny<MyProfile>(), 1)).Returns(test);
            GameLogic logic = new GameLogic(mockRepo2.Object);
            logic.LoadGameModel(new GameModel(), new MyProfile(" ", 0), 1);
            mockRepo2.Verify(repo => repo.LoadGameModel(It.IsAny<GameModel>(), It.IsAny<MyProfile>(), 1), Times.Once);
            Assert.IsTrue(logic.IsEndOfGame());
        }

        /// <summary>
        /// Changing the weapon test.
        /// </summary>
        [Test]
        public void Test_WeaponChange()
        {
            GameModel test = new GameModel() { Player = new Player(10, 10, 10, 10) { Weapons = new List<Weapon>() { new Weapon() { Damage = 10 }, new Weapon() { Damage = 20 } }, EquippedWeapon = new Weapon() { Damage = 1 } } };
            Mock<IGameModelRepository> mockRepo2 = new Mock<IGameModelRepository>();
            mockRepo2.Setup(repo => repo.LoadGameModel(It.IsAny<GameModel>(), It.IsAny<MyProfile>(), 1)).Returns(test);
            GameLogic logic = new GameLogic(mockRepo2.Object);
            logic.LoadGameModel(new GameModel(), new MyProfile(" ", 0), 1);
            mockRepo2.Verify(repo => repo.LoadGameModel(It.IsAny<GameModel>(), It.IsAny<MyProfile>(), 1), Times.Once);
            logic.ChangeWeapon();
            Assert.That(test.Player.EquippedWeapon.Damage == 10);
        }
    }
}
