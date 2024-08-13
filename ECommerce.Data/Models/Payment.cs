using System;

namespace ECommerce.DataAcces.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }

        public int? OrderId { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow; // Varsayılan olarak PaymentDate'yi ayarla

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Varsayılan olarak CreatedDate'yi ayarla

        public int CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool IsDeleted { get; set; } = false; // Varsayılan olarak IsDeleted'yi ayarla

        public bool IsActive { get; set; } = true; // Varsayılan olarak IsActive'yi ayarla

        public virtual Order? Order { get; set; }

        public Payment()
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
