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

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow; // Varsayılan olarak ReviewDate'yi ayarla

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Varsayılan olarak CreatedDate'yi ayarla

        public int CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool IsDeleted { get; set; } = false; // Varsayılan olarak IsDeleted'yi ayarla

        public bool IsActive { get; set; } = true; // Varsayılan olarak IsActive'yi ayarla

        public virtual Customer? Customer { get; set; }

        public virtual Product? Product { get; set; }

        public void Update(int updatedBy)
        {
            UpdatedDate = DateTime.UtcNow;
            UpdatedBy = updatedBy;
            IsDeleted = false; // Güncelleme yapıldığında silinmiş olma durumunu sıfırla
        }

        public void Delete(int deletedBy)
        {
            DeletedDate = DateTime.UtcNow;
            DeletedBy = deletedBy;
            IsDeleted = true;
            IsActive = false; // Silindiğinde aktiflik durumunu güncelle
        }
    }
}
