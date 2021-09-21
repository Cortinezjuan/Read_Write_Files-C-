using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_Write_Read_Files
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void btnCrear_Click(object sender, EventArgs e)
        {
            Ctrl nuevo = new Ctrl();
            nuevo.CrearTXT();
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            Ctrl nuevo2 = new Ctrl();
            nuevo2.LeerTXT();
        }
    }
}
