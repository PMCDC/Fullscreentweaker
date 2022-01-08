﻿using System;
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
using FT.Core.Extensions;

namespace FT.WinClient
{
    public partial class MainForm : Form
    {
        private readonly IProcessInteractorService _processInteractorService;
        private readonly ICacheService _cacheService;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(IProcessInteractorService processInteractorService, ICacheService cacheService) : this()
        {
            _processInteractorService = processInteractorService;
            _cacheService = cacheService;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshActiveWindows();
        }

        private void btnFullscreenize_Click(object sender, EventArgs e)
        {
            Fullscreenzine();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenGithubLink();
        }
        
        private void MainForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            OpenAboutMessage();
            e.Cancel = true; //to be sure the cursor does not change
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshActiveWindows();
        }        

        /// <summary>
        /// Refresh the current active process of the ListView
        /// </summary>
        private void RefreshActiveWindows()
        {
            try
            {
                var windows = _processInteractorService.GetActiveWindows();
                _cacheService.SetCachedWindowInformations(windows);


                //clear the list
                lvWindows.Clear();
                lvWindows.AddBlankColumnHeader();


                ImageList imageList = new ImageList();
                List<WindowIconReference> iconReferences = new List<WindowIconReference>();

                //add window into ListView
                for (int i = 0; i < windows.Count; i++)
                {
                    //get the current window
                    var window = windows[i];

                    //add the Form title to the listview
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

                //Resize columns
                lvWindows.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                //refresh count
                lblCountInfo.Visible = true;
                lblCountInfo.Text = $"{windows.Count} Toplevel window(s) detected";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Fullscreenzine the selected process in the ListView
        /// </summary>
        private void Fullscreenzine()
        {
            try
            {
                //verify if there is a process selected
                if (lvWindows.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("Please select a game first.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //get the selected process and validate that it does still exist
                var window = _cacheService.WindowInformations.FirstOrDefault(w => w.Index == lvWindows.Items.IndexOf(lvWindows.SelectedItems[0]));
                var windows = _processInteractorService.GetActiveWindows();
                if (!windows.Any(w => w.Pointer == window.Pointer))
                {
                    MessageBox.Show("The process does no longer exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //apply borderless fullscreen
                _processInteractorService.SetBorderlessFullscreen(new Core.Services.Parameters.SetBorderlessFullscreenParameter()
                {
                    Window = window,
                    IsStayOnTop = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Open the gitub of this solution
        /// </summary>
        private void OpenGithubLink()
        {
            try
            {
                var url = @"https://github.com/PMCDC/Fullscreentweaker";
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenAboutMessage()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(@"C#/.NET5 version of the Fullscreenizer app originaly made by Kostas ""Bad Sector"" Michalopoulos");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("Instructions:");
            stringBuilder.AppendLine(@"1. Open the game you want to force in ""Borderless Fullscreen"".");
            stringBuilder.AppendLine("2. Set the game into windowed mode.");
            stringBuilder.AppendLine("3. Hit refresh to make the game appear into the list.");
            stringBuilder.AppendLine(@"4. Hit the Fullscreenize button to make the game ""Borderless Fullscreen""");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine(@"//Recoded by Pierre-Marc Coursol de Carufel");

            MessageBox.Show(stringBuilder.ToString(), "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
