using System;
using System.Collections.Generic;

namespace ECommerce.DataAcces.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int? ShipperId { get; set; }

        public int? StatusId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public virtual Shipper? Shipper { get; set; }

        public virtual OrderStatus? Status { get; set; }

      
        public Order()
        {
            CreatedDate = DateTime.UtcNow;
            OrderDate = DateTime.UtcNow;
            IsActive = true;
            IsDeleted = false;
        }

        
        public void Update(int updatedBy)
        {
            UpdatedDate = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }

      
        public void Delete(int deletedBy)
        {
            DeletedDate = DateTime.UtcNow;
            DeletedBy = deletedBy;
            IsDeleted = true;
            IsActive = false;
        }
    }
}
