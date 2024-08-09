using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // Constructor
        public User()
        {
            CreatedDate = DateTime.UtcNow; // Otomatik olarak oluşturma tarihi ayarlama
            UpdatedDate = DateTime.UtcNow; // İlk güncelleme tarihi ayarlama
        }

        // Method to update UpdatedDate
        public void Update()
        {
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
