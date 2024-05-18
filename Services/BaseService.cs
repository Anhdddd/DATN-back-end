using AutoMapper;
using DATN_back_end.Common.CurrentUserService;
using DATN_back_end.Common.FileService;
using DATN_back_end.Data.UnitOfWork;

namespace DATN_back_end.Services
{
    public class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly ICurrentUserService _currentUserService;
        protected readonly IConfiguration _configuration;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IFileService _fileService;

        public BaseService(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetService<IMapper>();
            _currentUserService = serviceProvider.GetService<ICurrentUserService>();
            _configuration = serviceProvider.GetService<IConfiguration>();
            _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            _fileService = serviceProvider.GetService<IFileService>();
        }
    }
}
