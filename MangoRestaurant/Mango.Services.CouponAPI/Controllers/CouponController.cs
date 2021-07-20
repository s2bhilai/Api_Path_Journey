using Mango.Services.CouponAPI.Models.Dto;
using Mango.Services.CouponAPI.Models.Dtos;
using Mango.Services.CouponAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponController: ControllerBase
    {
        private ICouponRepository _repository;
        protected ResponseDto _response;

        public CouponController(ICouponRepository couponRepository)
        {
            _repository = couponRepository;
            this._response = new ResponseDto();
        }

        [HttpGet("{code}")]
        public async Task<object> GetDiscountForCode(string code)
        {
            try
            {
                CouponDto couponDto = await _repository.GetCouponbyCode(code);
                _response.Result = couponDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }
    }
}
