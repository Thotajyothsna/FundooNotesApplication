using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entities;

namespace BussinessLayer.Interfaces
{
    public interface IInventoryBussiness
    {
        public InventoryEntity AddProducts(InventoryStockModel stock);
        public List<InventoryEntity> GetAllStocks();
        public InventoryEntity UpdateStocks(StockUpdateModel model);
        public InventoryEntity DeleteProductById(long ProductId);
        public InventoryEntity UpdateOnlyStock(OnlyStockModel model);
        public InventoryEntity UpdateOnlyWarehouseId(OnlyWarehouseModel model);
    }
}
