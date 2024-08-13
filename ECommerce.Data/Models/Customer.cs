using System;
using System.Collections.Generic;

namespace ECommerce.DataAcces.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string PostalCode { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Varsayılan olarak CreatedDate'yi ayarla

        public int CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool IsDeleted { get; set; } = false; // Varsayılan olarak IsDeleted'yi ayarla

        public bool IsActive { get; set; } = true; // Varsayılan olarak IsActive'yi ayarla

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

        
        public Customer()
        {
            CreatedDate = DateTime.UtcNow;
            IsActive = true; // Varsayılan olarak aktif olarak ayarla
        }

        
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
