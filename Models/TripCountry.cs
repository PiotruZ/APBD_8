using System;
using System.Collections.Generic;

namespace APBD_8.Models;

public partial class TripCountry
{
    public int TripCountryId { get; set; }

    public int TripId { get; set; }

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual Trip Trip { get; set; } = null!;
}
