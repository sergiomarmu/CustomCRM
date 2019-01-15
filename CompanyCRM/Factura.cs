using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyCRM
{
    [Table("Factura", Schema = "company")]
    public partial class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int n_Factura { get; set; }
        public int id_Client { get; set; }
        [Column("Data")]
        public string Data { get; set; }
        [Column("Descompte")]
        public int Descompte { get; set; }
        [Column("IVA")]
        public int IVA { get; set; }


        //[Required]
        //[ForeignKey("id_Client")]
        //public virtual Customers Customers { get; set; }
    }
}