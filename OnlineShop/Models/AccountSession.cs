using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{

    [Serializable]
    public class AccountSession
    {
        public Account Account { get; set; }
    }
}