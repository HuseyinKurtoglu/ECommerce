﻿using System;
using System.Collections.Generic;

namespace ECommerce.DataAcces.Models
{
    public partial class OrderStatus
    {
        public int StatusId { get; set; }

        public string StatusDescription { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public OrderStatus()
        {
            CreatedDate = DateTime.UtcNow;
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
