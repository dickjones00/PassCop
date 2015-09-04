using PassCop.BLL;
using PassCop.DAL;
using PassCop.VM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassCop
{
    public partial class Data : Form
    {
        private int id = -1;
        public Data(int Id)
        {
            InitializeComponent();
            id = Id;
            if (id != -1)
            {
                using (UPdatabase ctx = new UPdatabase())
                {
                    var item = ctx.UpData.FirstOrDefault(s => s.ID == id);
                    txtName.Text = item.UserName;
                    lblPassword.Text = "New password";
                    //txtPassword.Text = Encryption.Decrypt(item.Password, true);
                }
            }
            else
            {
                txtName.Text = "";
                txtPassword.Text = "";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (id != -1)
            {
                using (var ctx = new UPdatabase())
                {
                    var result = ctx.UpData.SingleOrDefault(b => b.ID == id);
                    result.UserName = txtName.Text;
                    result.Password = Encryption.Encrypt(txtPassword.Text, true);
                    ctx.SaveChanges();
                }
            }
            else
            {
                var data = new DataVM();
                data.UserName = txtName.Text;
                data.Password = Encryption.Encrypt(txtPassword.Text, true);

                using (var dbCtx = new UPdatabase())
                {
                    //Add Student object into Students DBset
                    dbCtx.UpData.Add(data);

                    // call SaveChanges method to save student into database
                    dbCtx.SaveChanges();
                }
                //MessageBox.Show("Data stored in the database");
            }
            this.Close();
        }
    }
}
