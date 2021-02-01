using System;
using Flunt.Validations;

namespace Usuarios.BL.ValueObjects
{
    public class Email : ValueObject
    {
        public string EmailAddress { get; private set; }

        public Email(string emailAddress)
        {
            EmailAddress = emailAddress;

            AddNotifications(new Contract()
                                .Requires()
                                .IsEmail(EmailAddress, "EmailAddress", "Email inválido")
                            );
        }
    }
}
