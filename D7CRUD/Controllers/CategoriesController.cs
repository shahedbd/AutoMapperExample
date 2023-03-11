using D7CRUD.Data;
using D7CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using D7CRUD.Data;
using D7CRUD.Models;
using AutoMapper;

namespace TutorialProject.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var _Category = await _context.Category.ToListAsync();
            if (_Category.Count < 1)
                await CreateTestData();

            return _context.Category != null ?
                        View(await _context.Category.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Category'  is null.");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            CategoryViewModel _CategoryViewModel = new();
            return View(_CategoryViewModel);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel _CategoryViewModel)
        {
            var _Category = _mapper.Map<Category>(_CategoryViewModel);
            if (ModelState.IsValid)
            {
                _context.Add(_Category);
                await _context.SaveChangesAsync();
                _CategoryViewModel.AlertMessage = "Data Added Successfully. Id: " + _Category.Id;
                return View(_CategoryViewModel);
            }
            return View(_CategoryViewModel);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Description")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }



        [HttpDelete]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _Categories = await _context.Category.FindAsync(id);

                if (_Categories != null)
                {
                    _context.Category.Remove(_Categories);
                }
                await _context.SaveChangesAsync();
                return new JsonResult(_Categories);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool CategoryExists(long id)
        {
            return (_context.Category?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task CreateTestData()
        {
            foreach (var item in GetCategoryList())
            {
                _context.Category.Add(item);
                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<Category> GetCategoryList()
        {
            return new List<Category>
            {
                new Category { Name = "Item Category 01", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 02", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 03", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 04", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 05", Description = "Description of your category item: lorem ipsum" },

                new Category { Name = "Item Category 06", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 07", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 08", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 09", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 10", Description = "Description of your category item: lorem ipsum" },
            };
        }

    }
}
