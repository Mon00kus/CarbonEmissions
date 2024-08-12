﻿using System.ComponentModel;

namespace CarbonEmissions.Shared.Enums
{
    public enum UserType
    {
        [Description("Administrador")]
        Admin,

        [Description("Usuario")]
        User
    }
}