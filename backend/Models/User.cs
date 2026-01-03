using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("USERS")]
    public class User
    {
        [Column("ID")]
        public int Id {get;set;}

        [Column("NAME")]
        public string Name {get;set;} = null!;

        [Column("EMAIL")]
        public string Email {get;set;} = null!;

        [Column("PASSWORD_HASH")]
        public string PasswordHash {get;set;} = null!;

        [Column("CREATION_DATE")]
        public DateTime CreationDate {get;set;}

        [Column("STATUS")]
        public string Status {get;set;} = null!;
    }
}