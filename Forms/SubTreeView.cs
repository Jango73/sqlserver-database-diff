
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SQLServerDatabaseDiff
{
    public class SubTreeView : TreeView
    {
        public MainForm ParentForm;

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.InheritanceDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        [System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message msg)
        {
            const int WM_VSCROLL = 0x0115;
            const int WM_MOUSEWHEEL = 0x020A;

            base.WndProc(ref msg);

            switch (msg.Msg)
            {
                case WM_VSCROLL:
                case WM_MOUSEWHEEL:
                    if (ParentForm != null) ParentForm.Scrolled(this);
                    break;
            }
        }
    }
}
