using System;
using System.Collections.Generic;

namespace Data.DataContext;

public partial class TodoMasterStatus
{
    public int StatusId { get; set; }

    public string StatusDesc { get; set; } = null!;
}
