using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Users;

namespace Web.Helpers
{
    public static class CurrentInfos
    {
        public static User CurrentUser { get; set; }
    }
}
