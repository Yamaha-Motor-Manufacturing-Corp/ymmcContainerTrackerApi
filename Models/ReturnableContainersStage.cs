using System;
using System.Collections.Generic;

namespace YmmcContainerTrackerApi.Models;

public partial class ReturnableContainersStage
{
    public string? ItemNo { get; set; }

    public string? PackingCode { get; set; }

    public string? PrefixCode { get; set; }

    public string? ContainerNumber { get; set; }

    public string? OutsideLength { get; set; }

    public string? OutsideWidth { get; set; }

    public string? OutsideHeight { get; set; }

    public string? CollapsedHeight { get; set; }

    public string? Weight { get; set; }

    public string? PackQuantity { get; set; }

    public string? AlternateId { get; set; }
}
