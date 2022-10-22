using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backend;


namespace Trabajo_programacion
{
    public partial class Luminiscencia : Form
    {
        Habitacion habitacion = new Habitacion();

        Calculos Calculos = new Calculos();

        public DataTable dtLuminiscencia = new DataTable() { TableName = "Luminiscencia" };
        const string ERROR_CAMPO = "Llene los campos";
        const string ERROR_FILA = "Seleccione una fila";
        const string ERROR_NOMBRE = "Nombre repetido";
        const string ERROR_COMAS = "Solo una ,(coma) por campo";
        const string ERROR_LABELS = "Debe terminar los calculos";

        string path = "";
        string path2 = "";

        public Luminiscencia()
        {
            InitializeComponent();

            dtLuminiscencia.Columns.Add("Nombre");
            dtLuminiscencia.Columns.Add("Ilum. Sol.");
            dtLuminiscencia.Columns.Add("Lampara");
            dtLuminiscencia.Columns.Add("Iluminancia");
            dtLuminiscencia.Columns.Add("Lamp. a lo Largo");
            dtLuminiscencia.Columns.Add("Lamp. a lo Ancho");
            dtLuminiscencia.Columns.Add("Lamp. Totales");
            dtLuminiscencia.Columns.Add("Sep. Lamp. Largo");
            dtLuminiscencia.Columns.Add("Sep. Lamp. Ancho");
            dtLuminiscencia.Columns.Add("Sep. Lamp. Largo pared");
            dtLuminiscencia.Columns.Add("Sep. Lamp. Ancho pared");

            dgvLuminiscencia.DataSource = dtLuminiscencia;

            Consumos.dtConsumos.Columns.Add("Habitacion");
            Consumos.dtConsumos.Columns.Add("Consumo Unitario");
            Consumos.dtConsumos.Columns.Add("Consumo Total");

            var dir = Path.Combine(Environment
           .GetFolderPath(Environment.SpecialFolder.ApplicationData), "Luminiscenicia");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            path = Path.Combine(dir, "Habitaciones.xml");
            path2 = Path.Combine(dir, "Consumo.xml");

            LeerDatos();
        }

        // Resultado del indice K

