﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models.Classrooms;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ElevesController : ControllerBase
    {
        private readonly WebContext _context;

        public ElevesController(WebContext context)
        {
            _context = context;
        }

        // GET: api/Eleves
        [HttpGet("{idClass}")]
        public async Task<ActionResult<IEnumerable<Eleve>>> GetEleve(int idClass)
        {
            if (await _context.Classroom.FindAsync(idClass) == null)
                return NotFound(idClass);

            return await _context.Eleve
                .Where(x => x.IdClasse == idClass).ToListAsync();
        }

        // GET: api/Eleves/5
        [HttpGet("{idClass}/{id}")]
        public async Task<ActionResult<Eleve>> GetEleve(int idClass, int id)
        {
            if (await _context.Classroom.FindAsync(idClass) == null)
                return NotFound(idClass);

            var listElevByClass = await _context.Eleve.Where(x => x.IdClasse == idClass).ToListAsync();
            var eleve = listElevByClass.Find(x => x.Id == id);

            if (eleve == null)
            {
                return NotFound();
            }

            return eleve;
        }

        // PUT: api/Eleves/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEleve(int id, Eleve eleve)
        {
            if (id != eleve.Id)
            {
                return BadRequest();
            }

            _context.Entry(eleve).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EleveExists(id))
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

        // POST: api/Eleves
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("{idClass}")]
        public async Task<ActionResult<Eleve>> PostEleve(int idClass, Eleve eleve)
        {
            if (await _context.Classroom.FindAsync(idClass) == null)
                return NotFound(idClass);

            eleve.IdClasse = idClass;

            _context.Eleve.Add(eleve);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEleve", new { idClass, id = eleve.Id }, eleve);
        }

        // DELETE: api/Eleves/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Eleve>> DeleteEleve(int id)
        {
            var eleve = await _context.Eleve.FindAsync(id);
            if (eleve == null)
            {
                return NotFound();
            }

            RemoveEleve(eleve);

            await _context.SaveChangesAsync();

            return eleve;
        }

        public void RemoveEleve(Eleve ev)
        {
            _context.Eleve.Remove(ev);

            //Supprimer les activités par élèves pour chaques suppression d'élève
            //LDN : TODO
            // ----------------------------------------------------- 
        }
        private bool EleveExists(int id)
        {
            return _context.Eleve.Any(e => e.Id == id);
        }
    }
}
