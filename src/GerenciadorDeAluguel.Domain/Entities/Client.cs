using GerenciadorDeAluguel.Domain.Enums;
using GerenciadorDeAluguel.Domain.ValueObjects;

namespace GerenciadorDeAluguel.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Phone { get; }
        public Document DocumentNumber { get; }
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
            Name = name;
            Email = email;
            Phone = phone;
            DocumentNumber = documentNumber;
            Id = Guid.NewGuid();
            ResidentialAddress = residentialAddress;
        }
        public void UpdateAddress(Address address)
        {
            ResidentialAddress = address;
        }

        private Client()
        {
            Name = null!;
            Email = null!;
            Phone = null!;
            DocumentNumber = null!;
        }
    }
}
