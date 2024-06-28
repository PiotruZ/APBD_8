using System;
using System.Collections.Generic;

namespace APBD_8.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telephone { get; set; }

    public string Pesel { get; set; } = null!;

    public virtual ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
}

    public class ClientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Pesel { get; set; }
        public DateOnly? PaymentDate { get; set; }
    }

