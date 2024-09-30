using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.BLL.BaseFormat
{
    public class PaginateRequest
    {
        private int _page = 1; // Default to first page
        private int _pageSize = 10; // Default to page size 
        private const int MaxPageSize = 50;

        public int Page
        {
            get => _page;
            set => _page = (value > 0) ? value : 1;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > 0 && value <= MaxPageSize) ? value : 10;
        }

        public string SortBy { get; set; } = "";
    }
}
