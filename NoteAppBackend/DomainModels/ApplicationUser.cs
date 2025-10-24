using System;
using Microsoft.AspNetCore.Identity;

namespace NoteAppBackend.DomainModels;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string NickName { get; set; } = string.Empty;

    public string UserProfileImage { get; set; } = string.Empty;

    // public List<Note> UserNotes { get; set; } = [];
    
    public DateOnly CreatedOn { get; set; }
    public TimeOnly CreatedAt { get; set; }
    public DateOnly UpdatedOn { get; set; }
    public TimeOnly UpdatedAt { get; set; }
}
