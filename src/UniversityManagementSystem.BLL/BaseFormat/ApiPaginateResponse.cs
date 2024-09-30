namespace UniversityManagementSystem.BLL.GenericResponseFormat
{
    public class ApiPaginateResponse<T> : ApiResponse<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        public ApiPaginateResponse(T data, int currentPage, int totalPage , int pageSize):base(data)
        {
            CurrentPage = currentPage;
            TotalPage = totalPage;
            PageSize = pageSize;
        }


    }
}
