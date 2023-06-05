namespace AoSP.Entities;

public class PersonalSubjectTask
{
    public int Id { get; set; }
    public int? Score { get; set; }
    public int? SubjectTaskId { get; set; }

    public int? PersonalSubjectId { get; set; }
    public virtual PersonalSubject? PersonalSubject { get; set; }
}