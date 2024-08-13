using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;

public class ProductReviewRepository : IProductReviewRepository
{
    private readonly IDbConnection _dbConnection;

    // Repository sınıfının yapıcı metodu, bağımlılık enjeksiyonunu sağlar
    public ProductReviewRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // Tüm ürün incelemelerini asenkron olarak getirir
    public async Task<IEnumerable<ProductReview>> GetAllProductReviewsAsync()
    {
        var sql = "SELECT * FROM ProductReviews WHERE IsDeleted = 0";
        // SQL sorgusunu çalıştırır ve sonuçları alır
        var result = await _dbConnection.QueryAsync<ProductReview>(sql);
        // Sonuçları listeye çevirir ve döner
        return result.ToList();
    }

    // Belirli bir ürün incelemesini ID'ye göre asenkron olarak getirir
    public async Task<ProductReview> GetProductReviewByIdAsync(int reviewId)
    {
        var sql = "SELECT * FROM ProductReviews WHERE ReviewId = @ReviewId AND IsDeleted = 0";
        // Belirtilen ID'ye sahip ürünü veri tabanından alır
        var result = await _dbConnection.QueryFirstOrDefaultAsync<ProductReview>(sql, new { ReviewId = reviewId });
        // Sonucu döner, ürün bulunamazsa null döner
        return result;
    }

    // Yeni bir ürün incelemesini asenkron olarak ekler ve eklenen incelemenin ID'sini döner
    public async Task<int> AddProductReviewAsync(ProductReview productReview)
    {
        var sql = @"
            INSERT INTO ProductReviews (ProductId, CustomerId, Rating, Comment, ReviewDate, CreatedDate, CreatedBy, IsDeleted, IsActive)
            VALUES (@ProductId, @CustomerId, @Rating, @Comment, @ReviewDate, @CreatedDate, @CreatedBy, 0, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int);";
        // Yeni ürünü veri tabanına ekler ve eklenen incelemenin ID'sini döner
        var result = await _dbConnection.QuerySingleAsync<int>(sql, new
        {
            productReview.ProductId,
            productReview.CustomerId,
            productReview.Rating,
            productReview.Comment,
            productReview.ReviewDate,
            productReview.CreatedDate,
            productReview.CreatedBy,
            productReview.IsActive
        });
        return result;
    }

    // Mevcut bir ürün incelemesini asenkron olarak günceller
    public async Task<int> UpdateProductReviewAsync(ProductReview productReview)
    {
        var sql = @"
            UPDATE ProductReviews
            SET ProductId = @ProductId,
                CustomerId = @CustomerId,
                Rating = @Rating,
                Comment = @Comment,
                ReviewDate = @ReviewDate,
                UpdatedDate = @UpdatedDate,
                UpdatedBy = @UpdatedBy,
                IsActive = @IsActive
            WHERE ReviewId = @ReviewId AND IsDeleted = 0;";
        // Belirtilen ürün incelemesini günceller ve etkilenen satır sayısını döner
        var result = await _dbConnection.ExecuteAsync(sql, new
        {
            productReview.ProductId,
            productReview.CustomerId,
            productReview.Rating,
            productReview.Comment,
            productReview.ReviewDate,
            productReview.UpdatedDate,
            productReview.UpdatedBy,
            productReview.IsActive,
            productReview.ReviewId
        });
        return result;
    }

    // Bir ürün incelemesini asenkron olarak siler (yumuşak silme)
    public async Task<int> DeleteProductReviewAsync(int reviewId, int deletedBy)
    {
        var sql = @"
            UPDATE ProductReviews
            SET IsDeleted = 1,
                DeletedDate = @DeletedDate,
                DeletedBy = @DeletedBy
            WHERE ReviewId = @ReviewId;";
        // Belirtilen ürün incelemesini siler ve etkilenen satır sayısını döner
        var result = await _dbConnection.ExecuteAsync(sql, new
        {
            DeletedDate = DateTime.UtcNow,
            DeletedBy = deletedBy,
            ReviewId = reviewId
        });
        return result;
    }
}
