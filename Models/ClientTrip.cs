using System;
using System.Collections.Generic;

namespace APBD_8.Models;

public partial class ClientTrip
{
    public int ClientTripId { get; set; }

    public int ClientId { get; set; }

    public int TripId { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public DateTime RegisteredAt { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Trip Trip { get; set; } = null!;
}
