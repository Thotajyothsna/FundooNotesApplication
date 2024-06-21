using BussinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsBussiness iProductsBussiness;
        public ProductsController(IProductsBussiness iproductsBussiness)
        {
            this.iProductsBussiness = iproductsBussiness;
        }
        [HttpPost]
        [Route("AddProducts")]
        public IActionResult AddProducts(ProductsModel productsModel)
        {
            var products = iProductsBussiness.AddProducts(productsModel);
            if (products != null)
            {
                return Ok(new { status = true, msg = "Products Added Successfully", data = products });
            }
            else {
                return BadRequest(new { status = false, msg = "Products not added" });
            }
        }
        [HttpGet]
        [Route("GetByProductName / {ProductsName}")]
        public IActionResult GetProducts(string ProductName) 
        {
            var products=iProductsBussiness.GetByProductName(ProductName);
            if (products != null)
            {
                return Ok(new { status = true, msg = "Available Products with ProductName", data = products });
            }
            else
                return BadRequest(new { status = false, msg = "Sorry,Current There were no products available with your preffered productName" });
        }
    }
}
