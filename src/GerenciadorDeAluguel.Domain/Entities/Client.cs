using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public Document DocumentNumber { get; private set; }
        public Address? ResidentialAddress { get; private set; }

        public Client(
            string name,
            string email,
            string phone,
            Document documentNumber,
            Address? residentialAddress = null
        )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Client must have a name", nameof(name));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Client must have an email", nameof(email));
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Client must have a phone number", nameof(phone));
            if (documentNumber == null)
                throw new ArgumentNullException(nameof(documentNumber), "Client must have a CPF or CNPJ");
            
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Phone = phone;
            DocumentNumber = documentNumber;
            ResidentialAddress = residentialAddress;
        }

        public void UpdateAddress(Address address)
        {
            ResidentialAddress = address;
        }

        #pragma warning disable CS8618
        private Client() { }  // EF Core only
        #pragma warning restore CS8618
    }
}
