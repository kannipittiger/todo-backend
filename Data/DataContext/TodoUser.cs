using System;
using System.Collections.Generic;

namespace Data.DataContext;

public partial class TodoUser
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
}
