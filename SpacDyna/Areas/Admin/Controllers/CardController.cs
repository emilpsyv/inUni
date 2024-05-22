using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpacDyna.DAL;
using SpacDyna.Extension;
using SpacDyna.Models;
using SpacDyna.ViewModels.Cards;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SpacDyna.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CardController(SpacContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data =await _context.Cards.Select(x => new GetAdminCardVM
            {
                Name = x.Name,
                Description = x.Description,
                ImgUrl = x.ImgUrl,
                Id = x.Id,
                CreatedTime = x.CreatedTime,
            }).ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCardVM cardVM)
        {
            string FileName = await cardVM.Image.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "card"));

            Card card = new Card
            {
                ImgUrl=Path.Combine("imgs","card",FileName),
                Name = cardVM.Name,
                Description = cardVM.Description,
                CreatedTime = DateTime.Now,
            };
            await _context.Cards.AddAsync(card);           
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Update(int? id,IFormFile file)
        {
           var data= await _context.Cards.FirstOrDefaultAsync(x => x.Id == id);

            UpdateCardVM cardVM = new UpdateCardVM
            {
                Name = data.Name,
                Description = data.Description,
                Image = file,
                
                
            };
            
            return View(cardVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id ,UpdateCardVM cardVM)
        {
            string FileName = await cardVM.Image.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "card"));
            var existed = await _context.Cards.FirstOrDefaultAsync(x=>x.Id==id);
            existed.Name = cardVM.Name;
            existed.Description = cardVM.Description;

            System.IO.File.Delete(existed.ImgUrl);

            existed.ImgUrl = Path.Combine("imgs", "card", FileName);
           

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id )
        {
            var data = await _context.Cards.FirstOrDefaultAsync(x => x.Id == id);

            _context.Cards.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
