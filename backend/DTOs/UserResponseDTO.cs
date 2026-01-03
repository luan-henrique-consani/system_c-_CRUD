namespace backend.DTOs
{
    public class UserResponseDTO
    {
        public int Id {get;set;}
        public string Name {get;set;} = null!;
        public string Email {get;set;} = null!;
        public DateTime CreationDate {get;set;}
        public string Status {get;set;} = null!;
    }
}