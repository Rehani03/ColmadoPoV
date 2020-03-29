using POVColmado.BLL;
using POVColmado.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POVColmado.UI.Registros
{
    public partial class rVenta : Form
    {
        private List<DetalleFactura> Detalle { get; set; }
        private decimal TOTAL = 0;
        private decimal EFECTIVO = 0;
        private decimal DEVUELTA = 0;
        public rVenta()
        {
            InitializeComponent();
            this.DetalledataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Detalle = new List<DetalleFactura>();
            CantidadnumericUpDown.Maximum = 100000;
            PrecionumericUpDown.Maximum = 1000000;
        }

        private void Limpiar()
        {
            MyerrorProvider.Clear();
            this.Detalle = new List<DetalleFactura>();
            IDnumericUpDown.Value = 0;
            FechadateTimePicker.Value = DateTime.Now;
            NombrestextBox.Text = string.Empty;
            DirecciontextBox.Text = string.Empty;
            TOTAL = 0;
            EFECTIVO = 0;
            DEVUELTA = 0;
            DescripciontextBox.Text = string.Empty;
            PrecionumericUpDown.Value = 0;
            CantidadnumericUpDown.Value = 0;
        }

        private Factura LlenaClase()
        {
            Factura factura = new Factura();
            factura.FacturaId = Convert.ToInt32(IDnumericUpDown.Value);
            factura.Fecha = FechadateTimePicker.Value;
            factura.NombreCliente = NombrestextBox.Text;
            factura.direccion = DirecciontextBox.Text;
            factura.Detalles = this.Detalle;
            factura.Total = TOTAL;
            factura.Efectivo = EFECTIVO;
            return factura;
        }

        private void LlenaCampos(Factura f)
        {
            IDnumericUpDown.Value = f.FacturaId;
            NombrestextBox.Text = f.NombreCliente;
            DirecciontextBox.Text = f.direccion;
            this.Detalle = f.Detalles;
            TotaltextBox.Text = Convert.ToString(f.Total);
            TOTAL = f.Total;
            EfectivotextBox.Text = Convert.ToString(f.Efectivo);
            EFECTIVO = f.Efectivo;
            CargarGrid();
        }

        private void CalcularTotal()
        {
           
            decimal auxTotal = 0;
            foreach (var item in this.Detalle)
            {
                auxTotal += (item.Cantidad * item.Precio);
            }

            TotaltextBox.Text = Convert.ToString(auxTotal);
            TOTAL = auxTotal;
        }

        private void CargarGrid()
        {
            DetalledataGridView.DataSource = null;
            DetalledataGridView.DataSource = this.Detalle;
            DetalledataGridView.Columns["DetalleFacturaId"].Visible = false;
            DetalledataGridView.Columns["FacturaId"].Visible = false;
        }

        private bool Validar()
        {
            MyerrorProvider.Clear();
            bool paso = true;
            if (string.IsNullOrWhiteSpace(NombrestextBox.Text))
            {
                MyerrorProvider.SetError(NombrestextBox,"No puede estar vacio");
                paso = false;

            }

            if(this.Detalle.Count == 0)
            {
                MyerrorProvider.SetError(Agregarbutton, "Agregar un producto");
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(DirecciontextBox.Text))
            {
                MyerrorProvider.SetError(DirecciontextBox, "No puede estar vacio");
                paso = false;

            }

            if (string.IsNullOrWhiteSpace(EfectivotextBox.Text))
            {
                MyerrorProvider.SetError(EfectivotextBox, "No puede estar vacio");
                paso = false;
            }

            return paso;
        }

        private decimal GetDecimal()
        {
            decimal aux = 0;
            try
            {
                aux = Convert.ToDecimal(EfectivotextBox.Text);
                return aux;
            }
            catch (Exception)
            {
                //MessageBox.Show("El criterio debe ser numérico.", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MyerrorProvider.SetError(EfectivotextBox, "Este campo debe ser numerico.");
            }
            return aux;
        }

        private void Efectivolabel_Click(object sender, EventArgs e)
        {

        }

        private bool ValidarAgregar()
        {
            bool paso = true;
            MyerrorProvider.Clear();

            if (PrecionumericUpDown.Value == 0)
            {
                MyerrorProvider.SetError(PrecionumericUpDown, "Este campo no puede estar vacio.");
                paso = false;
            }

            if (CantidadnumericUpDown.Value == 0)
            {
                MyerrorProvider.SetError(CantidadnumericUpDown, "La cantidad no puede ser cero.");
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(DescripciontextBox.Text))
            {
                MyerrorProvider.SetError(DescripciontextBox, "Este campo no puede estar vacio.");
                paso = false;
            }
            return paso;
        }

        private bool Existe()
        {
            RepositorioFactura repositorio = new RepositorioFactura();
            Factura facturas = repositorio.Buscar((int)IDnumericUpDown.Value);
            return (facturas != null);
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            if (!Validar())
                return;
            bool paso;
            Factura facturas;
            RepositorioFactura repositorio = new RepositorioFactura();

            facturas = LlenaClase();

            var resultado = MessageBox.Show("Tu devuelta es: "+ calcularDevuelta()+" Es correcto?", "ButterSoft", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(resultado == DialogResult.Yes)
            {
                if (IDnumericUpDown.Value == 0)
                    paso = repositorio.Guardar(facturas);
                else
                {
                    if (!Existe())
                    {
                        MessageBox.Show("No se puede modificar porque no existe en la base de datos.",
                               "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    paso = repositorio.Modificar(facturas);
                }

                if (paso)
                {
                    MessageBox.Show("Guardado!!", "ColmadoPov", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("No fue posible guardar!!", "ButterSoft", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void Agregarbutton_Click(object sender, EventArgs e)
        {
            if (DetalledataGridView.DataSource != null)
                this.Detalle = (List<DetalleFactura>)DetalledataGridView.DataSource;
            if (!ValidarAgregar())
                return;
            this.Detalle.Add(new DetalleFactura(
                     detalleFacturaID: 0,
                     facturaID: Convert.ToInt32(IDnumericUpDown.Value),
                     descripcion: DescripciontextBox.Text,
                     cantidad: Convert.ToInt32(CantidadnumericUpDown.Value),
                     precio: Convert.ToDecimal(PrecionumericUpDown.Value)
               )
            ); ;
            MyerrorProvider.Clear();
            CargarGrid();
            DescripciontextBox.Text = string.Empty;
            PrecionumericUpDown.Value = 0;
            CantidadnumericUpDown.Value = 0;
            CalcularTotal();

        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(IDnumericUpDown.Value);
            Factura facturas = new Factura();
            RepositorioFactura repositorio = new RepositorioFactura();
            facturas = repositorio.Buscar(ID);

            if (facturas != null)
            {
                Limpiar();
                LlenaCampos(facturas);
            }
            else
            {
                MessageBox.Show("Factura no encontrada.");
            }
        }

        private void EfectivotextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void EfectivotextBox_TextChanged(object sender, EventArgs e)
        {

            
        }

        private decimal calcularDevuelta()
        {
            decimal efectivo = Convert.ToDecimal(EfectivotextBox.Text);
            decimal total = Convert.ToDecimal(TotaltextBox.Text);
            
            
            decimal auxDevuelta= Convert.ToDecimal(efectivo-total);


            return auxDevuelta;
        }
    }
}
