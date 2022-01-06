using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FT.Core.Services;
using FT.Core.Services.Models;

namespace FT.WinClient
{
    public partial class MainForm : Form
    {
        private readonly IProcessInteractorService _processInteractorService;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(IProcessInteractorService processInteractorService) : this()
        {
            _processInteractorService = processInteractorService;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshActiveWindows();
        }

        private void RefreshActiveWindows()
        {
            try
            {
                var windows = _processInteractorService.GetActiveWindows();

                lvWindows.Clear();

                ColumnHeader header = new ColumnHeader();
                header.Text = "";
                header.Name = "col1";
                lvWindows.Columns.Add(header);


                ImageList imageList = new ImageList();
                List<WindowIconReference> iconReferences = new List<WindowIconReference>();

                for (int i = 0; i < windows.Count; i++)
                {
                    var window = windows[i];

                    //add the form title to the list view
                    lvWindows.Items.Add(window.Title);

                    //create icon reference list for the listview
                    if (window.Icon != null)
                    {
                        imageList.Images.Add(window.Icon);

                        iconReferences.Add(new WindowIconReference()
                        {
                            Icon = window.Icon,
                            Index = iconReferences.Count,
                            Pointer = window.Pointer
                        });
                    }
                }

                //set window icons to the proper listview item
                lvWindows.SmallImageList = imageList;
                for (int i = 0; i < lvWindows.Items.Count; i++)
                {
                    var window = windows[i];
                    var iconReference = iconReferences.FirstOrDefault(wwi => wwi.Pointer == window.Pointer);

                    if (iconReference != null)
                    {
                        lvWindows.Items[i].ImageIndex = iconReference.Index;
                    }
                }

                ResizeListViewColumns(lvWindows);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResizeListViewColumns(ListView lv)
        {
            foreach (ColumnHeader column in lv.Columns)
            {
                column.Width = -2;
            }
        }
    }
}
