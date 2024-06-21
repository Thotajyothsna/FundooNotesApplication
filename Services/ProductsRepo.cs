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
    public class ProductsRepo:IProductsRepo
    {
        private readonly FundooNotesContext fundooNotesContext;
        private readonly IConfiguration configuration;
        public ProductsRepo(FundooNotesContext fundooNotesContext,IConfiguration configuration)
        {
            this.fundooNotesContext = fundooNotesContext;
            this.configuration = configuration;
        }

        public object AddProducts(ProductsModel productsModel) 
        {
            try
            {
                ProductsEntity productsEntity = new ProductsEntity();
                productsEntity.ProductName = productsModel.ProductName;
                productsEntity.BrandName = productsModel.BrandName;

                fundooNotesContext.Products.Add(productsEntity);
                fundooNotesContext.SaveChanges();
                return productsEntity;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public List<ProductsEntity> GetByProductName(string productName)
        {
            var products = fundooNotesContext.Products.Where(x=>x.ProductName==productName).ToList();
            if (products != null)
            {
                return products;
            }
            else
                return null;
        }
    }
}
