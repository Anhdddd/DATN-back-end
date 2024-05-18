namespace DATN_back_end.Common
{
    public enum Role
    {
        Admin,
        Employer,
        User
    }

    public enum CompanySize
    {
        Small,
        Medium,
        Large
    }

    public enum EmployeeType
    {
        FullTime,
        PartTime,
        Freelance,
    }

    public enum Experience
    {
        NoExperience,
        LessThanOneYear,
        OneToTwoYears,
        TwoToThreeYears,
        ThreeToFiveYears,
        MoreThanFiveYears
    }

    public enum JobPostingStatus
    {
        Active,
        Expired,
        Hidden,
        Pending,
        Rejected
    }

    public enum CompanyStatus
    {
        Active,
        Pending,
        Rejected
    }
    
    public enum CompanySortType
    {
        ViewCount
    }

    public enum JobPostingSortType
    {
        ViewCount
    }

    public enum UserJobPostingStatus
    {
        Rejected,
        Approved,
        Viewed,
        NotViewed
    }
}