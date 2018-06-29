using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Contacts.DataLayer.Infrastructure;
using Contacts.DataLayer.Models;
using ContactsAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Contacts.Tests
{
    [TestClass]
    public class TestContactController
    {
        [TestMethod]
        public void GetByIdReutunsOk()
        {
            var mockRepository = new Mock<IContactsRepository>();
            mockRepository.Setup(x => x.Get(42))
                .Returns(new Contact { Id = 42 });

            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(42);
            var contentResult = actionResult as OkNegotiatedContentResult<Contact>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);

        }
        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IContactsRepository>();
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IContactsRepository>();
            var controller = new ContactController(mockRepository.Object);
            // Act
            IHttpActionResult actionResult = controller.Delete(10);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }
        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var mockRepository = new Mock<IContactsRepository>();
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new Contact
            {
                Id = 10,
                FirstName = "Jayesh",
                LastName = "Patel",
                Status = true,
                Email = "jayesh5.ce@gmail.com",
                PhoneNumber = "768888888"
            });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Contact>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(10, createdResult.RouteValues["id"]);
        }
        [TestMethod]
        public void PutReturnsContentResult()
        {
            // Arrange
            var mockRepository = new Mock<IContactsRepository>();
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(10, 
                new Contact { Id = 10,
                            FirstName = "Jayesh",
                            LastName = "Patel",
                            Status = true,
                            Email = "jayesh5.ce@gmail.com",
                            PhoneNumber = "768888888"
                });
            var contentResult = actionResult as NegotiatedContentResult<Contact>;
            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(10, contentResult.Content.Id);
        }
    }
}
