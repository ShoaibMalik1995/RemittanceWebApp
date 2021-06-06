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
    public class SICreateController : Controller
    {
        private readonly RemittanceDbContext _context;

        public SICreateController(RemittanceDbContext context)
        {
            _context = context;
        }

        // GET: SICreate
        public async Task<IActionResult> Index()
        {
            ViewBag.Channels = Helper.GetChannelList();
            var list = await _context.SICreateLog.ToListAsync<SICreateLog>();
            DateTime today = new DateTime();
            list = list.Where(x => x.LogDate.Date == today.Date).ToList();
            return View(list);
        }

        public async Task<IActionResult> SICreateSearch(string userId, string channelId, string profileNo, string RefNo, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var list = await _context.SICreateLog.ToListAsync<SICreateLog>();
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

        // GET: SICreate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sICreateLog = await _context.SICreateLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sICreateLog == null)
            {
                return NotFound();
            }

            return View(sICreateLog);
        }

        // GET: SICreate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SICreate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LogDate,LogTime,FiscalYear,Ref_No,UserId,ChannelId,ProfileNumber,Request,Response")] SICreateLog sICreateLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sICreateLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sICreateLog);
        }

        // GET: SICreate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sICreateLog = await _context.SICreateLog.FindAsync(id);
            if (sICreateLog == null)
            {
                return NotFound();
            }
            return View(sICreateLog);
        }

        // POST: SICreate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LogDate,LogTime,FiscalYear,Ref_No,UserId,ChannelId,ProfileNumber,Request,Response")] SICreateLog sICreateLog)
        {
            if (id != sICreateLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sICreateLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SICreateLogExists(sICreateLog.Id))
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
            return View(sICreateLog);
        }

        // GET: SICreate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sICreateLog = await _context.SICreateLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sICreateLog == null)
            {
                return NotFound();
            }

            return View(sICreateLog);
        }

        // POST: SICreate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sICreateLog = await _context.SICreateLog.FindAsync(id);
            _context.SICreateLog.Remove(sICreateLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SICreateLogExists(int id)
        {
            return _context.SICreateLog.Any(e => e.Id == id);
        }
    }
}
