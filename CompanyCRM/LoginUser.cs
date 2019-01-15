using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyCRM
{
    [Table("Login", Schema = "company")]
    public partial class LoginUser
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        [Column("user")]
        public string user { get; set; }
        [Column("password")]
        public string password { get; set; }
        [Column("role")]
        public string role { get; set; }
    }
}