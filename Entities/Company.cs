using DATN_back_end.Common;
using DATN_back_end.Entities;
using System.Data.Common;

namespace DATN_back_end.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? Logo { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public CompanySize CompanySize { get; set; }
        public int ViewCount { get; set; }
        public CompanyStatus Status { get; set; }
        public User? Owner { get; set; }
        public Guid? OwnerId { get; set; }
        public string? TaxCode { get; set; }
        public string? BackgroundImage { get; set; }
    }
}
