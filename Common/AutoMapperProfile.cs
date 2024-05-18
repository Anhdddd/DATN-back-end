using AutoMapper;
using DATN_back_end.Dtos.Company;
using DATN_back_end.Dtos.JobPosting;
using DATN_back_end.Dtos.User;
using DATN_back_end.Dtos.UserJobPosting;
using DATN_back_end.Entities;

namespace DATN_back_end.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Company, CompanyDetailDto>();
            CreateMap<Company, CompanyDto>();
            CreateMap<JobPosting, JobPostingDetailDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.CompanyLogo, opt => opt.MapFrom(src => src.Company.Logo));
            CreateMap<JobPosting, JobPostingDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.CompanyLogo, opt => opt.MapFrom(src => src.Company.Logo));
            CreateMap<UserJobPosting, UserJobPostingDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.JobPosting.Title))
                .ForMember(dest => dest.SubmittedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CV, opt => opt.MapFrom(src => src.CV))
                .ForMember(dest => dest.SalaryRange, opt => opt.MapFrom(src => src.JobPosting.SalaryRange));
            CreateMap<User, UserDetailDto>();
        }
    }
}
