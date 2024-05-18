using DATN_back_end.Common;

namespace DATN_back_end.Dtos.Company
{
    public class CompanyDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public CompanySize CompanySize { get; set; }
        public int ViewCount { get; set; }
        public CompanyStatus Status { get; set; }
        public string TaxCode { get; set; }
    }
}
