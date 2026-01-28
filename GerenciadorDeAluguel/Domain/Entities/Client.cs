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
            Name = name ?? throw new ArgumentNullException(nameof(name), "Client must have a name");
            Email = email ?? throw new ArgumentNullException(nameof(email), "Client must have an email");
            Phone = phone ?? throw new ArgumentNullException(nameof(phone), "Client must have a phone number");
            CpfOrCnpj = cpfOrCnpj ?? throw new ArgumentNullException(nameof(cpfOrCnpj), "Client must have a CPF or CNPJ");
            Id = Guid.NewGuid();
            ResidentialAddress = residentialAddress;
        }
    }
}
