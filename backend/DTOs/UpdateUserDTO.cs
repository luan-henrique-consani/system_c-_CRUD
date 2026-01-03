namespace backend.DTOs
{
    public class UpdateUserDTO
    {
        public string Name {get; set;} = null!;
        public string Email {get; set;} = null!;
        public string Status {get; set;} = null!;        
    }
}