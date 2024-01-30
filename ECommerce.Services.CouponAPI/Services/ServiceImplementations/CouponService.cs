using ECommerce.Services.CouponAPI.Data;
using ECommerce.Services.CouponAPI.Models;
using ECommerce.Services.CouponAPI.Services.Interfaces;

namespace ECommerce.Services.CouponAPI.Services.ServiceImplementations
{
    public class CouponService: ICouponService
    {
        private readonly AppDbContext _dbContext;

        public CouponService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public IEnumerable<Coupon> GetAllCoupons()
        {
            return _dbContext.Coupons.first;
        }

        public Coupon GetCouponByCode(string couponCode)
        {
            return _dbContext.Coupons.First((item) => item.CouponCode == couponCode);
        }

        public Coupon GetCouponsById(int couponId)
        {
            return _dbContext.Coupons.First((item) => item.CouponId == couponId);

        }
    }
}
