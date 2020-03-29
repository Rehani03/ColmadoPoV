using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POVColmado.Entidades
{
    public class DetalleFactura
    {
        [Key]
        public int DetalleFacturaId { get; set; }
        public int FacturaId { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
       
        public DetalleFactura()
        {
            DetalleFacturaId = 0;
            Descripcion = string.Empty;
            FacturaId = 0;
            Cantidad = 0;
            Precio = 0;

        }

        public DetalleFactura(int detalleFacturaID, string descripcion, int facturaID, int cantidad, decimal precio)
        {
            this.DetalleFacturaId = detalleFacturaID;
            this.Descripcion = descripcion;
            this.FacturaId = facturaID;
            this.Cantidad = cantidad;
            this.Precio = precio;

        }
    }
}
