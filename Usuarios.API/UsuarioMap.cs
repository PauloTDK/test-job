using System;
using Usuarios.BL.Entities;
using Usuarios.BL.ValueObjects;

namespace Usuarios.API
{
    public static class UsuarioMap
    {
        public static UsuarioDto UsuarioToDto(this Usuario user) 
        {
            return new UsuarioDto
            {
                Id = user.Id,
                Nome = user.Nome.PrimeiroNome,
                SobreNome = user.Nome.SobreNome,
                DataNascimento = user.DataNascimento.Data,
                Email = user.Email.EmailAddress,
                Escolaridade = (EscolaridadeDto)user.Escolaridade,
            };
        }

        public static Usuario UsuarioDtoToBL(this UsuarioDto user) 
        {
            return new Usuario
            (
                user.Id.GetValueOrDefault(),
                new Nome(user.Nome, user.SobreNome),
                new Email(user.Email),
                new DataNascimento(user.DataNascimento),
                (Escolaridade)user.Escolaridade
            );
        }
    }
}
