using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyCRM
{
    [Table("Clients", Schema = "company")]
    public partial class Customers
    {
        [Key]
        [Column("id_Client")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_Client { get; set; }
        [Column("Nom")]
        public string Nom { get; set; }
        [Column("Cognom1")]
        public string Cognom1 { get; set; }
        [Column("Adreca")]
        public string Adreca { get; set; }
        [Column("Poblacio")]
        public string Poblacio { get; set; }
        [Column("Provincia")]
        public string Provincia { get; set; }
        [Column("Telefon")]
        public int Telefon { get; set; }
        [Column("Fax")]
        public int Fax { get; set; }
        [Column("E-mail")]
        public string Email { get; set; }
        [Column("Codi_Postal")]
        public string CodiPostal { get; set; }
    }
}
