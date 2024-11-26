using G23NHNT.Models;
using Microsoft.AspNetCore.Mvc;

namespace G23NHNT.Controllers
{
    public class DeleteAccountController : Controller
    {
        private readonly G23_NHNTContext _context;

        public DeleteAccountController(G23_NHNTContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Accounts.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(user);


            await _context.SaveChangesAsync();
            return RedirectToAction("Logout", "Account");
            //try
            //{
            //    var user = await _context.Accounts.FindAsync(id);
            //    if (user == null)
            //    {
            //        return Json(new { success = false, message = "Không tìm thấy nhà tài khoản!" });
            //    }
            //return RedirectToAction("Index", "Home");
            //_context.Accounts.Remove(user);
            //    await _context.SaveChangesAsync();


            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error deleting house: {ex.Message}");
            //    return Json(new { success = false, message = "Đã xảy ra lỗi khi xóa nhà trọ!" });
            //}

            //return RedirectToAction("Logout", "Account");
        }
    }
}
