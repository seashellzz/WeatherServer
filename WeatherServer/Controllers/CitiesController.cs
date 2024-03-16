﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountryModel;
using WeatherServer.Dto;

namespace WeatherServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController(CountriesSourceContext context) : ControllerBase
    {

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await context.Cities.ToListAsync();
        }

        [HttpGet("GetPopulation")]
        public async Task<ActionResult<IEnumerable<CountryPopulation>>> GetPopulation()
        {
            IQueryable<CountryPopulation> x = from c in context.Countries
                    select new CountryPopulation
                    {
                        countryName = c.Name,
                        countryId = c.CountryId,
                        //population = c.City.Sum(t => t.Population)  //a collection of cities 
                    };
            return await x.ToListAsync();
        }

        [HttpGet("GetPopulation2")]
        public async Task<ActionResult<IEnumerable<CountryPopulation>>> GetPopulation2()
        {
            IQueryable<CountryPopulation> x = context.Countries.Select(c => 
                                              new CountryPopulation
                                              {
                                                  countryName = c.Name,
                                                  countryId = c.CountryId,
                                                  //population = c.City.Sum(t => t.Population)  //a collection of cities 
                                              });
            return await x.ToListAsync();
        }
    }
}
