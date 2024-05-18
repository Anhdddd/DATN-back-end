using DATN_back_end.Entities;

namespace DATN_back_end.Entities
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
