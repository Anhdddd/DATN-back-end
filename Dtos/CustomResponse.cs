using System.Net;

namespace DATN_back_end.Dtos
{
    public class CustomResponse<T> where T : class
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public MetaData Meta { get; set; }
    }

    public class MetaData
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
