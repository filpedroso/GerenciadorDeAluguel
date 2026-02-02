// Tests/Builders/TestDataBuilder.cs
using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Tests.Builders
{
    public static class TestDataBuilder
    {
        // VALUE OBJECTS
        public static Document CreateValidCpf()
        {
            var cpf = "08903320611";
            return new Document(cpf);
        }

        public static Document CreateValidCnpj()
        {
            var cnpj = "42960849000319";
            return new Document(cnpj);
        }

        public static Address CreateValidAddress(
            string street = "Rua das Laranjeiras",
            string number = "100",
            string zipCode = "31234-567",
            string city = "Belo Horizonte",
            string state = "MG"
        )
        {
            return new Address(street, number, zipCode, city, state);
        }

        public static Money CreateValidMoney(decimal amount = 1200.00m)
        {
            return new Money(amount);
        }

        public static Period CreateValidPeriod(DateTime? start = null, DateTime? end = null)
        {
            var startDate = start ?? new DateTime(2026, 2, 1);
            var endDate = end ?? new DateTime(2026, 2, 28);
            return new Period(startDate, endDate);
        }

        // DOMAIN ENTITIES

        public static Client CreateValidClient(
            string name = "Maria Souza",
            string email = "maria@email.com",
            string phone = "31988887777",
            string document = "71250557315"
        )
        {
            return new Client(name, email, phone, document);
        }

        public static Client CreateValidTenant(
            string name = "Jacare da Silva",
            string email = "jacare@email.com",
            string phone = "31987872323",
            string document = "08903320611"
        )
        {
            return new Client(name, email, phone, document);
        }

        public static Client CreateValidOwner(
            string name = "João Dono",
            string email = "joao@email.com",
            string phone = "31988889999",
            string document = "42960849000319"
        )
        {
            return new Client(name, email, phone, document);
        }

        public static Property CreateValidProperty(
            Client? owner = null,
            Address? address = null,
            Money? rent = null,
            PropertyType type = PropertyType.Apartment
        )
        {
            var propertyOwner = owner ?? CreateValidOwner();
            var propertyAddress = address ?? CreateValidAddress();
            var propertyRent = rent ?? CreateValidMoney();

            return new Property(propertyOwner, propertyAddress, propertyRent, type);
        }

        public static Reservation CreateValidReservation(
            Client? tenant = null,
            Property? property = null,
            Period? period = null,
            Money? rent = null
        )
        {
            var reservationTenant = tenant ?? CreateValidClient();
            var reservationProperty = property ?? CreateValidProperty();
            var reservationPeriod = period ?? CreateValidPeriod();
            var reservationRent = rent ?? CreateValidMoney();

            return new Reservation(
                reservationTenant,
                reservationProperty,
                reservationPeriod,
                reservationRent
            );
        }
    }
}
