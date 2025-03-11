using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phone.Data;
using Phone.Data.Entities;
using Phone.Services;
using Phone.ViewModels;

namespace Phone.Controllers
{
    public class MobilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;
        private readonly string USER_CONTENT_FOLDER_NAME = "user-content";

        public MobilesController(ApplicationDbContext context, IMapper mapper, IStorageService storageService)
        {
            _context = context;
            _mapper = mapper;
            _storageService = storageService;
        }

        // GET: Mobiles
        public async Task<IActionResult> Index(string searchString)
        {
           ViewData["SearchName"] = searchString;
            var mobiles = from m in _context.Phones.Include(m => m.Category)
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                mobiles = mobiles.Where(s => s.Name.Contains(searchString));
            }
            var listMobile = await mobiles.ToListAsync();
            return View(_mapper.Map<IEnumerable<MobileViewModel>>(listMobile));

        }

        // GET: Mobiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobile = await _context.Phones
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobile == null)
            {
                return NotFound();
            }

            var mobileViewModel = _mapper.Map<MobileViewModel>(mobile);
            return View(mobileViewModel);
        }

        // GET: Mobiles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Mobiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MobileCreateRequest request)
        {
            if (ModelState.IsValid)
            {
                var mobile = _mapper.Map<Mobile>(request);
                if (request.ImageFile != null)
                {
                    mobile.ImageUrl = await SaveFile(request.ImageFile);
                }
                _context.Add(mobile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", request.CategoryId);
            return View(request);
        }

        // GET: Mobiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var mobile = await _context.Phones.FindAsync(id);
            var mobile = _mapper.Map<MobileEditRequest>(await _context.Phones.FindAsync(id));
            if (mobile == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", mobile.CategoryId);
            return View(mobile);
        }

        // POST: Mobiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MobileEditRequest request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var mobile = _mapper.Map<Mobile>(request);
                    if (request.ImageFile != null)
                    {
                        mobile.ImageUrl = await SaveFile(request.ImageFile);
                    }
                    _context.Update(mobile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobileExists(request.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", request.CategoryId);
            return View(request);
        }

        // GET: Mobiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobile = await _context.Phones
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobile == null)
            {
                return NotFound();
            }

            var mobileViewModel = _mapper.Map<MobileViewModel>(mobile);
            return View(mobileViewModel);
        }

        // POST: Mobiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mobile = await _context.Phones.FindAsync(id);
            if (mobile != null)
            {
                if (!string.IsNullOrEmpty(mobile.ImageUrl))
                {
                    await _storageService.DeleteFileAsync(mobile.ImageUrl.Replace("/" + USER_CONTENT_FOLDER_NAME + "/", ""));
                }
                _context.Phones.Remove(mobile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MobileExists(int id)
        {
            return _context.Phones.Any(e => e.Id == id);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}
