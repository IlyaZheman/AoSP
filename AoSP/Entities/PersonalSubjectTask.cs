﻿namespace AoSP.Entities;

public class PersonalSubjectTask
{
    public string Id { get; set; }
    public int? Score { get; set; }
    public string? FileName { get; set; }
    public virtual byte[]? File { get; set; }

    public string? PersonalSubjectId { get; set; }
    public virtual PersonalSubject PersonalSubject { get; set; }
    public string? SubjectTaskId { get; set; }
    public virtual SubjectTask SubjectTask { get; set; }
}