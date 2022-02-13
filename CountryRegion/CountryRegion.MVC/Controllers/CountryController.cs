using CountryData.Context;
using CountryData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CountryRegion.MVC.Controllers
{
    public class CountryController : Controller
    {
        private readonly AppDbContext _context;
        public CountryController(AppDbContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }
        public async Task<IActionResult> Index()
        {
            var countries = await _context.Countries.ToListAsync();
            return View(countries);
        }
       public async Task<IActionResult> Create()
        {
          var countries=await _context.Countries.ToListAsync();
            ViewBag.CountrySelect = countries.Select(p => new
            {
                Id = p.Id,
                Name = p.Name
            });
            return View();
        }
        public async Task<IActionResult> Add(Country country)
        {
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var country =await _context.Countries.FirstOrDefaultAsync(p => p.Id == id);
            if(country == null)
                return View("Error");
            return View(country);
        }
        public async Task<IActionResult> Update(Country country)
        {
            _context.Countries.Attach(country);
            _context.Entry(country).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var country =await _context.Countries.Include(p=>p.Region).FirstOrDefaultAsync(p=>p.Id==id);
            if(country==null)
                return View("Error");
            return View(country);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var country =await _context.Countries.Include(p=>p.Region).FirstOrDefaultAsync(P=>P.Id==id);
            if (country == null)
                return View("Error");
            return View(country);
        }
        public async Task<IActionResult> Remove(Country country)
        {
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
