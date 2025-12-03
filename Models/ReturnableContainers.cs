using System;
using System.Collections.Generic;

namespace YmmcContainerTrackerApi.Models;

public partial class ReturnableContainers
{
    public string ItemNo { get; set; } = null!;

    public string? PackingCode { get; set; }

    public string? PrefixCode { get; set; }

    public string? ContainerNumber { get; set; }

    public decimal? OutsideLength { get; set; }

    public decimal? OutsideWidth { get; set; }

    public decimal? OutsideHeight { get; set; }

    public decimal? CollapsedHeight { get; set; }

    public decimal? Weight { get; set; }

    public int? PackQuantity { get; set; }

    public string? AlternateId { get; set; }
}
