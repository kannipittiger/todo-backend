using System;
using System.Collections.Generic;

namespace Data.DataContext;

public partial class TodoUser
{
    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Password { get; set; }
}
