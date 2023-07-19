using LincolnAPI.Products;
using Microsoft.AspNetCore.Mvc;

namespace APITest
{
    
    public class BasicTests
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
            Assert.That(resultOkObject.Value, Is.EqualTo("Productstesting"));
            Assert.That(resultOkObject.StatusCode, Is.EqualTo(200));
        }
    }
}