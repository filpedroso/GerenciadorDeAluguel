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
        public string CpfOrCnpj { get; }
        public Address? ResidentialAddress { get; }

        public Client(string name, string email, string phone, string cpfOrCnpj, Address? residentialAddress = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Client must have a name", nameof(name));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Client must have an email", nameof(email));
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Client must have a phone number", nameof(phone));
            if (string.IsNullOrWhiteSpace(cpfOrCnpj))
                throw new ArgumentException("Client must have a CPF or CNPJ", nameof(cpfOrCnpj));
            Name = name;
            Email = email;
            Phone = phone;
            CpfOrCnpj = cpfOrCnpj;
            Id = Guid.NewGuid();
            ResidentialAddress = residentialAddress;
        }
    }
}
