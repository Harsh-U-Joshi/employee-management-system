namespace Employee.Management.MVC.Models.Common
{
    public class QueryParamModel
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string SortColumn { get; set; }

        public string OrderBy { get; set; }

        public string Search { get; set; }
    }
}
