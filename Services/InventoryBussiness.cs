using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Interfaces;
using ModelLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace BussinessLayer.Services
{
    public class InventoryBussiness:IInventoryBussiness
    {
        private readonly IInventoryRepo inventoryRepo;
        public InventoryBussiness(IInventoryRepo inventoryRepo)
        {
            this.inventoryRepo = inventoryRepo;
        }
        public InventoryEntity AddProducts(InventoryStockModel stock)
        {
            return inventoryRepo.AddProducts(stock);
        }
        public List<InventoryEntity> GetAllStocks()
        {
            return inventoryRepo.GetAllStocks();
        }
        public InventoryEntity UpdateStocks(StockUpdateModel model)
        {
            return inventoryRepo.UpdateStocks(model);
        }
        public InventoryEntity DeleteProductById(long ProductId)
        {
            return inventoryRepo.DeleteProductById(ProductId);
        }
        public InventoryEntity UpdateOnlyStock(OnlyStockModel model)
        {
            return inventoryRepo.UpdateOnlyStock(model);
        }
        public InventoryEntity UpdateOnlyWarehouseId(OnlyWarehouseModel model)
        {
            return inventoryRepo.UpdateOnlyWarehouseId(model);
        }
    }
}
