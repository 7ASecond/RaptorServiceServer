using RaptorServiceServer.Forms.Dockable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace RaptorServiceServer
{
    public partial class frmMain : Form
    {

        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        private frmToolbar toolbarForm;
        private frmMenu menuForm;
        private frmDamoclesHost damoclesHostForm;
        private frmHttpHost httpHostForm;
        private frmStreamingHost streamingHostForm;
        private frmTCPHost tcpHostForm;

        public frmMain()
        {
            InitializeComponent();
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
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
    }
}
