using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backend;

namespace Trabajo_programacion
{
    public partial class Luminiscencia : Form
    {

        Calculos Calculos = new Calculos();

        DataTable dtLuminiscencia = new DataTable() { TableName = "Luminiscencia" };
        const string ERROR_CAMPO = "Llene los campos";
        const string ERROR_FILA = "Seleccione una fila";
        const string ERROR_NOMBRE = "Nombre repetido";
        const string ERROR_COMAS = "Solo una coma por campo";

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
                lblFlujoLum.Text=ERROR_CAMPO;
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

        // Carga en el datagrid los valores importantes

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (!ValidarTabla(this.Controls))
            {
                dtLuminiscencia.Rows.Add();
                int i = dtLuminiscencia.Rows.Count - 1;

                dtLuminiscencia.Rows[i]["Nombre"] = txtNombre.Text;
                dtLuminiscencia.Rows[i]["Ilum. Sol."] = txtIlumSol.Text;
                dtLuminiscencia.Rows[i]["Lampara"] = txtLampNomb.Text;
                dtLuminiscencia.Rows[i]["Iluminancia"] = lblIlum.Text;
                dtLuminiscencia.Rows[i]["Lamp. a lo Largo"] = lblLampLargo.Text;
                dtLuminiscencia.Rows[i]["Lamp. a lo Ancho"] = lblLampAncho.Text;
                dtLuminiscencia.Rows[i]["Lamp. Totales"] = lblLampTotal.Text;
                dtLuminiscencia.Rows[i]["Sep. Lamp. Largo"] = lblSepLampLargo.Text;
                dtLuminiscencia.Rows[i]["Sep. Lamp. Ancho"] = lblSepLampAncho.Text;
                dtLuminiscencia.Rows[i]["Sep. Lamp. Largo pared"] = lblSepParedL.Text;
                dtLuminiscencia.Rows[i]["Sep. Lamp. Ancho pared"] = lblSepParedA.Text;
            }


        }

        // Valida la cantidad de comas

        private bool ValidarComas(Control.ControlCollection ctrlCollection)
        {
            char myChar = ',';
            int i = 0;
            bool comas = false;
            // Validar campos vacios para datagrid

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
                           // else { i = 0; comas = false; }

                        }
                        i = 0;

                    }
                }
              
            }
            
            return comas;
        }

        // Borra la fila seleccionada en el datagrid

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvLuminiscencia.CurrentRow == null)
            {
                MessageBox.Show(this, ERROR_FILA, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dgvLuminiscencia.Rows.Remove(dgvLuminiscencia.CurrentRow);
            }
        }

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

            }

            // Validar la repeticion del nombre para datagrid

            for (int i = 0; i < dtLuminiscencia.Rows.Count; i++)
            {
                if (dtLuminiscencia.Rows[i]["Codigo"].ToString() == txtNombre.Text)
                {
                    bandera = true;
                    MessageBox.Show(this, ERROR_NOMBRE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }




            return bandera;

        }
    }
}
