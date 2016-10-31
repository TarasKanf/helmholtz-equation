using System.Collections.Generic;
using NUnit.Framework;
using SocialNetwork.DataAccess.Repositories;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.UnitTest;

namespace SocialNetwork.DataAccess.IntegrationTests
{
    /*
    [TestFixture]
    public class XmlDataAccessTest
    {
        [SetUp]
        public void SetUp()
        {
            connectionRepository = injector.Get<IRepository<Connection>>();
            messageRepository = injector.Get<IRepository<Message>>();
            userRepository = injector.Get<IRepository<User>>();
            users = userRepository.GetAll();
            messages = messageRepository.GetAll();
        }

        private readonly DependencyInjector injector = new DependencyInjector();

        private IRepository<Connection> connectionRepository;

        private IRepository<Message> messageRepository;

        private IRepository<User> userRepository;

        private List<User> users;

        private List<Message> messages;

        [Test]
        public void UserRepositoryCreateTest()
        {
            var user = new User("Name1", "Surname1", "test1@gmail.com", "test1", "");
            userRepository.Create(user);
            int count = userRepository.GetAll().Count;
            int expectedCount = users.Count + 1;
            Assert.AreEqual(expectedCount, count);
            Assert.AreEqual(user.FirstName, userRepository.Get(user.Id).FirstName);
            Assert.AreEqual(user.Email, userRepository.Get(user.Id).Email);
            Assert.AreEqual(user.LastName, userRepository.Get(user.Id).LastName);
            Assert.AreEqual(user.Password, userRepository.Get(user.Id).Password);
        }

        [Test]
        public void UserRepositoryDeleteTest()
        {
            userRepository.Delete(users[0].Id);
            int count = userRepository.GetAll().Count;
            int expectedCount = users.Count - 1;
            Assert.AreEqual(expectedCount, count);
            Assert.AreEqual(null, userRepository.Get(users[0].Id));
        }

        [Test]
        public void UserRepositoryGetAllTest()
        {
            var user2 = new User("Name2", "Surname2", "test2@gmail.com", "test2", "");
            var user3 = new User("Name3", "Surname3", "test3@gmail.com", "test3", "");
            users.Add(user2);
            users.Add(user3);
            userRepository.Create(user2);
            userRepository.Create(user3);
            int count = userRepository.GetAll().Count;
            int expectedCount = users.Count;
            Assert.AreEqual(expectedCount, count);
            for (var i = 0; i < users.Count; ++i)
            {
                Assert.AreEqual(users[i].Email, userRepository.GetAll()[i].Email);
                Assert.AreEqual(users[i].FirstName, userRepository.GetAll()[i].FirstName);
                Assert.AreEqual(users[i].LastName, userRepository.GetAll()[i].LastName);
                Assert.AreEqual(users[i].Password, userRepository.GetAll()[i].Password);
            }
        }

        [Test]
        public void UserRepositoryGetTest()
        {
            var user2 = new User("Name2", "Surname2", "test2@gmail.com", "test2", "");
            users.Add(user2);
            userRepository.Update(users);
            Assert.AreEqual(user2.FirstName, userRepository.Get(user2.Id).FirstName);
            Assert.AreEqual(user2.LastName, userRepository.Get(user2.Id).LastName);
            Assert.AreEqual(user2.Email, userRepository.Get(user2.Id).Email);
            Assert.AreEqual(user2.Password, userRepository.Get(user2.Id).Password);
        }

        [Test]
        public void UserRepositoryUpdateTest()
        {
            users.Add(new User("Name2", "Surname2", "test2@gmail.com", "test2", ""));
            userRepository.Update(users);
            int count = userRepository.GetAll().Count;
            int expectedCount = users.Count;
            Assert.AreEqual(expectedCount, count);
        }
    }*/
}