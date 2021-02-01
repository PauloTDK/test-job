using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Usuarios.BL.Entities;
using Usuarios.DAL;

namespace Usuarios.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly UsuarioContext _context;

        public UsuariosController(ILogger<UsuariosController> logger,
                                  UsuarioContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var resp = await _context.Usuarios
                                    .AsNoTracking()
                                    .ToListAsync();
            if (resp == null || !resp.Any())
                return NoContent();
            
            return Ok(resp.Select(o => o.UsuarioToDto()));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var user = await _context.Usuarios
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();

            return Ok(user.UsuarioToDto());
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(UsuarioDto user)
        {
            var usuario = user.UsuarioDtoToBL();
            if (!usuario.Valid)
                return BadRequest(usuario.NotificationsAsText());

            await _context.Usuarios.AddAsync(usuario);
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(UsuarioDto userDto)
        {
            var usuario = userDto.UsuarioDtoToBL();
            if (!usuario.Valid)
                return BadRequest(usuario.NotificationsAsText());

            var user = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userDto.Id);
            if (user == null)
                return NotFound();

            _context.Usuarios.Update(usuario);
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long userId)
        {
            var user = await _context.Usuarios.FindAsync(userId);
            if (user == null)
                return NotFound();

            _context.Usuarios.Remove(user);
            return Ok(await _context.SaveChangesAsync());
        }

    }
}
