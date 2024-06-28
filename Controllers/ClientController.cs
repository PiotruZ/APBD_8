using APBD_8.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_8.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class ClientsController : ControllerBase
        {
            private readonly LocalDatabaseContext _context;

            public ClientsController(LocalDatabaseContext context)
            {
                _context = context;
            }

            // DELETE: api/clients/{idClient}
            [HttpDelete("{idClient}")]
            public IActionResult DeleteClient(int idClient)
            {
                var client = _context.Clients.Find(idClient);
                if (client == null)
                {
                    return NotFound();
                }

                if (_context.ClientTrips.Any(ct => ct.ClientId == idClient))
                {
                    return BadRequest("Client has trips assigned and cannot be deleted.");
                }

                _context.Clients.Remove(client);
                _context.SaveChanges();

                return NoContent();
            }
        }

}
