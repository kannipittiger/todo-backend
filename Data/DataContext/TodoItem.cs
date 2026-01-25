using System;
using System.Collections.Generic;

namespace Data.DataContext;

public partial class TodoItem
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public int? StatusId { get; set; }

    public string? Owner { get; set; }
}
