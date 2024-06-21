using Automatonymous.Contexts;
using BussinessLayer.Interfaces;
using MassTransit.Testing.Indicators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryBussiness inventoryBussiness;
        public InventoryController(IInventoryBussiness inventoryBussiness)
        {
            this.inventoryBussiness = inventoryBussiness;
        }

        [HttpPost]
        [Route("AddStock")]
        public IActionResult AddStocks(InventoryStockModel model)
        {
            var Record=inventoryBussiness.AddProducts(model);
            if(Record!=null)
            {
                return Ok(new { status = true, msg = "Stock Successfully Added", data = Record });
            }
            else
            {
                return BadRequest(new { status = false, msg = "Stock Not Added" });
            }
        }
        [HttpGet]
        [Route("GetAllProductsStock")]
        public IActionResult GetAllProducts()
        {
            var AllProducts= inventoryBussiness.GetAllStocks();
            if(AllProducts!=null)
            {
                return Ok(new { status=true,msg="Available Products Stock",data=AllProducts });
            }
            else
            {
                return BadRequest(new { status = false, msg = "No Products Available" });
            }
        }
        [HttpPut]
        [Route("UpdateProductsStock")]
        public IActionResult UpdateProductsStock(StockUpdateModel model)
        {
            var Updated=inventoryBussiness.UpdateStocks(model);
            if (Updated!=null)
            {
                return Ok(new { status = true, msg = "Successfully Product Stock UPdated", data = Updated });
            }
            else
            {
                return BadRequest(new { status = false, msg = "Not Able to Update,ProductId Not Exists" });
            }
        }
        [HttpDelete]
        [Route("DeleteProduct")]
        public IActionResult DeleteProductById(long ProductId)
        {
            var Deleted=inventoryBussiness.DeleteProductById(ProductId);
            if (Deleted != null)
            {
                return Ok(new { status = true, msg = "Deleted Successfully", data = Deleted });
            }
            else
                return BadRequest(new { status = false, msg = "ProductId Not Exists" });
        }
        [HttpPut]
        [Route("UpdateOnlyStockOnly")]
        public IActionResult UpdateOnlyStock(OnlyStockModel model)
        {
            var Updated = inventoryBussiness.UpdateOnlyStock(model);
            if (Updated != null)
            {
                return Ok(new { status = true, msg = "Successfully Product Stock UPdated", data = Updated });
            }
            else
            {
                return BadRequest(new { status = false, msg = "Not Able to Update,ProductId Not Exists" });
            }
        }
        [HttpPut]
        [Route("UpdateWarehouseId")]
        public IActionResult UpdateOnlyWarehouse(OnlyWarehouseModel model)
        {
            var Updated = inventoryBussiness.UpdateOnlyWarehouseId(model);
            if (Updated != null)
            {
                return Ok(new { status = true, msg = "Successfully Product Stock UPdated", data = Updated });
            }
            else
            {
                return BadRequest(new { status = false, msg = "Not Able to Update,ProductId Not Exists" });
            }
        }
    }
}
