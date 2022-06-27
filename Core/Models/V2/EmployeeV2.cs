namespace Core.Models;

public class EmployeeV2
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Gender { get; set; }
    public string? College { get; set; }
    public string? Ph_Number { get; set; }
    public CurrentJobV2? CurrentJob { get; set; }
    public ICollection<PreviousJobV2>? PreviousJobs { get; set; }
}

public partial class CurrentJobV2
{
    public string? Current_job_title { get; set; }
    public string? Company { get; set; }
    public string? Department { get; set; }
}

public partial class PreviousJobV2
{
    public string? Previous_job_title { get; set; }
    public string? Company { get; set; }
    public int Employee_Id { get; set; }
}