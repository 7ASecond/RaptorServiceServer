using RaptorServiceServer.Forms.Dockable;
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
using WeifenLuo.WinFormsUI.Docking;

namespace RaptorServiceServer
{
    public partial class frmMain : Form
    {

        private string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string appSettings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RaptorServer");
        private string LayoutFile = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RaptorServer"), "layout.xml");

        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        private frmToolbar toolbarForm = new frmToolbar();
        private frmMenu menuForm = new frmMenu();
        private frmDamoclesHost damoclesHostForm = new frmDamoclesHost();
        private frmHttpHost httpHostForm = new frmHttpHost();
        private frmStreamingHost streamingHostForm = new frmStreamingHost();
        private frmTCPHost tcpHostForm = new frmTCPHost();

        public frmMain()
        {
            InitializeComponent();

            if (!FirstRun())
            {
                if (File.Exists(LayoutFile))
                {
                    dockPanel.LoadFromXml(LayoutFile, m_deserializeDockContent);
                    m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
                }
            }
            else
            {
                AppSetup();
            }     
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(frmToolbar).ToString())
                return toolbarForm;
            else if (persistString == typeof(frmMenu).ToString())
                return menuForm;
            else if (persistString == typeof(frmDamoclesHost).ToString())
                return damoclesHostForm;
            else if (persistString == typeof(frmHttpHost).ToString())
                return httpHostForm;
            else if (persistString == typeof(frmStreamingHost).ToString())
                return streamingHostForm;
            else if (persistString == typeof(frmTCPHost).ToString())
                return tcpHostForm;
            //else
            //{
            //    // DummyDoc overrides GetPersistString to add extra information into persistString.
            //    // Any DockContent may override this value to add any needed information for deserialization.

            //    string[] parsedStrings = persistString.Split(new char[] { ',' });
            //    if (parsedStrings.Length != 3)
            //        return null;

            //    if (parsedStrings[0] != typeof(DummyDoc).ToString())
            //        return null;

            //    DummyDoc dummyDoc = new DummyDoc();
            //    if (parsedStrings[1] != string.Empty)
            //        dummyDoc.FileName = parsedStrings[1];
            //    if (parsedStrings[2] != string.Empty)
            //        dummyDoc.Text = parsedStrings[2];

            //    return dummyDoc;
            //}
            return null;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           
        }

        private void AppSetup()
        {

            if (Directory.Exists(appSettings))
            {
                if (File.Exists(LayoutFile))
                {
                    DialogResult dr = MessageBox.Show("Layout File Already Exists\nDo you want to load it?", "Layout File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        // Ok let's just use this Layout - so do nothing.
                    }
                    else
                    {
                        // Create a New Default Layout
                        SaveDefaultLayout();
                    }
                }
                else
                {
                    SaveDefaultLayout();
                    ShowDefaultLayout();
                }
            }
            else
            {
                Directory.CreateDirectory(appSettings);
                ShowDefaultLayout();
               
            }
        }

        private void ShowDefaultLayout()
        {
            menuForm.Show(dockPanel, DockState.DockTop);
            toolbarForm.Show(dockPanel, DockState.DockTop);
        }

        private void SaveDefaultLayout()
        {
            dockPanel.SaveAsXml(LayoutFile);
        }

        private bool FirstRun()
        {
            if (!File.Exists(LayoutFile)) return true;
            return false;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (m_bSaveLayout)
                dockPanel.SaveAsXml(LayoutFile);
            else if (File.Exists(LayoutFile))
                File.Delete(LayoutFile);
        }
    }
}