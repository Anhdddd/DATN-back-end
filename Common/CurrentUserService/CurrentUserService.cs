using System.Security.Claims;

namespace DATN_back_end.Common.CurrentUserService
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId => Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId"));
        public Role Role => (Role)Enum.Parse(typeof(Role), _httpContextAccessor.HttpContext?.User?.FindFirstValue("Role"));

    }
}
