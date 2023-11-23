using System;
using System.Collections.Generic;

namespace SmartHome.Models.Entity;

public partial class SecurityLog
{
    public int Id { get; set; }

    public string? DeviceName { get; set; }

    public DateTime? Time { get; set; }

    public double? Temperature { get; set; }

    public double? Humidity { get; set; }

    public double? Water { get; set; }

    public double? Gas { get; set; }

    public double? Light { get; set; }

    public string? State { get; set; }
}
