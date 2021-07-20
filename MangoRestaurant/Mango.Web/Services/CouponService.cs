using Mango.Web.Models;
using Mango.Web.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Web.Services
{
    public class CouponService : BaseService,ICouponService
    {
        private IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetCoupon<T>(string couponCode, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "api/coupon/" + couponCode,
                AccessToken = token
            });
        }
    }
}
