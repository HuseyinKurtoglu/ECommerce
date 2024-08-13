using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Net;
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

        // Tüm ürünleri asenkron olarak getirir.
        public async Task<ServiceResult<IEnumerable<Product>>> GetAllProductsAsync()
        {
            try
            {
                // Ürünleri veri erişim katmanından alır.
                var products = await _productRepository.GetAllProductsAsync();
                // Başarıyla getirildiğinde ServiceResult ile döner.
                // HTTP 200 (OK) durumu ile döner.
                return ServiceResult<IEnumerable<Product>>.SuccessResult(products, "Ürünler başarıyla getirildi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner.
                return ServiceResult<IEnumerable<Product>>.FailureResult($"Ürünleri getirirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Belirli bir ürünü asenkron olarak getirir.
        public async Task<ServiceResult<Product>> GetProductByIdAsync(int productId)
        {
            try
            {
                // Ürünü veri erişim katmanından alır.
                var product = await _productRepository.GetProductByIdAsync(productId);
                if (product == null)
                {
                    // Ürün bulunamadığında hata mesajı döner.
                    // HTTP 404 (Not Found) durumu ile döner.
                    return ServiceResult<Product>.FailureResult("Ürün bulunamadı.", HttpStatusCode.NotFound);
                }
                // Ürün başarıyla getirildiğinde ServiceResult ile döner.
                // HTTP 200 (OK) durumu ile döner.
                return ServiceResult<Product>.SuccessResult(product, "Ürün başarıyla getirildi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner.
                return ServiceResult<Product>.FailureResult($"Ürünü getirirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Yeni bir ürünü asenkron olarak ekler.
        public async Task<ServiceResult<int>> AddProductAsync(Product product)
        {
            try
            {
                // Ürünü veri erişim katmanına ekler ve yeni ürün ID'sini alır.
                var newProductId = await _productRepository.AddProductAsync(product);
                // Başarıyla eklendiğinde ServiceResult ile yeni ürün ID'si döner.
                // HTTP 201 (Created) durumu ile döner.
                return ServiceResult<int>.SuccessResult(newProductId, "Ürün başarıyla eklendi.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner.
                return ServiceResult<int>.FailureResult($"Ürün eklenirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Mevcut bir ürünü asenkron olarak günceller.
        public async Task<ServiceResult<int>> UpdateProductAsync(Product product)
        {
            try
            {
                // Ürünü veri erişim katmanında günceller ve güncellenmiş ürün ID'sini alır.
                var updatedProductId = await _productRepository.UpdateProductAsync(product);
                // Başarıyla güncellendiğinde ServiceResult ile güncellenmiş ürün ID'si döner.
                // HTTP 200 (OK) durumu ile döner.
                return ServiceResult<int>.SuccessResult(updatedProductId, "Ürün başarıyla güncellendi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner.
                
                return ServiceResult<int>.FailureResult($"Ürün güncellenirken bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        // Belirli bir ürünü asenkron olarak siler.
        public async Task<ServiceResult<int>> DeleteProductAsync(int productId, int deletedBy)
        {
            try
            {
                // Ürünü veri erişim katmanında siler ve silinmiş ürün ID'sini alır.
                var deletedProductId = await _productRepository.DeleteProductAsync(productId, deletedBy);
                // Başarıyla silindiğinde ServiceResult ile silinmiş ürün ID'si döner.
                // HTTP 200 (OK) durumu ile döner.
                return ServiceResult<int>.SuccessResult(deletedProductId, "Ürün başarıyla silindi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner.
               
                return ServiceResult<int>.FailureResult($"Ürün silinirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }
    }
}
