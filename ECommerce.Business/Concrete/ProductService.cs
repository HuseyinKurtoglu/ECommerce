using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _productRepository.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
               
                throw new Exception("Error retrieving products", ex);
            }
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            try
            {
                return await _productRepository.GetProductByIdAsync(productId);
            }
            catch (Exception ex)
            {
               
                throw new Exception($"Error retrieving product with ID {productId}", ex);
            }
        }

        public async Task<int> AddProductAsync(Product product)
        {
            try
            {
                return await _productRepository.AddProductAsync(product);
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error adding product", ex);
            }
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            try
            {
                return await _productRepository.UpdateProductAsync(product);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error updating product with ID {product.ProductId}", ex);
            }
        }

        public async Task<int> DeleteProductAsync(int productId, int deletedBy)
        {
            try
            {
                return await _productRepository.DeleteProductAsync(productId, deletedBy);
            }
            catch (Exception ex)
            {
               
                throw new Exception($"Error deleting product with ID {productId}", ex);
            }
        }
    }

}
