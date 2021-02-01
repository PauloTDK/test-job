using System;
using Flunt.Validations;

namespace Usuarios.BL.ValueObjects
{
    public class Nome : ValueObject
    {
        public Nome(string primeiroNome, string sobreNome)
        {
            PrimeiroNome = primeiroNome;
            SobreNome = sobreNome;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(PrimeiroNome, 3, "PrimeiroNome", "Nome Inválido")
                .HasMinLen(SobreNome, 3, "SobreNome", "Sobrenome Inválido")
                );

        }

        public string PrimeiroNome { get; private set; }
        public string SobreNome { get; private set; }
    }
}
