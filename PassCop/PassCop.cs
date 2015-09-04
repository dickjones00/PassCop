using PassCop.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassCop
{
    public partial class PassCop : Form
    {
        public PassCop()
        {
            InitializeComponent();
            this.Visible = false;
        }

        private void addPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Enabled = false;
            var upForm = new Data(-1);
            upForm.ShowDialog();
            contextMenuStrip1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            GetAllDbItems.GetAllDbData(tsmPasswords);
            tsmPasswords.DropDownItems.Add(new ToolStripSeparator());
            tsmPasswords.DropDownItems.Add("Refresh", null, refreshHandler);
        }

        private void refreshHandler(object sender, EventArgs e)
        {
            GetAllDbItems.GetAllDbData(tsmPasswords);
            tsmPasswords.DropDownItems.Add(new ToolStripSeparator());
            tsmPasswords.DropDownItems.Add("Refresh", null, refreshHandler);
            foreach (ToolStripItem item in tsmPasswords.DropDownItems)
            {
                if (item is ToolStripSeparator)
                    continue;
                if (item.Text != "Refresh")
                    item.Enabled = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notifyIcon1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                System.Reflection.MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                mi.Invoke(notifyIcon1, null);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var upForm = new EditData();
            contextMenuStrip1.Enabled = false;
            upForm.ShowDialog();
            foreach (ToolStripItem item in tsmPasswords.DropDownItems)
            {
                if (item is ToolStripSeparator)
                    continue;
                if (item.Text != "Refresh")
                item.Enabled = false;
                if (item.Text == "Refresh")
                    item.Text = "--> Please refresh! <--";
            }
            contextMenuStrip1.Enabled = true;
        }

        private void tsmInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Clicking on item under the 'Passwords menu' automatically decrypts and copies password to clipboard!");
        }
    }
}
