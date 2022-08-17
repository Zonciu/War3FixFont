using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using War3FixFont.Properties;

namespace War3FixFont
{
    public partial class Manual : Form
    {
        public Manual()
        {
            InitializeComponent();
            ManualContent.Text = Resources.Manual;
        }
    }
}