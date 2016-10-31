using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SocialNetwork.DataAccess.Repositories;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;
using Telerik.JustMock;

namespace SocialNetwork.Services.UnitTest
{
    [TestFixture]
    public class MessageSerchingServiceTest
    {
        public void SearchByText_NeededMessagesExist()
        {
            var actualData = "My data";
            var actualData2 = "My data bla bla";
            var messages = new List<Message>
            {
                new Message { Data = actualData },
                new Message { Data = actualData },
                new Message { Data = actualData2 },
                new Message(),
                new Message()
            };
            var expectedCount = 3;
            var repository = Mock.Create<IRepository<Message>>();            
            Mock.Arrange(() => repository.GetAll()).Returns(messages);
            var unitOfWork = Mock.Create<IUnitOfWork>();
            Mock.Arrange(() => unitOfWork.Messages).Returns(repository);
            var service = new MessageSearchingService(unitOfWork);
            var expectMessages = service.SearchByText(actualData);

            foreach (var item in expectMessages)
            {
                Assert.AreEqual(actualData, item.Data);
            }

            Assert.AreEqual(expectedCount, expectMessages.Count());
        }

        [Test]
        public void SearchById_NeededMessageDoesNotExist()
        {
            var messages = new List<Message>
            {
                new Message(),
                new Message(),
                new Message(),
                new Message(),
                new Message()
            };
            var actualId = Guid.NewGuid();
            var repository = Mock.Create<IRepository<Message>>();
            Mock.Arrange(() => repository.GetAll()).Returns(messages);
            var unitOfWork = Mock.Create<IUnitOfWork>();
            Mock.Arrange(() => unitOfWork.Messages).Returns(repository);
            var service = new MessageSearchingService(unitOfWork);
            var expectMessage = service.SearchById(actualId);
            Assert.AreEqual(null, expectMessage);
        }

        [Test]
        public void SearchById_NeededMessageExist()
        {
            var messages = new List<Message>
            {
                new Message(),
                new Message(),
                new Message(),
                new Message(),
                new Message()
            };
            var actualId = Guid.NewGuid();
            var actualMessage = new Message { Id = actualId };
            messages.Add(actualMessage);
            var repository = Mock.Create<IRepository<Message>>();
            Mock.Arrange(() => repository.GetAll()).Returns(messages);
            var unitOfWork = Mock.Create<IUnitOfWork>();
            Mock.Arrange(() => unitOfWork.Messages).Returns(repository);
            var service = new MessageSearchingService(unitOfWork);
            var expectMessage = service.SearchById(actualId);
            Assert.AreEqual(actualMessage, expectMessage);
        }

        [Test]
        public void SearchByReceiver_NeededMessagesDoesNotExist()
        {
            var actualId = Guid.NewGuid();
            var messages = new List<Message>
            {
                new Message(),
                new Message(),
                new Message(),
                new Message(),
                new Message()
            };
            var expectedCount = 0;
            var repository = Mock.Create<IRepository<Message>>();
            Mock.Arrange(() => repository.GetAll()).Returns(messages);
            var unitOfWork = Mock.Create<IUnitOfWork>();
            Mock.Arrange(() => unitOfWork.Messages).Returns(repository);
            var service = new MessageSearchingService(unitOfWork);
            var expectMessages = service.SearchByReceiver(actualId);

            Assert.AreEqual(expectedCount, expectMessages.Count());
        }

        [Test]
        public void SearchByReceiver_NeededMessagesExist()
        {
            var actualId = Guid.NewGuid();
            var messages = new List<Message>
            {
                new Message { ReceiverId = actualId },
                new Message { ReceiverId = actualId },
                new Message { ReceiverId = actualId },
                new Message(),
                new Message()
            };
            var expectedCount = 3;
            var repository = Mock.Create<IRepository<Message>>();
            Mock.Arrange(() => repository.GetAll()).Returns(messages);
            var unitOfWork = Mock.Create<IUnitOfWork>();
            Mock.Arrange(() => unitOfWork.Messages).Returns(repository);
            var service = new MessageSearchingService(unitOfWork);
            var expectMessages = service.SearchByReceiver(actualId);

            foreach (var item in expectMessages)
            {
                Assert.AreEqual(actualId, item.ReceiverId);
            }

            Assert.AreEqual(expectedCount, expectMessages.Count());
        }

        [Test]
        public void SearchBySender_NeededMessagesDoesNotExist()
        {
            var actualId = Guid.NewGuid();
            var messages = new List<Message>
            {
                new Message(),
                new Message(),
                new Message(),
                new Message(),
                new Message()
            };
            var expectedCount = 0;
            var repository = Mock.Create<IRepository<Message>>();
            Mock.Arrange(() => repository.GetAll()).Returns(messages);
            var unitOfWork = Mock.Create<IUnitOfWork>();
            Mock.Arrange(() => unitOfWork.Messages).Returns(repository);
            var service = new MessageSearchingService(unitOfWork);
            var expectMessages = service.SearchBySender(actualId);

            Assert.AreEqual(expectedCount, expectMessages.Count());
        }

        [Test]
        public void SearchBySender_NeededMessagesExist()
        {
            var actualId = Guid.NewGuid();
            var messages = new List<Message>
            {
                new Message { SenderId = actualId },
                new Message { SenderId = actualId },
                new Message { SenderId = actualId },
                new Message(),
                new Message()
            };
            var expectedCount = 3;
            var repository = Mock.Create<IRepository<Message>>();
            Mock.Arrange(() => repository.GetAll()).Returns(messages);
            var unitOfWork = Mock.Create<IUnitOfWork>();
            Mock.Arrange(() => unitOfWork.Messages).Returns(repository);
            var service = new MessageSearchingService(unitOfWork);
            var expectMessages = service.SearchBySender(actualId);

            foreach (var item in expectMessages)
            {
                Assert.AreEqual(actualId, item.SenderId);
            }

            Assert.AreEqual(expectedCount, expectMessages.Count());
        }

        [Test]
        public void SearchByText_NeededMessagesDoesNotExist()
        {
            var actualData = "My data";
            var messages = new List<Message>
            {
                new Message { Data = "not data" },
                new Message { Data = "not data" },
                new Message { Data = "not data" },
                new Message { Data = "not data" },
                new Message { Data = "not data" }
            };
            var expectedCount = 0;
            var repository = Mock.Create<IRepository<Message>>();
            Mock.Arrange(() => repository.GetAll()).Returns(messages);
            var unitOfWork = Mock.Create<IUnitOfWork>();
            Mock.Arrange(() => unitOfWork.Messages).Returns(repository);
            var service = new MessageSearchingService(unitOfWork);
            var expectMessages = service.SearchByText(actualData);

            Assert.AreEqual(expectedCount, expectMessages.Count());
        }
    }
}