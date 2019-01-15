using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyCRM
{
    [Table("Productes", Schema = "company")]
    public partial class Productes
    {
        [Key]  
        [Column("id_Producte")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_Producte { get; set; }
        [Column("Producte")]
        public string Producte { get; set; }
        [Column("Preu")]
        public double Preu { get; set; }
    }
}
