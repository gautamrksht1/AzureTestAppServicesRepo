using ECommerce.Services.CouponAPI.Models;

namespace ECommerce.Services.CouponAPI.Services.Interfaces
{
    public interface ICouponService
    {
        IEnumerable<Coupon> GetAllCoupons();

        Coupon GetCouponsById(int couponId);

        Coupon GetCouponByCode(string code);
    }
}
