using DATN_back_end.Common;

namespace DATN_back_end.Dtos.Company
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string TaxCode { get; set; }
        public CompanyStatus Status { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string PhoneNumber { get; set; }
    }
}
