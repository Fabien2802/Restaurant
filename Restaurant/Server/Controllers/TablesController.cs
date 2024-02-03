using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Server.Data;
using Restaurant.Server.IRepository;
using Restaurant.Shared.Domain;

namespace Restaurant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //public TablesController(ApplicationDbContext context)
        public TablesController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Tables
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Table>>> GetTables()
        public async Task<IActionResult> GetTables()
        {
            //return await _context.Tables.ToListAsync();
            var tables = await _unitOfWork.Tables.GetAll();
            return Ok(tables);
        }

        // GET: api/Tables/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Table>> GetTable(int id)
        public async Task<IActionResult> GetTable(int id)
        {
            //var table = await _context.Tables.FindAsync(id);
            var table = await _unitOfWork.Tables.Get(q => q.TableID == id);

            if (table == null)
            {
                return NotFound();
            }

            return Ok(table);
        }

        // PUT: api/Tables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Table table)
        {
            if (id != table.TableID)
            {
                return BadRequest();
            }

            //_context.Entry(table).State = EntityState.Modified;
            _unitOfWork.Tables.Update(table);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!TableExists(id))
                if(!await TableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Table>> PostTable(Table table)
        {
            //_context.Tables.Add(table);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Tables.Insert(table);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetTable", new { id = table.TableID }, table);
        }

        // DELETE: api/Tables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            //var table = await _context.Tables.FindAsync(id);
            var table = await _unitOfWork.Tables.Get(q =>q.TableID == id);
            if (table == null)
            {
                return NotFound();
            }

            //_context.Tables.Remove(table);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Tables.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool TableExists(int id)
        private async Task<bool> TableExists(int id)
        {
            //return (_context.Tables?.Any(e => e.TableID == id)).GetValueOrDefault();
            var table = await _unitOfWork.Tables.Get(q => q.TableID == id);
            return table != null;
        }
    }
}
