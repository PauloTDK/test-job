using System;

namespace Usuarios.DAL.Repositories
{
    public class UsuarioRepository :IUsuarioRepository
    {
        private readonly UsuarioContext _context;

        public UsuarioRepository(UsuarioContext context)
        {
            _context = context;
        }
    }
}
