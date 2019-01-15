using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyCRM
{
    [Table("Factura_Detall", Schema = "company")]
    public partial class Factura_Detail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int n_Factura { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_Producte { get; set; }
        [Column("Quantitat")]
        public int quantitat { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    }
}
