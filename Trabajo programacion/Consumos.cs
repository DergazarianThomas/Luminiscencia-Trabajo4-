using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabajo_programacion
{
    public partial class Consumos : Form
    {
        internal static DataTable dtConsumos = new DataTable() { TableName = "Consumos" };

        

        public Consumos()
        {
            InitializeComponent();

 
           dgvConsumo.DataSource = dtConsumos;
         
        }


    }

    
}
