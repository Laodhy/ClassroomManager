using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models.Classrooms;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatieresController : ControllerBase
    {
        private readonly WebContext _context;

        public MatieresController(WebContext context)
        {
            _context = context;
        }

        // GET: api/Matieres
        [HttpGet("{idClass}")]
        public async Task<ActionResult<IEnumerable<Matiere>>> GetMatiere(int idClass)
        {
            if (await _context.Classroom.FindAsync(idClass) == null)
                return NotFound(idClass);

            return await _context.Matiere
                .Where(x => x.IdClasse == idClass).ToListAsync();
        }

        // GET: api/Matieres/5
        [HttpGet("{idClass}/{id}")]
        public async Task<ActionResult<Matiere>> GetMatiere(int idClass, int id)
        {
            if (await _context.Classroom.FindAsync(idClass) == null)
                return NotFound(idClass);

            var listMatByClass = await _context.Matiere.Where(x => x.IdClasse == idClass).ToListAsync();
            var matiere = listMatByClass.Find(x => x.Id == id);

            if (matiere == null)
            {
                return NotFound();
            }

            return matiere;
        }

        // PUT: api/Matieres/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatiere(int id, Matiere matiere)
        {
            if (id != matiere.Id)
            {
                return BadRequest();
            }

            _context.Entry(matiere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatiereExists(id))
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

        // POST: api/Matieres
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("{idClass}")]
        public async Task<ActionResult<Matiere>> PostMatiere(int idClass, Matiere matiere)
        {
            if (await _context.Classroom.FindAsync(idClass) == null)
                return NotFound(idClass);

            matiere.IdClasse = idClass;

            _context.Matiere.Add(matiere);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatiere", new { idClass,  id = matiere.Id }, matiere);
        }

        // DELETE: api/Matieres/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Matiere>> DeleteMatiere(int id)
        {
            var matiere = await _context.Matiere.FindAsync(id);
            if (matiere == null)
            {
                return NotFound();
            }

            _context.Matiere.Remove(matiere);
            await _context.SaveChangesAsync();

            return matiere;
        }

        private bool MatiereExists(int id)
        {
            return _context.Matiere.Any(e => e.Id == id);
        }
    }
}
