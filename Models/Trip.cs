using System;
using System.Collections.Generic;

namespace APBD_8.Models;

public partial class Trip
{
    public int TripId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly DateFrom { get; set; }

    public DateOnly DateTo { get; set; }

    public int MaxPeople { get; set; }

    public virtual ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();

    public virtual ICollection<TripCountry> TripCountries { get; set; } = new List<TripCountry>();
}
