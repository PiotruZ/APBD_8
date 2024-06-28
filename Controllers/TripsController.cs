using Microsoft.AspNetCore.Mvc;
using APBD_8.Models;

namespace APBD_8.Controllers // Update namespace accordingly
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly LocalDatabaseContext _context;

        public TripsController(LocalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/trips
        [HttpGet]
        public IActionResult GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var trips = _context.Trips
                .OrderByDescending(t => t.DateFrom)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new {
                    t.Name,
                    t.Description,
                    t.DateFrom,
                    t.DateTo,
                    t.MaxPeople,
                    Countries = t.TripCountries.Select(tc => new { tc.Country.Name }),
                    Clients = t.ClientTrips.Select(ct => new { ct.Client.FirstName, ct.Client.LastName })
                })
                .ToList();

            var totalTrips = _context.Trips.Count();
            var allPages = (int)Math.Ceiling(totalTrips / (double)pageSize);

            return Ok(new
            {
                pageNum = page,
                pageSize = pageSize,
                allPages = allPages,
                trips = trips
            });
        }

        // POST: api/trips/{idTrip}/clients
        [HttpPost("{idTrip}/clients")]
        public IActionResult AssignClientToTrip(int idTrip, [FromBody] ClientDto clientDto)
        {
            if (_context.Clients.Any(c => c.Pesel == clientDto.Pesel))
            {
                return BadRequest("Client with this PESEL already exists.");
            }

            if (_context.ClientTrips.Any(ct => ct.Client.Pesel == clientDto.Pesel && ct.TripId == idTrip))
            {
                return BadRequest("Client already on this trip.");
            }

            var trip = _context.Trips.Find(idTrip);
            if (trip == null || trip.DateFrom <= DateOnly.FromDateTime(DateTime.Now))
            {
                return BadRequest("Trip does not exist or has already started.");
            }

            var client = new Client
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                Telephone = clientDto.Telephone,
                Pesel = clientDto.Pesel
            };

            _context.Clients.Add(client);
            _context.ClientTrips.Add(new ClientTrip
            {
                Client = client,
                Trip = trip,
                PaymentDate = clientDto.PaymentDate,
                RegisteredAt = DateTime.Now
            });

            _context.SaveChanges();

            return Ok();
        }
    }
}
