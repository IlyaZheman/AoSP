﻿using AoSP.Enums;

namespace AoSP.Entities;

public class User
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public Role Role { get; set; }

    public int? GroupId { get; set; }
    public virtual Group Group { get; set; }
    
    public virtual ICollection<PersonalSubject> PersonalSubjects { get; set; }
}