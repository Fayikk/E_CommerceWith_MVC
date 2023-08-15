using E_Commerce_MVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_MVC.Controllers
{
    public class UserProcessController : Controller
    {
        private readonly E_Commerce_MVCContext _context;
        public UserProcessController(E_Commerce_MVCContext context)
        {
            _context = context; 
        }

        [HttpGet("VerificationEmail/{concurrencyStamp}")]
        public async Task<IActionResult> Index([FromRoute]string concurrencyStamp)
        {
            var user = await _context.Users.Where(x => x.ConcurrencyStamp == concurrencyStamp).FirstOrDefaultAsync();
            return View(user);
        }

        [HttpPost("RegisterConfirmation/{concurrencyStamp}")]
        public async Task<IActionResult> RegisterVerify([FromRoute]string concurrencyStamp)
        {
            var userDetail = _context.Users.Where(x => x.ConcurrencyStamp == concurrencyStamp).FirstOrDefault();
            userDetail.IsRegisterVerification = true;
            await _context.SaveChangesAsync();
            return View();
        }
    }
}
