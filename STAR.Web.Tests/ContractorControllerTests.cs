using Moq;
using NUnit.Framework;
using STAR.Data;
using STAR.Domain;
using STAR.Web.Controllers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace STAR.Web.Tests {
    [TestFixture]
    public class ContractorControllerTests {
        [Test]
        public void NoContractors() {
            var contractors = new List<Contractor>().AsQueryable();

            var mockContractors = new Mock<DbSet<Contractor>>();
            mockContractors.As<IQueryable<Contractor>>().Setup(x => x.Provider).Returns(contractors.Provider);
            mockContractors.As<IQueryable<Contractor>>().Setup(x => x.Expression).Returns(contractors.Expression);
            mockContractors.As<IQueryable<Contractor>>().Setup(x => x.ElementType).Returns(contractors.ElementType);
            mockContractors.As<IQueryable<Contractor>>().Setup(x => x.GetEnumerator()).Returns(contractors.GetEnumerator());

            var mockContext = new Mock<StarContext>();
            mockContext.Setup(x => x.Contractors).Returns(mockContractors.Object);
            var c = new ContractorController(mockContext.Object);
            var view = c.Index() as ViewResult;
            var model = view.Model as List<Contractor>;

            Assert.That(model, Has.Count.EqualTo(0));
        }
    }
}
