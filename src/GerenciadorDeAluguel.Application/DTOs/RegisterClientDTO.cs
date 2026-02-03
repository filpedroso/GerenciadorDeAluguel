namespace GerenciadorDeAluguel.Application.DTOs
{
    public class RegisterClientDTO
    {
        public string Name { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    
        // Optional address
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Complement { get; set; }
    }
}
