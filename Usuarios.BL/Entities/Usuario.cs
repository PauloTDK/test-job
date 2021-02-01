using System;
using Flunt.Validations;
using Usuarios.BL.ValueObjects;

namespace Usuarios.BL.Entities
{
    public class Usuario : Entity
    {
        public long Id { get; private set; }
        public Nome Nome { get; private set; }
        public Email Email { get; private set; }
        public DataNascimento DataNascimento { get; private set; }
        public Escolaridade Escolaridade { get; private set; }

        public Usuario(long id, Nome nome, Email email, DataNascimento dataNascimento, Escolaridade escolaridade)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Escolaridade = escolaridade;

            if (nome.Invalid)
                AddNotifications(nome.Notifications);
            if (email.Invalid)
                AddNotifications(email.Notifications);
            if (dataNascimento.Invalid)
                AddNotifications(dataNascimento.Notifications);

            AddNotifications(new Contract()
                .Requires()
                .IsBetween((int)escolaridade,
                           (int)Escolaridade.Infantil, 
                           (int)Escolaridade.Superior,
                           "Escolaridade", "Escolaridade inválida")
                );
        }

        protected Usuario() { }
    }

    public enum Escolaridade
    {
        Infantil = 1,
        Fundamental = 2,
        Medio = 3,
        Superior = 4
    }
}
