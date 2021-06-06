using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemittanceWebApp.Helpers
{
    public static class Helper
    {
        public static List<SelectListItem> GetChannelList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "ONLINE", Value = "ONLINE" });
            items.Add(new SelectListItem { Text = "MOBILE", Value = "MOBILE" });
            items.Add(new SelectListItem { Text = "AGENT", Value = "AGENT" });
            return items;
        }
    }
}
