using System;
using System.Collections.Generic;

namespace SmartHome.Models.Entity;

public partial class Log
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public DateTime? Time { get; set; }
}
