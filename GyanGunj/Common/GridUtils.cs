using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace GyanGunj.Common
{
    public static class GridUtils
    {
        public static GridViewColumn LastColumn(RadGridView grid)
        {
            return grid.Columns.Where<GridViewColumn>(x => x.IsVisible && x.IsReadOnly == false).OrderByDescending(x => x.DisplayIndex).FirstOrDefault();
        }
        public static GridViewColumn FirstColumn(RadGridView grid)
        {
            return grid.Columns.Where<GridViewColumn>(x => x.IsVisible && x.IsReadOnly == false).OrderBy(x => x.DisplayIndex).FirstOrDefault();
        }

        public static void BeginRowEditing(RadGridView grid, int Rowindex, int Columnindex = 0)
        {
            if (Rowindex < 0 || Columnindex < 0)
                return;
            if (grid.Items.Count > Rowindex && grid.Columns.Count > Columnindex)
            {
                grid.CurrentCellInfo = new GridViewCellInfo(grid.Items[Rowindex], grid.Columns[Columnindex]);
                grid.BeginEdit();
            }
        }
    }
}
