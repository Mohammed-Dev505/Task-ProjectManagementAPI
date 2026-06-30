namespace Task_ProjectManagementAPI.Application.Models
{
    public class TaskParams : PaginationParams
    {
        public string? Title {  get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
    }
}
