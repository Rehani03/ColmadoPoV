using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POVColmado.Entidades
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }
        public string NombreCliente { get; set; }
        public string direccion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public decimal Efectivo { get; set; }
        public virtual List<DetalleFactura> Detalles { get; set; }


    }
}
