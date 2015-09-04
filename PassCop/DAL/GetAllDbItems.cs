using PassCop.BLL;
using PassCop.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassCop.DAL
{
    public class GetAllDbItems
    {
        private static DataVM item = null;
        internal static void GetAllDbData(ToolStripDropDownItem toolStripMenu)
        {
            using (UPdatabase ctx = new UPdatabase())
            {
                var allRows = ctx.UpData.ToList();
                if (toolStripMenu.DropDownItems.Count != 0)
                {
                    toolStripMenu.DropDownItems.Clear();
                }
                foreach (var name in allRows)
                {
                    toolStripMenu.DropDownItems.Add(name.ID + "-" + name.UserName, null, myClickHandler);
                    item = ctx.UpData.FirstOrDefault(s => s.UserName == name.UserName);
                }                
            }
        }
        internal static void myClickHandler(object sender, EventArgs e)
        {
            var menuItem = sender.ToString();
            var idParts = menuItem.Split('-');
            var id = Convert.ToInt32(idParts[0]);
            using (var ctx = new UPdatabase())
            {
                var result = ctx.UpData.SingleOrDefault(b => b.ID == id);
                Clipboard.SetText(Encryption.Decrypt(result.Password, true));
            }
        }

        internal static void RefreshGrid(DataGridView grid)
        {
            grid.DataSource = typeof(List<>); // OR use grid_accounts.DataSource = null;
            using (UPdatabase ctx = new UPdatabase())
            {
                var allRows = ctx.UpData.ToList();
                grid.DataSource = allRows;
                grid.Rows[0].Selected = true;
            }
        }
    }
}
