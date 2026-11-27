using System;
using System.Globalization;
using System.Windows.Forms;

namespace Pedro_Nominal_Quincenal
{
    public partial class Form1 : Form
    {
       
        
        private const int HORAS_REGULARES = 88;          
        private const decimal RECARGO_EXTRA = 1.35m;     
        private const decimal INCENTIVO_TRANSPORTE = 1500m;
        private const decimal TASA_AFP = 0.0287m;       
        private const decimal TASA_SFS = 0.0304m;        

        
        private const int HORAS_MIN = 1;
        private const int HORAS_MAX = 120;

        
        private const string CARGO_AUXILIAR = "Auxiliar";
        private const string CARGO_TECNICO = "Técnico";
        private const string CARGO_SUPERVISOR = "Supervisor";
        private const string CARGO_GERENTE = "Gerente";

        
        private const decimal TARIFA_AUXILIAR = 180m;
        private const decimal TARIFA_TECNICO = 250m;
        private const decimal TARIFA_SUPERVISOR = 350m;
        private const decimal TARIFA_GERENTE = 500m;

        
        private static readonly CultureInfo Cultura = CultureInfo.InvariantCulture;

        
        private int cantidadEmpleados = 0;
        private decimal totalBruto = 0m;
        private decimal totalDeducciones = 0m;
        private decimal totalNeto = 0m;

        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbCargo.Items.Add(CARGO_AUXILIAR);
            cmbCargo.Items.Add(CARGO_TECNICO);
            cmbCargo.Items.Add(CARGO_SUPERVISOR);
            cmbCargo.Items.Add(CARGO_GERENTE);

            LimpiarCampos();
            ReiniciarResumen();   
        }

        
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            
            
            string nombre = txtEmpleado.Text.Trim();
            if (nombre == "")
            {
                MessageBox.Show("Escribe el nombre del empleado.", "Dato requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmpleado.Focus();
                return;
            }

            
            if (!rdbFijo.Checked && !rdbTemporal.Checked)
            {
                MessageBox.Show("Selecciona el tipo de contrato (Fijo o Temporal).", "Dato requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (cmbCargo.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un cargo.", "Dato requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCargo.Focus();
                return;
            }

           
            int horas;
            if (!int.TryParse(txtHoras.Text.Trim(), out horas) || horas < HORAS_MIN || horas > HORAS_MAX)
            {
                MessageBox.Show("Las horas deben ser un número entero entre " + HORAS_MIN +
                    " y " + HORAS_MAX + ".", "Horas inválidas",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoras.Focus();
                txtHoras.SelectAll();
                return;
            }

            
            string cargo = cmbCargo.SelectedItem.ToString();

            
            decimal tarifa = 0m;
            if (cargo == CARGO_AUXILIAR)
            {
                tarifa = TARIFA_AUXILIAR;
            }
            else if (cargo == CARGO_TECNICO)
            {
                tarifa = TARIFA_TECNICO;
            }
            else if (cargo == CARGO_SUPERVISOR)
            {
                tarifa = TARIFA_SUPERVISOR;
            }
            else if (cargo == CARGO_GERENTE)
            {
                tarifa = TARIFA_GERENTE;
            }

            
            int horasRegulares = horas;
            int horasExtra = 0;
            if (horas > HORAS_REGULARES)
            {
                horasRegulares = HORAS_REGULARES;
                horasExtra = horas - HORAS_REGULARES;
            }

            decimal salarioRegular = horasRegulares * tarifa;
            decimal pagoExtra = horasExtra * tarifa * RECARGO_EXTRA;

            
            decimal incentivo = 0m;
            if (chkTransporte.Checked)
            {
                incentivo = INCENTIVO_TRANSPORTE;
            }

            decimal bruto = salarioRegular + pagoExtra + incentivo;

            
            decimal deducciones = 0m;
            if (rdbFijo.Checked)
            {
                deducciones = (salarioRegular + pagoExtra) * (TASA_AFP + TASA_SFS);
            }

            decimal neto = bruto - deducciones;

           
            lstNomina.Items.Add(nombre + " · " + cargo + " · " + Moneda(neto));

            
            cantidadEmpleados++;
            totalBruto += bruto;
            totalDeducciones += deducciones;
            totalNeto += neto;

            
            LimpiarCampos();
        }

        
        private void btnResumen_Click(object sender, EventArgs e)
        {
            
            if (cantidadEmpleados == 0)
            {
                MessageBox.Show("Aún no hay empleados registrados.", "Sin datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lblEmpleados.Text = cantidadEmpleados.ToString();
            lblBruto.Text = Moneda(totalBruto);
            lblDeducciones.Text = Moneda(totalDeducciones);
            lblNeto.Text = Moneda(totalNeto);
        }

        
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            
            lstNomina.Items.Clear();
            cantidadEmpleados = 0;
            totalBruto = 0m;
            totalDeducciones = 0m;
            totalNeto = 0m;

            
            ReiniciarResumen();

            LimpiarCampos();
        }

        
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

       

        
        private void LimpiarCampos()
        {
            txtEmpleado.Clear();
            rdbFijo.Checked = true;      
            cmbCargo.SelectedIndex = -1;
            txtHoras.Clear();
            chkTransporte.Checked = false;
            txtEmpleado.Focus();
        }

        
        private void ReiniciarResumen()
        {
            lblEmpleados.Text = "0";
            lblBruto.Text = Moneda(0m);
            lblDeducciones.Text = Moneda(0m);
            lblNeto.Text = Moneda(0m);
        }

        
        private string Moneda(decimal valor)
        {
            return "RD$ " + valor.ToString("N2", Cultura);
        }
    }
}