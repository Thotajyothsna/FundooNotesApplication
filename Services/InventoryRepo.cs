using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class InventoryRepo : IInventoryRepo
    {
        private readonly FundooNotesContext fundooNotesContext;
        private readonly IConfiguration configuration;
        public InventoryRepo(FundooNotesContext fundooNotesContext, IConfiguration configuration)
        {
            this.fundooNotesContext = fundooNotesContext;
            this.configuration = configuration;
        }
        public InventoryEntity AddProducts(InventoryStockModel stock)
        {
            try
            {
                InventoryEntity inventoryEntity = new InventoryEntity();
                inventoryEntity.WarehouseId = stock.WarehouseId;
                inventoryEntity.Quantity_In_Stock = stock.StockQuantity;
                inventoryEntity.Last_Updated = DateTime.Now;

                fundooNotesContext.InventoryTable.Add(inventoryEntity);
                fundooNotesContext.SaveChanges();

                return inventoryEntity;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public List<InventoryEntity> GetAllStocks()
        {
            try
            {
                List<InventoryEntity> inventoryEntity = fundooNotesContext.InventoryTable.ToList();
                if(inventoryEntity.Count > 0)
                {
                    return inventoryEntity;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public InventoryEntity UpdateStocks(StockUpdateModel model)
        {
            try
            {
                var recordExixts = fundooNotesContext.InventoryTable.FirstOrDefault(x => x.ProductId == model.ProductId);
                if (recordExixts != null)
                {
                    recordExixts.Quantity_In_Stock = model.Current_Stock;
                    recordExixts.WarehouseId = model.WarehouseId;
                    recordExixts.Last_Updated = DateTime.Now;

                    fundooNotesContext.InventoryTable.Update(recordExixts);
                    fundooNotesContext.SaveChanges();

                    return recordExixts;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public InventoryEntity DeleteProductById(long ProductId)
        {
            try
            {
                var IdExists = fundooNotesContext.InventoryTable.FirstOrDefault(x => x.ProductId == ProductId);
                if (IdExists != null)
                {
                    var Deleted = IdExists;
                    fundooNotesContext.InventoryTable.Remove(IdExists);
                    fundooNotesContext.SaveChanges();

                    return Deleted;

                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public InventoryEntity UpdateOnlyStock(OnlyStockModel model)
        {
            try
            {
                var recordExixts = fundooNotesContext.InventoryTable.FirstOrDefault(x => x.ProductId == model.ProductId);
                if (recordExixts != null)
                {
                    recordExixts.Quantity_In_Stock = model.CurrentStock;
            
                    recordExixts.Last_Updated = DateTime.Now;

                    fundooNotesContext.InventoryTable.Update(recordExixts);
                    fundooNotesContext.SaveChanges();

                    return recordExixts;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public InventoryEntity UpdateOnlyWarehouseId(OnlyWarehouseModel model)
        {
            try
            {
                var recordExixts = fundooNotesContext.InventoryTable.FirstOrDefault(x => x.ProductId == model.ProductId);
                if (recordExixts != null)
                {
                    //recordExixts.Quantity_In_Stock = model.Current_Stock;
                    recordExixts.WarehouseId = model.WarehouseId;
                    recordExixts.Last_Updated = DateTime.Now;

                    fundooNotesContext.InventoryTable.Update(recordExixts);
                    fundooNotesContext.SaveChanges();

                    return recordExixts;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
