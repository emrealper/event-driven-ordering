using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace Domain.Enums
{
    public enum CurrencyType
    {

        [Display(Name = "EURO")]
        Euro = 1,
        [Display(Name = "USD")]
        Dolar = 2,
        [Display(Name = "GBP")]
        GBP = 3


    }
}
