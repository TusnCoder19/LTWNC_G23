using G23NHNT.Models;
using Microsoft.AspNetCore.Mvc;

namespace G23NHNT.Controllers
{
    public class ChangeAccountController : Controller
    {
        private readonly G23_NHNTContext _context;

        public ChangeAccountController(G23_NHNTContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            // Lấy dữ liệu từ database
            var account = await _context.Accounts.FindAsync(1);
            if (HttpContext.Session.GetInt32("UserId") != null)
                account = await _context.Accounts.FindAsync(HttpContext.Session.GetInt32("UserId"));



            if (account == null)
            {
                return NotFound(); // Xử lý nếu không tìm thấy
            }
            return View(account);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Account account)
        {
            var user = await _context.Accounts.FindAsync(account.IdUser);

            if (user == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin
            user.UserName = account.UserName;
            user.PhoneNumber = account.PhoneNumber;
            user.Password = account.Password;
            //user.Role = account.Role;

            HttpContext.Session.SetString("UserName", user.UserName);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
