using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SocialNetwork.DataAccess.Repositories;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace SocialNetwork.Services.UnitTest
{
    [TestFixture]
    public class UserSearchingServiceTest
    {
        //// [Test]
        //// public void SearchByEmail_EmailParamNull_ShouldReturnNull()
        //// {
        ////    // arrange
        ////    var toFound = new User("Iryna", "Kachmar", "kachmar@gmail.com", "password", new ProfilePhoto());            
        ////    var users = new List<User>
        ////    {               
        ////        new User(string.Empty, string.Empty, string.Empty, string.Empty, new ProfilePhoto()),
        ////        new User(string.Empty, string.Empty, string.Empty, string.Empty, new ProfilePhoto())
        ////    };

        ////    users.Add(toFound);

        ////    var repository = Mock.Create<IUserRepository>();            
        ////    Mock.Arrange(() => repository.GetAll()).Returns(users);            

        ////    var unitOfWork = Mock.Create<IUnitOfWork>();          
        ////    Mock.Arrange(() => unitOfWork.Users).Returns(repository);

        ////    var searchingService = new UserSearchingService(unitOfWork);

        ////    // act
        ////    var userFounded = searchingService.SearchByEmail("kachmar@gmail.com");

        ////    // assert
        ////     Assert.AreEqual(toFound, toFound);
        //// }

        //// [Test]
        //// public void SearchByEmail_ExistsMoreThanOneNeededUser_ShouldReturnFirstNeeded()
        //// {
        ////    // arrange
        ////    var users = new List<User>
        ////    {
        ////        new User(null, null, "NotMyEmail1", null, null),
        ////        new User(null, null, "MyEmail", null, null),
        ////        new User(null, null, "MyEmail", null, null)
        ////    };
        ////    const int FisrtNeeded = 1;
        ////    var repository = Mock.Create<IRepository<User>>();
        ////    Mock.Arrange(() => repository.GetAll()).Returns(users);
        ////    var unitOfWork = Mock.Create<IUnitOfWork>();
        ////    Mock.Arrange(() => unitOfWork.Users).Returns(repository);
        ////    var searchingService = new UserSearchingService(unitOfWork);

        ////    // act
        ////    var userFounded = searchingService.SearchByEmail("MyEmail");

        ////    // assert
        ////    Assert.AreSame(users[FisrtNeeded], userFounded);
        //// }

        ////[Test]
        ////public void SearchByEmail_NeededUserDontExists_ShouldReturnNull()
        ////{
        ////    // arrange
        ////    var users = new List<User>
        ////    {
        ////        new User(null, null, "NotMyEmail1", null, null),
        ////        new User(null, null, "notMyEmail2", null, null),
        ////        new User(null, null, "notMyEmail3", null, null)
        ////    };
        ////    var repository = Mock.Create<IRepository<User>>();
        ////    Mock.Arrange(() => repository.GetAll()).Returns(users);
        ////    var unitOfWork = Mock.Create<IUnitOfWork>();
        ////    Mock.Arrange(() => unitOfWork.Users).Returns(repository);
        ////    var searchingService = new UserSearchingService(unitOfWork);

        ////    // act
        ////    var userFounded = searchingService.SearchByEmail("myEmail");

        ////    // assert
        ////    Assert.AreEqual(null, userFounded);
        ////}

        ////[Test]
        ////public void SearchByEmail_NeededUserExists_ShouldReturnUserWithThisEmail()
        ////{
        ////    // arrange
        ////    var users = new List<User>
        ////    {
        ////        new User(null, null, "myEmail", null, null),
        ////        new User(null, null, "notMyEmail1", null, null),
        ////        new User(null, null, "notMyEmail2", null, null)
        ////    };
        ////    var repository = Mock.Create<IRepository<User>>();
        ////    Mock.Arrange(() => repository.GetAll()).Returns(users);
        ////    var unitOfWork = Mock.Create<IUnitOfWork>();
        ////    Mock.Arrange(() => unitOfWork.Users).Returns(repository);
        ////    var searchingService = new UserSearchingService(unitOfWork);

        ////    // act
        ////    var userFounded = searchingService.SearchByEmail("myEmail");

        ////    // assert
        ////    Assert.AreEqual("myEmail", userFounded.Email);
        ////}

        ////[Test]
        ////public void SearchByFirstName_NeededUserDontExists_ShouldReturnCountZeroIEnumarable()
        ////{
        ////    // arrange
        ////    const string NeededFName = "NeededFName";
        ////    const string NotNeededFName = "NotNeededFName";
        ////    var users = new List<User>
        ////    {
        ////        new User(NotNeededFName, null, null, null, null),
        ////        new User(NotNeededFName, null, null, null, null),
        ////        new User(NotNeededFName, null, null, null, null)
        ////    };
        ////    var repository = Mock.Create<IRepository<User>>();
        ////    Mock.Arrange(() => repository.GetAll()).Returns(users);
        ////    var unitOfWork = Mock.Create<IUnitOfWork>();
        ////    Mock.Arrange(() => unitOfWork.Users).Returns(repository);
        ////    var searchingService = new UserSearchingService(unitOfWork);

        ////    // act
        ////    var usersFounded = searchingService.SearchByFirstName(NeededFName);

        ////    // assert
        ////    Assert.AreEqual(usersFounded.Count(), 0);
        ////}

        ////[Test]
        ////public void SearchByFirstName_NeededUserExists_ShouldReturnAllNeeded()
        ////{
        ////    // arrange
        ////    const string NeededFName = "NeededFName";
        ////    const string NotNeededFName = "NotNeededFName";
        ////    var users = new List<User>
        ////    {
        ////        new User(NotNeededFName, null, null, null, null),
        ////        new User(NeededFName, null, null, null, null),
        ////        new User(NeededFName, null, null, null, null)
        ////    };
        ////    var repository = Mock.Create<IRepository<User>>();
        ////    Mock.Arrange(() => repository.GetAll()).Returns(users);
        ////    var unitOfWork = Mock.Create<IUnitOfWork>();
        ////    Mock.Arrange(() => unitOfWork.Users).Returns(repository);
        ////    var searchingService = new UserSearchingService(unitOfWork);

        ////    // act
        ////    var usersFounded = searchingService.SearchByFirstName(NeededFName);

        ////    // assert
        ////    foreach (var user in usersFounded)
        ////    {
        ////        Assert.AreEqual(NeededFName, user.FirstName);
        ////    }
        ////}

        ////[Test]
        ////public void SearchById_NeededUserDontExists_ShouldReturnNull()
        ////{
        ////    // arrange
        ////    var users = new List<User> { new User(), new User(), new User(), new User() };
        ////    var repository = Mock.Create<IRepository<User>>();
        ////    Mock.Arrange(() => repository.GetAll()).Returns(users);
        ////    var unitOfWork = Mock.Create<IUnitOfWork>();
        ////    Mock.Arrange(() => unitOfWork.Users).Returns(repository);
        ////    var searchingService = new UserSearchingService(unitOfWork);
        ////    var g = Guid.NewGuid(); // new Guid();//("ca861232ed4211cebacd00aa0057b223");

        ////    // act
        ////    var userFounded = searchingService.SearchById(g);

        ////    // assert
        ////    Assert.AreEqual(null, userFounded);
        ////}

        ////[Test]
        ////public void SearchById_NeededUserExists_ShouldReturnUser3()
        ////{
        ////    // arrange           
        ////    var users = new List<User> { new User(), new User(), new User(), new User() };

        ////    var repository = Mock.Create<IRepository<User>>();
        ////    Mock.Arrange(() => repository.GetAll()).Returns(users);
        ////    var unitOfWork = Mock.Create<IUnitOfWork>();
        ////    Mock.Arrange(() => unitOfWork.Users).Returns(repository);
        ////    const int UserToSearchNumber = 1;
        ////    var searchingService = new UserSearchingService(unitOfWork);

        ////    // act
        ////    var userFounded = searchingService.SearchById(users[UserToSearchNumber].Id);

        ////    // assert
        ////    Assert.AreSame(users[UserToSearchNumber], userFounded);
        ////    Assert.AreEqual(users[UserToSearchNumber], userFounded);
        ////}

        ////[Test]
        ////public void SearchByLastName_NeededUserDontExists_ShouldReturnShouldReturnCountZeroIEnumarable()
        ////{
        ////    // arrange
        ////    const string NeededLName = "NeededLName";
        ////    const string NotNeededLName = "NotNeededLName";
        ////    var users = new List<User>
        ////    {
        ////        new User(null, NotNeededLName, null, null, null),
        ////        new User(null, NotNeededLName, null, null, null),
        ////        new User(null, NotNeededLName, null, null, null)
        ////    };
        ////    var repository = Mock.Create<IRepository<User>>();
        ////    Mock.Arrange(() => repository.GetAll()).Returns(users);
        ////    var unitOfWork = Mock.Create<IUnitOfWork>();
        ////    Mock.Arrange(() => unitOfWork.Users).Returns(repository);
        ////    var searchingService = new UserSearchingService(unitOfWork);

        ////    // act
        ////    var usersFounded = searchingService.SearchByFirstName(NeededLName);

        ////    // assert
        ////    Assert.AreEqual(usersFounded.Count(), 0);
        ////}

        ////[Test]
        ////public void SearchByLastName_NeededUserExists_ShouldReturnShouldReturnAllNeeded()
        ////{
        ////    // arrange
        ////    const string NeededLName = "NeededLName";
        ////    const string NotNeededLName = "NotNeededLName";
        ////    var users = new List<User>
        ////    {
        ////        new User(null, NotNeededLName, null, null, null),
        ////        new User(null, NeededLName, null, null, null),
        ////        new User(null, NeededLName, null, null, null)
        ////    };
        ////    var repository = Mock.Create<IRepository<User>>();
        ////    Mock.Arrange(() => repository.GetAll()).Returns(users);
        ////    var unitOfWork = Mock.Create<IUnitOfWork>();
        ////    Mock.Arrange(() => unitOfWork.Users).Returns(repository);
        ////    var searchingService = new UserSearchingService(unitOfWork);

        ////    // act
        ////    var usersFounded = searchingService.SearchByFirstName(NeededLName);

        ////    // assert
        ////    foreach (var user in usersFounded)
        ////    {
        ////        Assert.AreEqual(NeededLName, user.LastName);
        ////    }
        ////}

        ////[Test]
        ////public void SearchByName_NeededUserDontExists_ShouldReturnCountZeroIEnumarable()
        ////{
        ////    // arrange
        ////    const string NeededFName = "NeededFName";
        ////    const string NotNeededFName = "NotNeededFName";
        ////    const string NeededLName = "NeededLName";
        ////    const string NotNeededLName = "NotNeededLName";
        ////    var users = new List<User>
        ////    {
        ////        new User(NotNeededFName, NotNeededLName, null, null, null),
        ////        new User(NotNeededFName, NotNeededLName, null, null, null),
        ////        new User(NotNeededFName, NotNeededLName, null, null, null)
        ////    };
        ////    var repository = Mock.Create<IRepository<User>>();
        ////    Mock.Arrange(() => repository.GetAll()).Returns(users);
        ////    var unitOfWork = Mock.Create<IUnitOfWork>();
        ////    Mock.Arrange(() => unitOfWork.Users).Returns(repository);
        ////    var searchingService = new UserSearchingService(unitOfWork);

        ////    // act
        ////    var usersFounded = searchingService.SearchByName(NeededFName, NeededLName);

        ////    // assert
        ////    Assert.AreEqual(usersFounded.Count(), 0);
        ////}

        ////[Test]
        ////public void SearchByName_NeededUserExists_ShouldReturnAllNeeded()
        ////{
        ////    // arrange
        ////    const string NeededFName = "NeededFName";
        ////    const string NotNeededFName = "NotNeededFName";
        ////    const string NeededLName = "NeededLName";
        ////    const string NotNeededLName = "NotNeededLName";
        ////    var users = new List<User>
        ////    {
        ////        new User(NotNeededFName, NotNeededLName, null, null, null),
        ////        new User(NeededFName, NeededLName, null, null, null),
        ////        new User(NeededFName, NeededLName, null, null, null)
        ////    };
        ////    var repository = Mock.Create<IRepository<User>>();
        ////    Mock.Arrange(() => repository.GetAll()).Returns(users);
        ////    var unitOfWork = Mock.Create<IUnitOfWork>();
        ////    Mock.Arrange(() => unitOfWork.Users).Returns(repository);
        ////    var searchingService = new UserSearchingService(unitOfWork);

        ////    // act
        ////    var usersFounded = searchingService.SearchByName(NeededFName, NeededLName);

        ////    // assert
        ////    foreach (var user in usersFounded)
        ////    {
        ////        Assert.AreEqual(NeededFName, user.FirstName);
        ////        Assert.AreEqual(NeededLName, user.LastName);
        ////    }
        ////}
    }
}