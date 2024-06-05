using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Models;
using SuperHeroApi.Repository;
using System.Runtime.CompilerServices;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        { 
            _context = context;
        }

        [HttpGet] // devuelve todos los heroes
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            var heroes = await _context.superheroes.ToListAsync();    
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = await _context.superheroes.FindAsync(id);
            if (hero == null) 
            {
                return NotFound("Heroe not found");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> AddHero(SuperHero hero)
        {
            if (hero is not null)
            {
                _context.superheroes.Add(hero);
                await _context.SaveChangesAsync();
                return Ok(hero);
            }
                   
            return BadRequest("Hero was null");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteHero(int id)
        {
            if (id >= 0)
            {
                var dbHero = await _context.superheroes.FindAsync(id);
                if (dbHero is not null)
                {
                    _context.superheroes.Remove(dbHero);
                    await _context.SaveChangesAsync();
                    return Ok($"Hero {dbHero.Name} with id={id} was deleted");
                }             
            }
            return BadRequest("Hero was not found");
        }
        [HttpPut]
        public async Task<ActionResult> UpdateHero(SuperHero updatedHero)
        {
            if (updatedHero is not null)
            {
                var dbHero = await _context.superheroes.FindAsync(updatedHero.Id);
                if(dbHero is not null)
                {
                    dbHero.Name = updatedHero.Name;
                    dbHero.FirstName = updatedHero.FirstName;
                    dbHero.LastName = updatedHero.LastName;
                    dbHero.Place = updatedHero.Place;
                    await _context.SaveChangesAsync();
                    return Ok($"Hero {updatedHero.Name} was updated");
                }
            }
            return BadRequest("Hero was null");
        }
    }
}
