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
    public class BeneficiaryController : Controller
    {
        private readonly RemittanceDbContext _context;

        public BeneficiaryController(RemittanceDbContext context)
        {
            _context = context;
        }

        // GET: Beneficiary
        public async Task<IActionResult> Index()
        {
            ViewBag.Channels = Helper.GetChannelList();
            var list = await _context.BeneficiaryLog.ToListAsync<BeneficiaryLog>();
            DateTime today = new DateTime();
            list = list.Where(x => x.LogDate.Date == today.Date).ToList();
            return View(list);
        }

        public async Task<IActionResult> BeneficiarySearch(string userId, string channelId, string profileNo, string RefNo, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var list = await _context.BeneficiaryLog.ToListAsync<BeneficiaryLog>();
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


        // GET: Beneficiary/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beneficiaryLog = await _context.BeneficiaryLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beneficiaryLog == null)
            {
                return NotFound();
            }

            return View(beneficiaryLog);
        }

        // GET: Beneficiary/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Beneficiary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LogDate,LogTime,FiscalYear,Ref_No,UserId,ChannelId,ProfileNumber,Request,Response")] BeneficiaryLog beneficiaryLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beneficiaryLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beneficiaryLog);
        }

        // GET: Beneficiary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beneficiaryLog = await _context.BeneficiaryLog.FindAsync(id);
            if (beneficiaryLog == null)
            {
                return NotFound();
            }
            return View(beneficiaryLog);
        }

        // POST: Beneficiary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LogDate,LogTime,FiscalYear,Ref_No,UserId,ChannelId,ProfileNumber,Request,Response")] BeneficiaryLog beneficiaryLog)
        {
            if (id != beneficiaryLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beneficiaryLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeneficiaryLogExists(beneficiaryLog.Id))
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
            return View(beneficiaryLog);
        }

        // GET: Beneficiary/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beneficiaryLog = await _context.BeneficiaryLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beneficiaryLog == null)
            {
                return NotFound();
            }

            return View(beneficiaryLog);
        }

        // POST: Beneficiary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beneficiaryLog = await _context.BeneficiaryLog.FindAsync(id);
            _context.BeneficiaryLog.Remove(beneficiaryLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeneficiaryLogExists(int id)
        {
            return _context.BeneficiaryLog.Any(e => e.Id == id);
        }
    }
}
