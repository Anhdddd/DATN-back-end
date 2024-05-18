namespace DATN_back_end.Dtos.Company
{
    public class CompanyAddDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string TaxCode { get; set; }
        public string? Email { get; set; }
        public IFormFile? Logo { get; set; }
        public IFormFile? BackgroundImage { get; set; }

    }
}
