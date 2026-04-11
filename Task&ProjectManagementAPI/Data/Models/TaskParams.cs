namespace Task_ProjectManagementAPI.Data.Models
{
    public class TaskParams : PaginationParams
    {
        public string? Search {  get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
    }
}
