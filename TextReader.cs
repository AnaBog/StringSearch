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

namespace StringSearcher
{
    public partial class TextReader : Form
    {
        public TextReader(string file)
        {
            InitializeComponent();
            // reading text file in textBox
            textBox.Text = File.ReadAllText(file);
        }
    }
}
