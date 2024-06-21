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
    public class ProductsBussiness:IProductsBussiness
    {
        private readonly IProductsRepo iProductsRepo;
        public ProductsBussiness(IProductsRepo iproductsRepo)
        {
            this.iProductsRepo = iproductsRepo;
        }
        public object AddProducts(ProductsModel productsModel)
        { 
            return iProductsRepo.AddProducts(productsModel);
        }
        public List<ProductsEntity> GetByProductName(string productName)
        { 
            return iProductsRepo.GetByProductName(productName);
        }
    }
}
