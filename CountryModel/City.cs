using System;
using System.Collections.Generic;

namespace CountryModel;

public partial class City
{
    public int CityId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public int Population { get; set; }

    public virtual Country Country { get; set; } = null!;
}
