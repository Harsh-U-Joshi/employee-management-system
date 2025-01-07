namespace Employee.Management.MVC.Models.Common
{
    public class GridDataResponse<T> where T : class
    {
        public int RecordsTotalCount { get; set; }
        public int RecordsFilteredCount { get; set; }
        public T Data { get; set; }
        public int PageIndex { get; set; }
    }
}
