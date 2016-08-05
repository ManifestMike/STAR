using Moq;
using NUnit.Framework;
using STAR.Data;
using STAR.Domain;
using STAR.Web.Controllers;
using STAR.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace STAR.Web.Tests {
    [TestFixture]
    public class ContractorControllerTests {
        [Test]
        [Ignore("Must mock out .Include method")]
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
            var view = c.Index(null) as ViewResult;
            var model = view.Model as List<Contractor>;

            Assert.That(model, Has.Count.EqualTo(0));
        }
        [Test]
        public void PostBlankFirstNameYeildsInvalid() {
            var mockContext = new Mock<StarContext>();
            var controller = new ContractorController(mockContext.Object);
            var contractor = new PostContractorViewModel();
            contractor.Id = 1;
            contractor.LastName = "Judd";
            contractor.SkillIds = "1,2,3";

            controller.ModelState.Clear();
            var validationContext = new ValidationContext(contractor, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(contractor, validationContext, validationResults, true);
            foreach (var v in validationResults) {
                foreach (var name in v.MemberNames) {
                    controller.ModelState.AddModelError(name, v.ErrorMessage);
                }
            }

            var result = controller.Details(contractor) as ViewResult;
            Assert.False(result.ViewData.ModelState.IsValid);
        }
    }
}
