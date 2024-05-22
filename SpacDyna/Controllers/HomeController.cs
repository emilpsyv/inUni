using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpacDyna.DAL;
using SpacDyna.ViewModels.Cards;

namespace SpacDyna.Controllers
{
    public class HomeController(SpacContext _context) : Controller
    {
        
        public async Task< IActionResult> Index()
        {
            var data = await _context.Cards.Select(x=>new GetCardMemberVM 
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImgUrl = x.ImgUrl,
            }).ToListAsync();
            return View(data);
        }

       
    }
}
