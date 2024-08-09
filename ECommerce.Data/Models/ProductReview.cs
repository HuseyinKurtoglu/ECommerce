using System;

namespace ECommerce.DataAcces.Models
{
    public partial class ProductReview
    {
        public int ReviewId { get; set; }

        public int? ProductId { get; set; }

        public int? CustomerId { get; set; }

        public int? Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime? ReviewDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual Product? Product { get; set; }

        // Constructor to initialize CreatedDate and IsActive
        public ProductReview()
        {
            CreatedDate = DateTime.UtcNow;
            IsActive = true; // Varsayılan olarak aktif olarak ayarla
        }

        // Method to update UpdatedDate and UpdatedBy
        public void Update(int updatedBy)
        {
            UpdatedDate = DateTime.UtcNow;
            UpdatedBy = updatedBy;
            IsDeleted = false; // Güncelleme yapıldığında silinmiş olma durumunu sıfırla
        }

        // Method to delete ProductReview
        public void Delete(int deletedBy)
        {
            DeletedDate = DateTime.UtcNow;
            DeletedBy = deletedBy;
            IsDeleted = true;
            IsActive = false; // Silindiğinde aktiflik durumunu güncelle
        }
    }
}
