




using System;
using System.Collections.Generic;

namespace Unowhy_Tools_WPF.Models;

public class Hardware
{
    public string Name { get; set; } = String.Empty;
    public double Value { get; set; } = 0d;
    public double Min { get; set; } = 0d;
    public double Max { get; set; } = 0d;

    public IEnumerable<Hardware> SubItems { get; set; } = new Hardware[] { };
}

