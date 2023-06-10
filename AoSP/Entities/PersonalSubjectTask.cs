namespace AoSP.Entities;

public class PersonalSubjectTask
{
    public string Id { get; set; }
    public int? Score { get; set; }
    public string? SubjectTaskId { get; set; }

    public string? PersonalSubjectId { get; set; }
    public virtual PersonalSubject? PersonalSubject { get; set; }
}