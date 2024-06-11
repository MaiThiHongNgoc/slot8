using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBizApplication.model;
using MyBizApplication.service;

namespace MyBizApplication.Controller
{
    public class ProductController
    {
        private IProductRespository productService;
        public ProductController(IProductRespository productService){
            this.productService=productService;
        }
        public void AddProduct(Product product){
            productService.AddProduct(product);
        }
        public List<Product> GetAllProducts(){
            return productService.GetAllProducts();
    }
    }
}