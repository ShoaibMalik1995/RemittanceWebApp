using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RemittanceWebApp.Data;
using RemittanceWebApp.Helpers;
using RemittanceWebApp.Models;

namespace RemittanceWebApp.Controllers
{
    public class FCRequestController : Controller
    {
        private readonly RemittanceDbContext _context;

        public FCRequestController(RemittanceDbContext context)
        {
            _context = context;
        }

        // GET: FCRequest
        public async Task<IActionResult> Index()
        {
            ViewBag.Channels = Helper.GetChannelList();
            var list = await _context.FCRequestLog.ToListAsync<FCRequestLog>();
            DateTime today = new DateTime();
            list = list.Where(x => x.LogDate.Date == today.Date).ToList();
            return View(list);
        }

        public async Task<IActionResult> FCRequestSearch(string userId, string channelId, string profileNo, string RefNo, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var list = await _context.FCRequestLog.ToListAsync<FCRequestLog>();
                if (list.Count > 0)
                {
                    if (!string.IsNullOrEmpty(userId))
                        list = list.Where(x => x.UserId == userId).ToList();
                    if (!string.IsNullOrEmpty(channelId))
                        list = list.Where(x => x.ChannelId == channelId).ToList();
                    if (!string.IsNullOrEmpty(profileNo))
                        list = list.Where(x => x.ProfileNumber == profileNo).ToList();
                    if (!string.IsNullOrEmpty(RefNo))
                        list = list.Where(x => x.Ref_No == RefNo).ToList();
                    if (!string.IsNullOrEmpty(fromDate.ToString("dd/MM/yyyy")) && !string.IsNullOrEmpty(toDate.ToString("dd/MM/yyyy")))
                        list = list.Where(x => x.LogDate.Date >= fromDate.Date && x.LogDate.Date <= toDate.Date).ToList();
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: FCRequest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fCRequestLog = await _context.FCRequestLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fCRequestLog == null)
            {
                return NotFound();
            }

            return View(fCRequestLog);
        }

        // GET: FCRequest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FCRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LogDate,LogTime,FiscalYear,Ref_No,UserId,ChannelId,ProfileNumber,Request,Response")] FCRequestLog fCRequestLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fCRequestLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fCRequestLog);
        }

        // GET: FCRequest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fCRequestLog = await _context.FCRequestLog.FindAsync(id);
            if (fCRequestLog == null)
            {
                return NotFound();
            }
            return View(fCRequestLog);
        }

        // POST: FCRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LogDate,LogTime,FiscalYear,Ref_No,UserId,ChannelId,ProfileNumber,Request,Response")] FCRequestLog fCRequestLog)
        {
            if (id != fCRequestLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fCRequestLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FCRequestLogExists(fCRequestLog.Id))
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
            return View(fCRequestLog);
        }

        // GET: FCRequest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fCRequestLog = await _context.FCRequestLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fCRequestLog == null)
            {
                return NotFound();
            }

            return View(fCRequestLog);
        }

        // POST: FCRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fCRequestLog = await _context.FCRequestLog.FindAsync(id);
            _context.FCRequestLog.Remove(fCRequestLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FCRequestLogExists(int id)
        {
            return _context.FCRequestLog.Any(e => e.Id == id);
        }
    }
}
