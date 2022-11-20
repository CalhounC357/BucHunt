using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScavengeRUs.Data;
using ScavengeRUs.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ScavengeRUs.Controllers
{
    [Authorize(Roles = "Admin")] //makes sure that only admin can see this page
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// This method injects the context into the class so that the methods can connect to
        /// the database
        /// </summary>
        /// <param name="context"></param>
        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method maps to the /Locations URL. It shows the table
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
              return View(await _context.Location.ToListAsync());
        }

        /// <summary>
        /// This method shows the details of a specific location based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Location == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        /// <summary>
        /// This is the page that will be shown when the admin want to create a new location from the 
        /// website
        /// </summary>
        /// <returns></returns>
        public IActionResult Create([Bind(Prefix = "Id")] int huntid)
        {
            return View();
        }

        /// <summary>
        /// This method will be called when the admin submits the newly created location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(Prefix = "Id")] int huntid, Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                if (huntid != 0)
                {
                    return RedirectToAction("ManageTasks", "Hunt", new { id = huntid });
                }
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        /// <summary>
        /// This is the page that will be shown when the admin attempts to edit a location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Location == null)
            {
                return NotFound();
            }

            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        /// <summary>
        /// This method will be called when the admin submits the edit to the location from the previous
        /// method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HuntId,Place,Lat,Lon,Task,AccessCode,QRCode,Answer")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id))
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
            return View(location);
        }

        /// <summary>
        /// This page will be shown when the admin attempts to delete a location from the site
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Location == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        /// <summary>
        /// This method will activate when the admin submits the delete action on a location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Location == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Location'  is null.");
            }
            var location = await _context.Location.FindAsync(id);
            if (location != null)
            {
                _context.Location.Remove(location);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// This method checks to make sure that a Location row exists in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool LocationExists(int id)
        {
          return _context.Location.Any(e => e.Id == id);
        }
    }
}
