using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DataLayer;


namespace Project_WindowsFormPart02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string err = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            Cls_Database data = new Cls_Database(Cls_Main.pathconn, false);
            if(data.KiemTraKetNoi(ref err))
            {
                MessageBox.Show("Thanh cong\n"+data.conn);
            }
            else
            {
                MessageBox.Show(err);
            }
        }
       
    }
}
