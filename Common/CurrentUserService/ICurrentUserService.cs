namespace DATN_back_end.Common.CurrentUserService
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }
        public Role Role { get; }
    }
}
