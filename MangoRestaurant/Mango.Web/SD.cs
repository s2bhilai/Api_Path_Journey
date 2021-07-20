using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Web
{
    public static class SD
    {
        public static string ProductBaseAPI { get; set; }
        public static string ShoppingCartBaseAPI { get; set; }
        public static string CouponAPIBase { get; set; }

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

    }
}
