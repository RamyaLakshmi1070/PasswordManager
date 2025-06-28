using System;
using System.Collections.Generic;

namespace PasswordManager.Data.Models;

public partial class Password
{
    public int Id { get; set; }

    public string? Category { get; set; }

    public string? App { get; set; }

    public string? UserName { get; set; }

    public string? EncryptedPassword { get; set; }
}
