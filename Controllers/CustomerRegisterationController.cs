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
    public class CustomerRegisterationController : Controller
    {
        private readonly RemittanceDbContext _context;

        public CustomerRegisterationController(RemittanceDbContext context)
        {
            _context = context;
        }

        // GET: CustomerRegisteration
        public async Task<IActionResult> Index()
        {
            ViewBag.Channels = Helper.GetChannelList();
            var list = await _context.CustomerRegisterationLog.ToListAsync<CustomerRegisterationLog>();
            DateTime today = new DateTime();
            list = list.Where(x => x.LogDate.Date == today.Date).ToList();
            return View(list);
        }

        public async Task<IActionResult> CustomerRegisterationSearch(string userId, string channelId, string name, string RefNo, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var list = await _context.CustomerRegisterationLog.ToListAsync<CustomerRegisterationLog>();
                if (list.Count > 0)
                {
                    if (!string.IsNullOrEmpty(userId))
                        list = list.Where(x => x.UserId == userId).ToList();
                    if (!string.IsNullOrEmpty(channelId))
                        list = list.Where(x => x.ChannelId == channelId).ToList();
                    if (!string.IsNullOrEmpty(name))
                        list = list.Where(x => x.Name == name).ToList();
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

        // GET: CustomerRegisteration/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerRegisterationLog = await _context.CustomerRegisterationLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerRegisterationLog == null)
            {
                return NotFound();
            }

            return View(customerRegisterationLog);
        }

        // GET: CustomerRegisteration/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerRegisteration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LogDate,LogTime,FiscalYear,Ref_No,UserId,ChannelId,Name,MobileNumber,Request,Response")] CustomerRegisterationLog customerRegisterationLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerRegisterationLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerRegisterationLog);
        }

        // GET: CustomerRegisteration/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerRegisterationLog = await _context.CustomerRegisterationLog.FindAsync(id);
            if (customerRegisterationLog == null)
            {
                return NotFound();
            }
            return View(customerRegisterationLog);
        }

        // POST: CustomerRegisteration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LogDate,LogTime,FiscalYear,Ref_No,UserId,ChannelId,Name,MobileNumber,Request,Response")] CustomerRegisterationLog customerRegisterationLog)
        {
            if (id != customerRegisterationLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerRegisterationLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerRegisterationLogExists(customerRegisterationLog.Id))
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
            return View(customerRegisterationLog);
        }

        // GET: CustomerRegisteration/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerRegisterationLog = await _context.CustomerRegisterationLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerRegisterationLog == null)
            {
                return NotFound();
            }

            return View(customerRegisterationLog);
        }

        // POST: CustomerRegisteration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerRegisterationLog = await _context.CustomerRegisterationLog.FindAsync(id);
            _context.CustomerRegisterationLog.Remove(customerRegisterationLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerRegisterationLogExists(int id)
        {
            return _context.CustomerRegisterationLog.Any(e => e.Id == id);
        }
    }
}
