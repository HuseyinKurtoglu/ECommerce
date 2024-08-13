using ECommerce.DataAcces.Models;

public partial class Shipper
{
    public int ShipperId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime? CreatedDate { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
    public int? DeletedBy { get; set; }
    public bool? IsDeleted { get; set; }
    public bool? IsActive { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    // Parametresiz varsayılan yapıcı
    public Shipper() { }

    // Diğer constructor ve metodlar
    public Shipper(int createdBy) : this()
    {
        CreatedDate = DateTime.UtcNow;
        CreatedBy = createdBy;
        IsActive = true;
    }

    public void Update(int updatedBy)
    {
        UpdatedDate = DateTime.UtcNow;
        UpdatedBy = updatedBy;
        IsDeleted = false;
    }

    public void Delete(int deletedBy)
    {
        DeletedDate = DateTime.UtcNow;
        DeletedBy = deletedBy;
        IsDeleted = true;
        IsActive = false;
    }
}