        private void btnIndK_Click(object sender, EventArgs e)
        {
            if (txtLargo.Text.Length == 0 || txtAncho.Text.Length == 0 || txtAltura.Text.Length == 0)
            {
                MessageBox.Show(this, ERROR_CAMPO, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (ValidarComas(this.Controls))
            {
                MessageBox.Show(this, ERROR_COMAS, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                lblIndK.Text = Calculos.IndiceK(txtLargo.Text, txtAncho.Text, txtAltura.Text);

            }

        }

        // Resultado de flujo luminoso

        private void btnFlujoLum_Click(object sender, EventArgs e)
        {
            if (txtLargo.Text.Length == 0 || txtAncho.Text.Length == 0 || txtIlumSol.Text.Length == 0 || txtMant.Text.Length == 0 || txtUtiliz.Text.Length == 0)
            {
                MessageBox.Show(this, ERROR_CAMPO, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ValidarComas(this.Controls))
            {
                MessageBox.Show(this, ERROR_COMAS, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                lblFlujoLum.Text = Calculos.FlujoLuminoso(txtLargo.Text, txtAncho.Text, txtIlumSol.Text, txtMant.Text, txtUtiliz.Text);
            }
        }

        // Resultado de Iluminancia

        private void btnIlum_Click(object sender, EventArgs e)
        {
            if (txtLargo.Text.Length == 0 || txtAncho.Text.Length == 0 || txtLumenes.Text.Length == 0 || txtMant.Text.Length == 0 || txtUtiliz.Text.Length == 0 || txtPotencia.Text.Length == 0)
            {
                MessageBox.Show(this, ERROR_CAMPO, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ValidarComas(this.Controls))
            {
                MessageBox.Show(this, ERROR_COMAS, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                lblLumOblg.Text = Calculos.NumLamp(txtLumenes.Text);

                lblIlum.Text = Calculos.Iluminancia(txtPotencia.Text, txtLumenes.Text, txtMant.Text, txtUtiliz.Text, txtLargo.Text, txtAncho.Text);


                string[] pepe = Calculos.LampFinales(txtAncho.Text, txtLargo.Text, lblLumOblg.Text, txtPotencia.Text);
                lblLampAncho.Text = pepe[0];
                lblLampLargo.Text = pepe[1];
                lblSepLampLargo.Text = pepe[2];
                lblSepLampAncho.Text = pepe[3];
                lblSepParedL.Text = pepe[4];
                lblSepParedA.Text = pepe[5];
                lblLampTotal.Text = pepe[6];
                lblPotTotal.Text = pepe[7];

            }


        }

        // validacion de valores escritos

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar) && (e.KeyChar != ',')))
            {
                e.Handled = true;
            }

        }

        // Limpia los valores de la calculadora

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);
            lblIndK.Text = "";
            lblFlujoLum.Text = "";
            lblLumOblg.Text = "";
            lblIlum.Text = "";
            lblLampTotal.Text = "";
            lblPotTotal.Text = "";
            lblLampLargo.Text = "";
            lblLampAncho.Text = "";
            lblSepLampAncho.Text = "";
            lblSepLampLargo.Text = "";
            lblSepParedA.Text = "";
            lblSepParedL.Text = "";
        }

        // Carga en el datagrid los valores calculados

        private void btnCargar_Click(object sender, EventArgs e)
        {
            // se validan los valores
            if (!ValidarTabla(this.Controls))
            {
                // Se crea la habitacion

                CrearHabitacion();

                // Se añaden filas a las tablas

                Consumos.dtConsumos.Rows.Add();
                dtLuminiscencia.Rows.Add();
                int i = dtLuminiscencia.Rows.Count - 1;

                // Se le dan valores a los campos de la tabla
                
                Consumos.dtConsumos.Rows[i]["Habitacion"] = habitacion.Nombre;
                Consumos.dtConsumos.Rows[i]["Consumo Unitario"] = habitacion.Potencia;
                Consumos.dtConsumos.Rows[i]["Consumo Total"] = habitacion.PotenciaTotal;

                dtLuminiscencia.Rows[i]["Nombre"] = habitacion.Nombre;
                dtLuminiscencia.Rows[i]["Ilum. Sol."] = habitacion.IlumSol;
                dtLuminiscencia.Rows[i]["Lampara"] = habitacion.NombreLamp;
                dtLuminiscencia.Rows[i]["Iluminancia"] = habitacion.Iluminancia;
                dtLuminiscencia.Rows[i]["Lamp. a lo Largo"] = habitacion.NLampLargo;
                dtLuminiscencia.Rows[i]["Lamp. a lo Ancho"] = habitacion.NLampAncho;
                dtLuminiscencia.Rows[i]["Lamp. Totales"] = habitacion.NDeLuminariasTotal;
                dtLuminiscencia.Rows[i]["Sep. Lamp. Largo"] = habitacion.SepEntreLampLargo;
                dtLuminiscencia.Rows[i]["Sep. Lamp. Ancho"] = habitacion.SepEntreLampAncho;
                dtLuminiscencia.Rows[i]["Sep. Lamp. Largo pared"] = habitacion.SepEntreLampParedLargo;
                dtLuminiscencia.Rows[i]["Sep. Lamp. Ancho pared"] = habitacion.SepEntreLampParedAncho;



                GuardarDatos();
            }


        }

        // Borra la fila seleccionada en los 2 datagrid

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Valida si se selecciono una fila
            if (dgvLuminiscencia.CurrentRow == null)
            {
                MessageBox.Show(this, ERROR_FILA, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int i = dgvLuminiscencia.CurrentRow.Index;

                Consumos.dtConsumos.Rows.RemoveAt(i);

                dgvLuminiscencia.Rows.Remove(dgvLuminiscencia.CurrentRow);
            }

            GuardarDatos();
        }

        // Abre un nuevo form con la tabla de consumo

        private void btnConsumo_Click(object sender, EventArgs e)
        {
            Consumos consumos = new Consumos();
            consumos.Show();
        }

        // Metodo para guardar datos

        private void GuardarDatos()
        {
            dtLuminiscencia.WriteXml(path);
            Consumos.dtConsumos.WriteXml(path2);
        }


        // Valida que no se escriba mas de una coma por textbox

        private bool ValidarComas(Control.ControlCollection ctrlCollection)
        {
            char myChar = ',';
            int i = 0;
            bool comas = false;

            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    if (!comas)
                    {
                        foreach (char c in ctrl.Text)
                        {
                            if (c == myChar)
                            {
                                i++;
                                if (i > 1)
                                {
                                    comas = true;
                                    break;
                                }

                            }
                        }
                        i = 0;

                    }
                }

            }

