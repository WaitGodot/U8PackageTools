using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using U8PackageTools.code;
using System.Collections;

namespace U8PackageTools.Froms
{
    public partial class SDKList : Form
    {
        public ArrayList SelectSDKList = new ArrayList();
        public SDKList()
        {
            InitializeComponent();
        }

        private void SDKList_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<String,SDKManager.SDK> key in SDKManager.getInstance().SDKList)
            {
                ListViewItem li = new ListViewItem();
                li.Tag = key.Value;
                li.Text = key.Value.ShowName;
                listView.Items.Add(li);
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lv in listView.SelectedItems)
            {
                SelectSDKList.Add(lv.Tag as SDKManager.SDK);
            }
            Close();
        }
    }
}
