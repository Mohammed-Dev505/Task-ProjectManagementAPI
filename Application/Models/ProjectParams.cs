namespace Task_ProjectManagementAPI.Application.Models
{
    public class ProjectParams : PaginationParams
    {
        public string? projectName {  get; set; }
        public string? Status { get; set; }
    }
}
