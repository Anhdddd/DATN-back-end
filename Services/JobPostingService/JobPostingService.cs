using DATN_back_end.Common;
using DATN_back_end.Dtos;
using DATN_back_end.Dtos.Company;
using DATN_back_end.Dtos.JobPosting;
using DATN_back_end.Dtos.UserJobPosting;
using DATN_back_end.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace DATN_back_end.Services.JobPostingService
{
    public class JobPostingService : BaseService, IJobPostingService
    {
        public JobPostingService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<CustomResponse<JobPostingDetailDto>> AddAsync(JobPostingAddDto jobPostingDto)
        {
            var jobPosting = new JobPosting
            {
                Title = jobPostingDto.Title,
                ExpiryDate = jobPostingDto.ExpiryDate,
                Occupation = new Occupation
                {
                    Name = jobPostingDto.Occupation
                },
                SalaryRange = jobPostingDto.SalaryRange,
                Description = jobPostingDto.Description,
                Requirement = jobPostingDto.Requirement,
                Benefit = jobPostingDto.Benefit,
                WorkingTime = jobPostingDto.WorkingTime,
                EmployeeType = jobPostingDto.EmployeeType,
                ProvinceOrCity = await (await _unitOfWork.Queryable<ProvinceOrCity>()).FirstOrDefaultAsync(x => x.Id == jobPostingDto.ProvinceOrCity),
                HiringQuota = jobPostingDto.HiringQuota,
                Experience = jobPostingDto.Experience,
                Location = jobPostingDto.Location,
                JobPostingStatus = Common.JobPostingStatus.Pending,
                CompanyId = await (await _unitOfWork.Queryable<Company>()).Where(x => x.OwnerId == _currentUserService.UserId).Select(x => x.Id).FirstOrDefaultAsync()
            };

            await _unitOfWork.AddAsync(jobPosting);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<JobPostingDetailDto>
            {
                Data = _mapper.Map<JobPostingDetailDto>(jobPosting)
            };
        }

        public async Task<CustomResponse<List<JobPostingDto>>> GetJobPostingsAsync(FilterJobPostingDto? filterJobPostingDto, int pageSize, int pageNumber)
        {
            var jobPostingQuery = await _unitOfWork.Queryable<JobPosting>();
            jobPostingQuery = jobPostingQuery.Include(x => x.Company);

            if (!string.IsNullOrEmpty(filterJobPostingDto.SearchValue))
            {
                jobPostingQuery = jobPostingQuery.Where(x => x.Title.Contains(filterJobPostingDto.SearchValue));
            }
            if (filterJobPostingDto.SortType.HasValue)
            {
                switch (filterJobPostingDto.SortType)
                {
                    case JobPostingSortType.ViewCount:
                        jobPostingQuery = jobPostingQuery.OrderByDescending(x => x.ViewCount);
                        break;
                    default:
                        jobPostingQuery = jobPostingQuery.OrderByDescending(x => x.CreatedDate);
                        break;
                }
            }

            if (filterJobPostingDto.CompanyId.HasValue)
            {
                jobPostingQuery = jobPostingQuery.Where(x => x.CompanyId == filterJobPostingDto.CompanyId);
            }

            int totalItems = await jobPostingQuery.CountAsync();
            var pagedJobPostings = await jobPostingQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => _mapper.Map<JobPostingDto>(r))
                .ToListAsync();
            return new CustomResponse<List<JobPostingDto>>
            {
                Data = pagedJobPostings,
                Meta = new MetaData
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
                }
            };
        }

        public async Task<CustomResponse<JobPostingDetailDto>> GetJobPostingByIdAsync(Guid jobPostingId)
        {
            var jobPosting = await (await _unitOfWork.Queryable<JobPosting>())
                .Include(x => x.Company)
                .FirstOrDefaultAsync(x => x.Id == jobPostingId);
            if (jobPosting == null)
            {
                throw new NotFoundException();
            }

            return new CustomResponse<JobPostingDetailDto>
            {
                Data = _mapper.Map<JobPostingDetailDto>(jobPosting)
            };
        }

        public async Task<CustomResponse<JobPostingDetailDto>> UpdateAsync(Guid id, JobPostingUpdateDto jobPostingDto)
        {
            var jobPosting = await (await _unitOfWork.Queryable<JobPosting>())
                .FirstOrDefaultAsync(x => x.Id == id);
            if (jobPosting == null)
            {
                throw new NotFoundException();
            }
            
            if (jobPostingDto.Title != null)
            {
                jobPosting.Title = jobPostingDto.Title;
            }
            if (jobPostingDto.ExpiryDate != null)
            {
                jobPosting.ExpiryDate = jobPostingDto.ExpiryDate.Value;
            }
            if (jobPostingDto.Occupation != null)
            {
                jobPosting.Occupation = jobPostingDto.Occupation;
            }
            if (jobPostingDto.SalaryRange != null)
            {
                jobPosting.SalaryRange = jobPostingDto.SalaryRange;
            }
            if (jobPostingDto.Description != null)
            {
                jobPosting.Description = jobPostingDto.Description;
            }
            if (jobPostingDto.Requirement != null)
            {
                jobPosting.Requirement = jobPostingDto.Requirement;
            }
            if (jobPostingDto.Benefit != null)
            {
                jobPosting.Benefit = jobPostingDto.Benefit;
            }
            if (jobPostingDto.WorkingTime != null)
            {
                jobPosting.WorkingTime = jobPostingDto.WorkingTime;
            }
            if (jobPostingDto.EmployeeType != null)
            {
                jobPosting.EmployeeType = jobPostingDto.EmployeeType.Value;
            }
            if (jobPostingDto.ProvinceOrCity != null)
            {
                jobPosting.ProvinceOrCity.Id = jobPostingDto.ProvinceOrCity.Value;
            }
            if (jobPostingDto.HiringQuota != null)
            {
                jobPosting.HiringQuota = jobPostingDto.HiringQuota.Value;
            }
            if (jobPostingDto.Experience != null)
            {
                jobPosting.Experience = jobPostingDto.Experience.Value;
            }
            if (jobPostingDto.Location != null)
            {
                jobPosting.Location = jobPostingDto.Location;
            }
            if (jobPostingDto.JobPostingStatus != null)
            {
                jobPosting.JobPostingStatus = jobPostingDto.JobPostingStatus.Value;
            }

            await _unitOfWork.UpdateAsync(jobPosting);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<JobPostingDetailDto>
            {
                Data = _mapper.Map<JobPostingDetailDto>(jobPosting)
            };
        }

        public async Task<CustomResponse<object>> SaveJobPosting(Guid jobPostingId)
        {
            var userSavedJobPosting = new UserSavedJobPosting
            {
                UserId = _currentUserService.UserId,
                JobPostingId = jobPostingId
            };
            await _unitOfWork.AddAsync(userSavedJobPosting);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<object>();
        }

        public async Task<CustomResponse<object>> UnSaveJobPosting(Guid jobPostingId)
        {
            var userSavedJobPosting = await (await _unitOfWork.Queryable<UserSavedJobPosting>())
                .FirstOrDefaultAsync(x => x.UserId == _currentUserService.UserId && x.JobPostingId == jobPostingId);
            if (userSavedJobPosting == null)
            {
                throw new NotFoundException();
            }
            await _unitOfWork.DeleteAsync(userSavedJobPosting, isHardDeleted : true);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<object>();
        }

        public async Task<CustomResponse<List<JobPostingDto>>> GetSavedJobPostingsAsync(int pageSize, int pageNumber)
        {
            var savedJobPostingQuery = await _unitOfWork.Queryable<UserSavedJobPosting>();
            savedJobPostingQuery = savedJobPostingQuery.Include(x => x.JobPosting).ThenInclude(x => x.Company);

            int totalItems = await savedJobPostingQuery.CountAsync();
            var pagedSavedJobPostings = await savedJobPostingQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => _mapper.Map<JobPostingDto>(r.JobPosting))
                .ToListAsync();

            return new CustomResponse<List<JobPostingDto>>
            {
                Data = pagedSavedJobPostings,
                Meta = new MetaData
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
                }
            };
        }

        public async Task<CustomResponse<object>> IncreaseViewCount(Guid jobPostingId)
        {
            var jobPosting = await (await _unitOfWork.Queryable<JobPosting>())
                .FirstOrDefaultAsync(x => x.Id == jobPostingId);
            if (jobPosting == null)
            {
                throw new NotFoundException();
            }
            jobPosting.ViewCount++;

            await _unitOfWork.UpdateAsync(jobPosting);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<object>();
        }

        public async Task<CustomResponse<JobPostingDetailDto>> UpdateJobPostingStatus (Guid jobPostingId, JobPostingStatus jobPostingStatus)
        {
            var jobPosting = await (await _unitOfWork.Queryable<JobPosting>())
                .FirstOrDefaultAsync(x => x.Id == jobPostingId);
            if (jobPosting == null)
            {
                throw new NotFoundException();
            }
            jobPosting.JobPostingStatus = jobPostingStatus;

            await _unitOfWork.UpdateAsync(jobPosting);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<JobPostingDetailDto>
            {
                Data = _mapper.Map<JobPostingDetailDto>(jobPosting)
            };
        }
        public async Task<CustomResponse<UserJobPostingDto>> ChangeUserJobPostingStatus(Guid id, UserJobPostingStatus status)
        {
            var userJobPosting = await (await _unitOfWork.Queryable<UserJobPosting>()).FirstOrDefaultAsync(x => x.Id == id);
            if (userJobPosting == null)
            {
                throw new NotFoundException();
            }

            userJobPosting.Status = status;
            await _unitOfWork.UpdateAsync(userJobPosting);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<UserJobPostingDto>
            {
                Data = _mapper.Map<UserJobPostingDto>(userJobPosting)
            };

        }

        public async Task<CustomResponse<List<UserJobPostingDto>>> GetUserJobPostingsAsync(FilterUserJobPostingDto filterUserJobPostingDto, int pageSize, int pageNumber)
        {
            var userJobPostingQuery = await _unitOfWork.Queryable<UserJobPosting>();
            if (filterUserJobPostingDto.Status != null)
            {
                userJobPostingQuery = userJobPostingQuery.Where(x => x.Status == filterUserJobPostingDto.Status);
            }

            var totalItems = await userJobPostingQuery.CountAsync();
            var userJobPostings = await userJobPostingQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => _mapper.Map<UserJobPostingDto>(r))
                .ToListAsync();


            return new CustomResponse<List<UserJobPostingDto>>
            {
                Data = userJobPostings,
                Meta = new MetaData
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
                }
            };

        }

        public async Task<CustomResponse<UserJobPostingDto>> SubmitApplication(SubmitApplicationDto submitApplicationDto)
        {
            var userJobPosting = new UserJobPosting
            {
                UserId = _currentUserService.UserId,
                JobPostingId = submitApplicationDto.JobPostingId,
                CV = await _fileService.UploadFileGetUrlAsync(submitApplicationDto.CV),
            };
            await _unitOfWork.AddAsync(userJobPosting);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<UserJobPostingDto>
            {
                Data = _mapper.Map<UserJobPostingDto>(userJobPosting)
            };
        }

        public async Task<CustomResponse<Dictionary<string, int>>> GetTopOccupation()
        {
            var topOccupation = await (await _unitOfWork.Queryable<JobPosting>())
                .GroupBy(x => x.Occupation)
                .Select(x => new {Occupation = x.Key, Count = x.Count()})
                .OrderByDescending(x => x.Count)
                .ToDictionaryAsync(x => x.Occupation.Name, x => x.Count);
            return new CustomResponse<Dictionary<string, int>>
            {
                Data = topOccupation
            };
        }
    }
}
