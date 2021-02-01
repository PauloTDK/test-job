using System;

namespace Usuarios.API
{
    public class UsuarioDto
    {
        public long? Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public EscolaridadeDto Escolaridade { get; set; }
    }

    public enum EscolaridadeDto
    {
        Infantil,
        Fundamental,
        Medio,
        Superior
    }
}
