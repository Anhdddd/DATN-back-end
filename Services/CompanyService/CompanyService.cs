using DATN_back_end.Common;
using DATN_back_end.Dtos;
using DATN_back_end.Dtos.Company;
using DATN_back_end.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DATN_back_end.Services.CompanyService
{
    public class CompanyService : BaseService, ICompanyService
    {
        public CompanyService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public async Task<CustomResponse<CompanyDetailDto>> AddAsync(CompanyAddDto companyDto)
        {
            var company = new Company
            {
                Name = companyDto.Name,
                Address = companyDto.Address,
                Description = companyDto.Description,
                Status = CompanyStatus.Pending,
                ViewCount = 0,
                OwnerId = _currentUserService.UserId,
                TaxCode = companyDto.TaxCode
            };
            if (companyDto.Logo != null)
            {
                company.Logo = await _fileService.UploadFileGetUrlAsync(companyDto.Logo);
            }
            if (companyDto.BackgroundImage is not null)
            {
                company.BackgroundImage = await _fileService.UploadFileGetUrlAsync(companyDto.BackgroundImage);
            }

            await _unitOfWork.AddAsync(company);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<CompanyDetailDto>
            {
                Data = _mapper.Map<CompanyDetailDto>(company),
            };
        }

        public async Task<CustomResponse<List<CompanyDto>>> GetCompaniesAsync(FilterCompanyDto? filterCompanyDto, string searchValue = "", int pageSize = 10, int pageNumber = 1)
        {
            var companyQuery = await _unitOfWork.Queryable<Company>();
            companyQuery = companyQuery.Include(x => x.Owner);


            if (string.IsNullOrEmpty(searchValue) == false)
            {
                companyQuery = companyQuery.Where(x => x.Name.Contains(searchValue));
            }

            switch (filterCompanyDto.SortType)
            {
                case CompanySortType.ViewCount:
                    companyQuery = companyQuery.OrderByDescending(x => x.ViewCount);
                    break;
                default:
                    companyQuery = companyQuery.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            int totalItems = await companyQuery.CountAsync();
            var pagedCompanies = await companyQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => _mapper.Map<CompanyDto>(r))
                .ToListAsync();

            return new CustomResponse<List<CompanyDto>>
            {
                Data = pagedCompanies,
                Meta = new MetaData
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
                },
            };
        }

        public async Task<CustomResponse<CompanyDetailDto>> GetCompanyByIdAsync(Guid CompanyId)
        {
            var company = await (await _unitOfWork.Queryable<Company>())
                .FirstOrDefaultAsync(x => x.Id == CompanyId);
            if (company == null)
            {
                throw new NotFoundException();
            }

            return new CustomResponse<CompanyDetailDto>
            {
                Data = _mapper.Map<CompanyDetailDto>(company),
            };
        }

        public async Task<CustomResponse<CompanyDetailDto>> GetMyCompanyAsync()
        {
            var company = await (await _unitOfWork.Queryable<Company>())
                .FirstOrDefaultAsync(x => x.OwnerId == _currentUserService.UserId);
            if (company == null)
            {
                throw new NotFoundException();
            }
            return new CustomResponse<CompanyDetailDto>
            {
                Data = _mapper.Map<CompanyDetailDto>(company),
            };
        }

        public async Task<CustomResponse<CompanyDetailDto>> UpdateAsync(CompanyUpdateDto companyDto)
        {
            var company = await (await _unitOfWork.Queryable<Company>())
                .FirstOrDefaultAsync(x => x.OwnerId == _currentUserService.UserId);

            if (company == null)
            {
                throw new NotFoundException();
            }
            if (companyDto.Name != null)
            {
                company.Name = companyDto.Name;
            }
            if (companyDto.Address != null)
            {
                company.Address = companyDto.Address;
            }
            if (companyDto.Description != null)
            {
                company.Description = companyDto.Description;
            }
            if (companyDto.Email != null)
            {
                company.Email = companyDto.Email;
            }
            if (companyDto.PhoneNumber != null)
            {
                company.PhoneNumber = companyDto.PhoneNumber;
            }
            if (companyDto.Website != null)
            {
                company.Website = companyDto.Website;
            }
            if (companyDto.CompanySize != null)
            {
                company.CompanySize = companyDto.CompanySize.Value;
            }
            if (companyDto.TaxCode != null)
            {
                company.TaxCode = companyDto.TaxCode;
            }

            await _unitOfWork.UpdateAsync(company);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<CompanyDetailDto>
            {
                Data = _mapper.Map<CompanyDetailDto>(company),
            };
        }

        public async Task<CustomResponse<CompanyDetailDto>> UpdateLogoAsync(CompanyUpdateImageDto companyDto)
        {
            var company = await (await _unitOfWork.Queryable<Company>())
                .FirstOrDefaultAsync(x => x.OwnerId == _currentUserService.UserId);

            if (company == null)
            {
                throw new NotFoundException();
            }
            if (companyDto.Logo != null)
            {
                List<string> imageNamesToBeDeleted = new();
                if (company.Logo != null)
                {
                    var imageNameToBeDeleted = company.Logo.Split("/").Last();
                    imageNamesToBeDeleted.Add(imageNameToBeDeleted);
                    await _fileService.DeleteFileAsync(imageNamesToBeDeleted);
                }

                company.Logo = await _fileService.UploadFileGetUrlAsync(companyDto.Logo);
            }

            await _unitOfWork.UpdateAsync(company);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<CompanyDetailDto>
            {
                Data = _mapper.Map<CompanyDetailDto>(company),
            };
        }

        public async Task<CustomResponse<object>> SaveCompanyAsync(Guid companyId)
        {
            var userSavedCompany = new UserSavedCompany
            {
                UserId = _currentUserService.UserId,
                CompanyId = companyId
            };
            await _unitOfWork.AddAsync(userSavedCompany);
            await _unitOfWork.SaveChangesAsync();
            return new CustomResponse<object>
            {
            };
        }

        public async Task<CustomResponse<List<CompanyDto>>> GetSavedCompanyAsync(int pageSize, int pageNumber)
        {
            var savedCompanyQuery = await (await _unitOfWork.Queryable<UserSavedCompany>())
                .Where(x => x.UserId == _currentUserService.UserId)
                .Select(x => x.Company).ToListAsync();

            var totalItems = savedCompanyQuery.Count();
            var pagedCompanies = savedCompanyQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => _mapper.Map<CompanyDto>(r))
                .ToList();
            return new CustomResponse<List<CompanyDto>>
            {
                Data = pagedCompanies,
                Meta = new MetaData
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
                },
            };
        }

        public async Task<CustomResponse<CompanyDetailDto>> UpdateCompanyStatus(Guid companyId, CompanyStatus companyStatus)
        {
            var company = await (await _unitOfWork.Queryable<Company>())
                .FirstOrDefaultAsync(x => x.Id == companyId);
            if (company == null)
            {
                throw new NotFoundException();
            }
            company.Status = companyStatus;
            await _unitOfWork.UpdateAsync(company);
            await _unitOfWork.SaveChangesAsync();
            return new CustomResponse<CompanyDetailDto>
            {
                Data = _mapper.Map<CompanyDetailDto>(company),
            };
        }
    }
}
