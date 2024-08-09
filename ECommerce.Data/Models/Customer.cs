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

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

        // Constructor to initialize CreatedDate and IsActive
        public Customer()
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

        // Method to delete Customer
        public void Delete(int deletedBy)
        {
            DeletedDate = DateTime.UtcNow;
            DeletedBy = deletedBy;
            IsDeleted = true;
            IsActive = false; // Silindiğinde aktiflik durumunu güncelle
        }
    }
}
