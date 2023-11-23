using System;
using System.Collections.Generic;

namespace SmartHome.Models.Entity;

public partial class Email
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gmail { get; set; } = null!;
}
