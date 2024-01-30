using ECommerce.Services.CouponAPI.Models;
using ECommerce.Services.CouponAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponAPIController(ICouponService couponService)
        {
            this._couponService = couponService;
        }

        [HttpGet]
        [Route("GetAll")]
        public object Get()
        {
            try { 
                IEnumerable<Coupon> objLIst = this._couponService.GetAllCoupons();
                return objLIst;
            
            } catch (Exception ex)
            {

            }

            return null;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                Coupon couponObj = this._couponService.GetCouponsById(id);
                return couponObj;

            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
