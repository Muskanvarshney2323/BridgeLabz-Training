using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FundooNotesApp.RepositoryLayer.Context;
using FundooNotesApp.RepositoryLayer.Service;
using FundooNotesApp.BusinessLayer.Service;

namespace FundooNotesApp.Tests
{
    [TestClass]
    public class LabelTests
    {
        private AppDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [TestMethod]
        public void Label_ShouldNotBeNull()
        {
            var context = GetInMemoryContext();

            Assert.IsNotNull(context);
        }
    }
}