            return comas;
        }

      

        // Validacion de los valores que se intentan cargar al datagrid

        private bool ValidarTabla(Control.ControlCollection ctrlCollection)
        {
            bool bandera = false;

            // Validar campos vacios para datagrid

            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    if (!ctrl.Text.Any())
                    {
                        bandera = true;
                        MessageBox.Show(this, ERROR_CAMPO, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    };
                }
                if (lblIlum.Text == "")
                {
                    bandera = true;
                    MessageBox.Show(this, ERROR_LABELS, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }

            // Validar que no se repita el nombre para datagrid

            for (int i = 0; i < dtLuminiscencia.Rows.Count; i++)
            {
                if (dtLuminiscencia.Rows[i]["Nombre"].ToString() == txtNombre.Text)
                {
                    bandera = true;
                    MessageBox.Show(this, ERROR_NOMBRE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }





            return bandera;

        }

        // Metodo para leer datos

        private void LeerDatos()
        {
            if (System.IO.File.Exists(path))
            {
                dtLuminiscencia.ReadXml(path);
                Consumos.dtConsumos.ReadXml(path2);
            }
        }

        // Valores de Habitacion

        private void CrearHabitacion()
        {
            habitacion.Nombre = txtNombre.Text;
            habitacion.Ancho = Convert.ToDouble(txtAncho.Text);
            habitacion.Largo = Convert.ToDouble(txtLargo.Text);
            habitacion.Altura = Convert.ToDouble(txtAltura.Text);
            habitacion.IlumSol = Convert.ToDouble(txtIlumSol.Text);
            habitacion.IndiceK = Convert.ToDouble(lblIndK.Text);
            habitacion.ReflexTecho = Convert.ToDouble(txtRefTecho.Text);
            habitacion.ReflexSuelo = Convert.ToDouble(txtRefSuelo.Text);
            habitacion.ReflexPared = Convert.ToDouble(txtRefPared.Text);
            habitacion.CoefMant = Convert.ToDouble(txtMant.Text);
            habitacion.CoefUtiliz = Convert.ToDouble(txtUtiliz.Text);
            habitacion.FlujoLuminoso = Convert.ToDouble(lblFlujoLum.Text);
            habitacion.NombreLamp = txtLampNomb.Text;
            habitacion.Potencia = Convert.ToDouble(txtPotencia.Text);
            habitacion.Lumenes = Convert.ToDouble(txtLumenes.Text);
            habitacion.NDeLuminariasOblg = Convert.ToDouble(lblLumOblg.Text);
            habitacion.Iluminancia = Convert.ToDouble(txtIlumSol.Text);
            habitacion.NDeLuminariasTotal = Convert.ToDouble(lblLampTotal.Text);
            habitacion.NLampAncho = Convert.ToDouble(lblLampAncho.Text);
            habitacion.NLampLargo = Convert.ToDouble(lblLampLargo.Text);
            habitacion.SepEntreLampAncho = Convert.ToDouble(lblSepLampAncho.Text);
            habitacion.SepEntreLampLargo = Convert.ToDouble(lblSepLampLargo.Text);
            habitacion.SepEntreLampParedAncho = Convert.ToDouble(lblSepLampAncho.Text);
            habitacion.SepEntreLampParedLargo = Convert.ToDouble(lblSepLampLargo.Text);
            habitacion.PotenciaTotal = Convert.ToDouble(lblPotTotal.Text);
        }

        // Metodo para limpiar textboxs
        public void ClearTextBoxes(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Text = String.Empty;
                }
                else
                {
                    ClearTextBoxes(ctrl.Controls);
                }
            }

        }
    }
}
