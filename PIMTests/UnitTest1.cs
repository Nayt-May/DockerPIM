using LincolnAPI.Products;
using Microsoft.AspNetCore.Mvc;

namespace PIMTests
{
    public class Tests
    {

        private ProductsController _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ProductsController();
        }

        [Test]
        public void Test1()
        {
            var result = _sut.getAll();
            var resultOkObject = result as OkObjectResult;
            Assert.IsNotNull(resultOkObject);
            Assert.AreEqual("Productstesting", resultOkObject.Value);
            Assert.AreEqual(200, resultOkObject.StatusCode);
        }
    }
}