using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Data;



namespace ONE016
{
    public partial class ONE012WIN : Form
    {
        public ONE012WIN()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(tB1.Text))
            {
                DS2CSV Ds2csv = new DS2CSV();
                Ds2csv.Sok(tB1.Text, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Podana ścieżka nie istnieje.", "Komunikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        #region wybranie pliku
        void WybraniePliku()
        {
            FolderBrowserDialog bfDialog = new FolderBrowserDialog();

            if (bfDialog.ShowDialog() == DialogResult.OK)
            {
                tB1.Text = bfDialog.SelectedPath;
                //MessageBox.Show(path);
            }
            return;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            WybraniePliku();
        }
        private void TextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WybraniePliku();
        }
        #endregion

        private void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ComboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
