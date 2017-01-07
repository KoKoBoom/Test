using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Taki.Common;
using Taki.Logging;

namespace TestWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PrintException();
            }
            catch (Exception ex)
            {
                LoggerFactory.Create().Error(ex);
            }
        }

        private void PrintException()
        {
            "11".To<DateTime>(false);
        }
    }
}
