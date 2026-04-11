namespace Task_ProjectManagementAPI.Data.Models
{
    public class ProjectParams : PaginationParams
    {
        public string? Search {  get; set; }
        public string? Status { get; set; }
    }
}
