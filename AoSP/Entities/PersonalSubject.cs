﻿namespace AoSP.Entities;

public class PersonalSubject
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    public ICollection<PersonalSubjectTask> PersonalSubjectTasks { get; set; }
}