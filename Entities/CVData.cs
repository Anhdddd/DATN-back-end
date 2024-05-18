using DATN_back_end.Entities;

namespace DATN_back_end.Entities
{
    public class CVData : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string Education { get; set; }
    }
}
