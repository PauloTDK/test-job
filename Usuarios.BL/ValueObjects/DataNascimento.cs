using System;
using Flunt.Validations;

namespace Usuarios.BL.ValueObjects
{
    public class DataNascimento : ValueObject
    {
        public DateTime Data { get; private set; }

        public DataNascimento(DateTime data)
        {
            Data = data;

            AddNotifications(new Contract()
                                .Requires()
                                .IsGreaterThan(DateTime.Today, data.Date, "DataNascimento", "Data de nascimento inválida")
                            );
        }
    }
}
