using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonEmissions.Shared.Enums
{
    public enum EmissionType
    {
        [Description("Ninguna")]
        None,
        [Description("Controladas")]
        DirectEmissions,
        [Description("No controladas")]
        IndirectEmissions
    }
}
