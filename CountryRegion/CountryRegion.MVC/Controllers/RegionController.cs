using CountryData.Context;
using CountryData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountryRegion.MVC.Controllers
{
    public class RegionController : Controller
    {
        private readonly AppDbContext _context;
        public RegionController(AppDbContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }
        public async Task<IActionResult> Index()
        {
            var regions = await _context.Regions.ToListAsync();
            return View(regions);
        }
        public async Task<IActionResult> Create()
        {
            List<SelectListItem> CountryId = new List<SelectListItem>();
            var countries = await _context.Countries.ToListAsync();
            foreach (var country in countries)
            {
                CountryId.Add(new SelectListItem { Text = country.Name, Value = (country.Id).ToString() });
            }
            ViewBag.CountryName = CountryId;
            return View();
        }
        public async Task<IActionResult> Add(Region region)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(p => p.Id == region.CountryId);
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int Id)
        {
            List<SelectListItem> CountryId = new List<SelectListItem>();
            var countries = await _context.Countries.ToListAsync();
            var region = await _context.Regions.FirstOrDefaultAsync(p => p.Id == Id);
            if (region == null)
                return Redirect("Error");
            ViewBag.CountryName = CountryId;
            return View(region);
        }
        public async Task<IActionResult> Update(Region region)
        {
            _context.Regions.Attach(region);
            _context.Entry(region).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int Id)
        {
            var region = await _context.Regions.Include(p => p.CountryId).FirstOrDefaultAsync(p => p.Id == Id);
            if (region is null)
                return Redirect("Error");
            return View(region);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var region = await _context.Regions.Include(p => p.Country).FirstOrDefaultAsync(p => p.Id == Id);
            if (region is null)
                return Redirect("Error");
            return View(region);
        }
        public async Task<IActionResult> Remove(Region region)
        {
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
