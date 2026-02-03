// Tests/Builders/TestDataBuilder.cs
using GerenciadorDeAluguel.Domain.Entities;
using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Tests.Builders
{
    public static class TestDataBuilder
    {
        // VALID TEST DOCUMENTS (all pass modulo 11 validation)
        public static class ValidDocuments
        {
            // Valid CPFs
            public const string CPF_1 = "08903320611";
            public const string CPF_2 = "11144477735";
            public const string CPF_3 = "00000000191";
            
            // Valid CNPJs
            public const string CNPJ_1 = "42960849000319";
            public const string CNPJ_2 = "11222333000181";
            public const string CNPJ_3 = "00000000000191";
        }

        // VALUE OBJECTS
        public static Document CreateValidCpf(string? cpf = null)
        {
            return new Document(cpf ?? ValidDocuments.CPF_1);
        }

        public static Document CreateValidCnpj(string? cnpj = null)
        {
            return new Document(cnpj ?? ValidDocuments.CNPJ_1);
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

        public static Period CreateValidPeriod(DateOnly? start = null, DateOnly? end = null)
        {
            var startDate = start ?? new DateOnly(2026, 2, 1);
            var endDate = end ?? new DateOnly(2026, 2, 28);
            return new Period(startDate, endDate);
        }

        // DOMAIN ENTITIES

        public static Client CreateValidClient(
            string name = "Maria Souza",
            string email = "maria@email.com",
            string phone = "31988887777",
            Document? document = null
        )
        {
            return new Client(
                name, 
                email, 
                phone, 
                document ?? CreateValidCpf()
            );
        }

        public static Client CreateValidTenant(
            string name = "Jacare da Silva",
            string email = "jacare@email.com",
            string phone = "31987872323",
            Document? document = null
        )
        {
            return new Client(
                name, 
                email, 
                phone, 
                document ?? CreateValidCpf(ValidDocuments.CPF_2)  // Use different CPF
            );
        }

        public static Client CreateValidOwner(
            string name = "João Dono",
            string email = "joao@email.com",
            string phone = "31988889999",
            Document? document = null
        )
        {
            return new Client(
                name, 
                email, 
                phone, 
                document ?? CreateValidCnpj()
            );
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
            var reservationTenant = tenant ?? CreateValidTenant();  // Changed to use Tenant
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
