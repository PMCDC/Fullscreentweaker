using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FT.Core.Extensions
{
    public static class ListViewExtension
    {
        /// <summary>
        /// Add a blank column header to have proper resizable rows on the ListView.
        /// </summary>
        /// <param name="listView"></param>
        public static void AddBlankColumnHeader(this ListView listView)
        {
            ColumnHeader header = new ColumnHeader() 
            {
                Text = string.Empty,
                Name = nameof(header)
            };

            listView.Columns.Clear();
            listView.Columns.Add(header);
        }

        /// <summary>
        /// Resize Columns to fit Rows content
        /// </summary>
        /// <param name="listView"></param>
        public static void ResizeListViewColumns(this ListView listView)
        {
            foreach (ColumnHeader column in listView.Columns)
            {
                column.Width = -2;
            }
        }
    }
}
