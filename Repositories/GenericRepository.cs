using Microsoft.EntityFrameworkCore;
using RemittanceWebApp.Data;
using RemittanceWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemittanceWebApp.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Properties
        private RemittanceDbContext _context = null;
        private DbSet<T> table = null;
        #endregion

        #region Ctor
        public GenericRepository(RemittanceDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        #endregion


        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }
        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }
        public async Task<bool> Insert(T obj)
        {
            table.Add(obj);

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Delete(object id)
        {
            T existing = await table.FindAsync(id);
            table.Remove(existing);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
