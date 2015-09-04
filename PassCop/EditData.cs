using PassCop.BLL;
using PassCop.DAL;
using PassCop.VM;
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

namespace PassCop
{
    public partial class EditData : Form
    {
        private int id = -1;
        public EditData()
        {
            InitializeComponent();
            GetAllDbItems.RefreshGrid(grdData);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            id = -1;
            var upForm = new Data(id);
            upForm.ShowDialog();
            GetAllDbItems.RefreshGrid(grdData);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var upForm = new Data(id);
            upForm.ShowDialog();
            GetAllDbItems.RefreshGrid(grdData);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            UPdatabase ctx = new UPdatabase();
            var item = new DataVM() { ID = id };
            ctx.UpData.Attach(item);
            ctx.UpData.Remove(item);
            ctx.SaveChanges();
            GetAllDbItems.RefreshGrid(grdData);
        }

        private void grdData_Validated(object sender, EventArgs e)
        {
            if (grdData.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.grdData.SelectedRows[0];
                id = Convert.ToInt32(row.Cells[0].Value.ToString());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetAllDbItems.RefreshGrid(grdData);
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            dlgFileSave.Filter = "CSV format|*.csv";
            dlgFileSave.Title = "Export to CSV";
            dlgFileSave.FileName = "Export";
            dlgFileSave.DefaultExt = "csv";
            dlgFileSave.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            dlgFileSave.RestoreDirectory = true;
            DialogResult result = dlgFileSave.ShowDialog();

            if (dlgFileSave.FileName != "" && result != DialogResult.Cancel)
            {
                WriteCsv.writeCSV(grdData, dlgFileSave.FileName);
                FileInfo fileInfo = new FileInfo(dlgFileSave.FileName);
            }
        }

        private void grdData_SelectionChanged(object sender, EventArgs e)
        {
            if (grdData.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.grdData.SelectedRows[0];
                id = Convert.ToInt32(row.Cells[0].Value.ToString());
            }
        }
    }
}
