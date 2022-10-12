using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Enums
{
    public enum GenderInformation
    {
        [Display(Name = "Male")]
        M = 1,

        [Display(Name = "Female")]
        F = 2,
        [Display(Name = "Unspecified")]
        U = 3,
    }
}
