using FluentValidation;
using FluentValidation.Results;
using MaximApp.Areas.Manage.ViewModels;
using MaximApp.DAL;
using MaximApp.Helper;
using MaximApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaximApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin , Member" )]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IValidator<CreateServiceVM> _validator; 
        private readonly IValidator<UpdateServiceVM> _validatorUpdate; 

        public ServiceController(AppDbContext context, IValidator<CreateServiceVM> validator , IValidator<UpdateServiceVM> validatorUpdate)
        {
            _context = context;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
        }

        public async Task<IActionResult> Index()
        {
            ReadServiceVM readServiceVM = new ReadServiceVM()
            {
                services = await _context.services.ToListAsync()
            };
            return View(readServiceVM);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceVM createServiceVM)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(createServiceVM);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View("Create", createServiceVM);
            }
            Service service = new Service()
            {
                Title = createServiceVM.Title,
                Icon = createServiceVM.Icon , 
                Description=createServiceVM.Description,
                CreatedAt = DateTime.Now

            };
            await _context.services.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> Update(int id)
        {
            TempData["error"] = string.Empty;
            if (id <= 0)
            {
                TempData["error"] = "Problem bas verdi";
                return RedirectToAction(nameof(Index));
            }
            Service service = await _context.services.Where(x=>x.Id==id).FirstOrDefaultAsync();
            if(service is null)
            {
                TempData["error"] = "Problem bas verdi";
                return RedirectToAction(nameof(Index));
            }
            UpdateServiceVM updateServiceVM = new UpdateServiceVM()
            {
                Id = id,
                Title = service.Title,
                Description = service.Description,
                Icon = service.Icon,
                CreatedAt = service.CreatedAt
            };
            return View(updateServiceVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateServiceVM updateServiceVM)
        {
            ValidationResult validationResult = _validatorUpdate.Validate(updateServiceVM);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View("Update", updateServiceVM);
            }
            if (updateServiceVM.Id <= 0)
            {
                TempData["error"] = "Problem bas verdi";
                return RedirectToAction(nameof(Index));
            }
            Service service =await _context.services.FindAsync(updateServiceVM.Id);

            TempData["error"] = string.Empty;
            if(service is null)
            {
                TempData["error"] = "Problem bas verdi";
                return RedirectToAction(nameof(Index));
            }
            service.Title = updateServiceVM.Title;
            service.Description = updateServiceVM.Description;
            service.Icon = updateServiceVM.Icon;
            service.CreatedAt = updateServiceVM.CreatedAt;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["error"] = "Problem bas verdi";
                return RedirectToAction(nameof(Index));
            }
            Service service = await _context.services.FindAsync(id);
            TempData["error"] = string.Empty;
            if (service is null)
            {
                TempData["error"] = "Problem bas verdi";
                return RedirectToAction(nameof(Index));
            }
            _context.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
