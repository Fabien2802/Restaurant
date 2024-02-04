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
    public class ReservationsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //public ReservationsController(ApplicationDbContext context)
        public ReservationsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Reservations
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        public async Task<IActionResult> GetReservations()
        {
            //return await _context.Reservations.ToListAsync();
            var reservations = await _unitOfWork.Reservations.GetAll(includes: q => q.Include(x => x.Table));
            return Ok(reservations);
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Reservation>> GetReservation(int id)
        public async Task<IActionResult> GetReservation(int id)
        {
            //var Reservation = await _context.Reservations.FindAsync(id);
            var Reservation = await _unitOfWork.Reservations.Get(q => q.ReservationID == id);

            if (Reservation == null)
            {
                return NotFound();
            }

            return Ok(Reservation);
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation Reservation)
        {
            if (id != Reservation.ReservationID)
            {
                return BadRequest();
            }

            //_context.Entry(Reservation).State = EntityState.Modified;
            _unitOfWork.Reservations.Update(Reservation);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ReservationExists(id))
                if (!await ReservationExists(id))
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

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation Reservation)
        {
            //_context.Reservations.Add(Reservation);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Reservations.Insert(Reservation);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetReservation", new { id = Reservation.ReservationID }, Reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            //var Reservation = await _context.Reservations.FindAsync(id);
            var Reservation = await _unitOfWork.Reservations.Get(q => q.ReservationID == id);
            if (Reservation == null)
            {
                return NotFound();
            }

            //_context.Reservations.Remove(Reservation);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Reservations.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool ReservationExists(int id)
        private async Task<bool> ReservationExists(int id)
        {
            //return (_context.Reservations?.Any(e => e.ReservationID == id)).GetValueOrDefault();
            var Reservation = await _unitOfWork.Reservations.Get(q => q.ReservationID == id);
            return Reservation != null;
        }
    }
}
