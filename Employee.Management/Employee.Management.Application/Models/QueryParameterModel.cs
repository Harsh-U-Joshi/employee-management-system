namespace Employee.Management.Application.Models
{
    public class QueryParamModel
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int Skip { get { return (PageIndex - 1) * PageSize; } }

        public string SortColumn { get; set; }

        public string OrderBy { get; set; } = "desc";

        public string Search { get; set; } = "";
    }

    public class GridDataResponse
    {
        public int RecordsTotalCount { get; set; }
        public int RecordsFilteredCount { get; set; }
        public object Data { get; set; }
        public int PageIndex { get; set; }
    }
}
