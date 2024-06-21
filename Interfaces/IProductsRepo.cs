using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Interfaces
{
    public interface IProductsRepo
    {
        public object AddProducts(ProductsModel productsModel);
        public List<ProductsEntity> GetByProductName(string productName);
    }
}
