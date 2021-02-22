using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace Domain.Enums
{
    public enum PaymentMethodType
    {
        [Display(Name = "Cash")]
        Cash = 1,
        [Display(Name = "CreditCard")]
        CreditCard = 2
      

    }
}
