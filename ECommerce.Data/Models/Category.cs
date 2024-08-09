using System;
using System.Collections.Generic;

namespace ECommerce.DataAcces.Models
{
    public partial class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        // Constructor to set CreatedDate and CreatedBy
        public Category()
        {
            CreatedDate = DateTime.UtcNow;
            IsActive = true; // Varsayılan olarak aktif olarak ayarla
        }

        // Method to update UpdatedDate and UpdatedBy
        public void Update(int updatedBy)
        {
            UpdatedDate = DateTime.UtcNow;
            UpdatedBy = updatedBy;
            IsDeleted = false; // Güncelleme işlemi yapıldığında silinmiş olma durumunu sıfırla
        }

        // Method to delete Category
        public void Delete(int deletedBy)
        {
            DeletedDate = DateTime.UtcNow;
            DeletedBy = deletedBy;
            IsDeleted = true;
            IsActive = false; // Silindiğinde aktiflik durumunu güncelle
        }
    }
}
