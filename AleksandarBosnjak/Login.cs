using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AleksandarBosnjak
{

    public partial class Login : Form
    {
        private bool openedDialog = false;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text == "admin" && textBox2.Text == "admin"))
            {
                openedDialog = true;
                MessageBox.Show("Korisničko ime ili šifra nisu validni. Pokušajte ponovo.", "Greška prilikom autentifikacije", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                textBox1.Text = "";
                textBox1.Select();

                textBox2.Text = "";
            }
            else {
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Windows.Forms.Application.Exit();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !openedDialog) { 
                button1_Click(new object(), null);
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !openedDialog)
            {
                button1_Click(new object(), null);
            }
        }



    }
}
