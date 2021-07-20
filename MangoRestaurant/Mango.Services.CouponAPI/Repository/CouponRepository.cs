using AutoMapper;
using Mango.Services.CouponAPI.DbContexts;
using Mango.Services.CouponAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private ApplicationDbContext _dbContext;
        private IMapper _mapper;

        public CouponRepository(ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _dbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<CouponDto> GetCouponbyCode(string couponCode)
        {
            var couponFromDb = await _dbContext.Coupons
                .FirstOrDefaultAsync(u => u.CouponCode == couponCode);

            return _mapper.Map<CouponDto>(couponFromDb);
        }
    }
}
