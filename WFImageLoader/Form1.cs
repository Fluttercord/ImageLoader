using System;
using System.IO;
using System.Windows.Forms;

namespace WFImageLoader
{
    public partial class Form1 : Form
    {
        private FormVm _vm;

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            _vm?.AddAddress(tbAddressToAdd.Text);
            RefreshList();
        }

        private void RefreshList()
        {
            if (_vm != null)
            {
                lbQueue.Items.Clear();
                _vm.Queue.ForEach(item => lbQueue.Items.Add(item));
            }
        }

        private void BtnSetupFolderClick(object sender, EventArgs e)
        {
            var folder = tbFolder.Text;
            Directory.CreateDirectory(folder);
            _vm = new FormVm(folder);
            _vm.OnProgerss += _vm_OnProgerss;
            _vm.OnStatusChanged += _vm_OnStatusChanged;
        }

        private void _vm_OnStatusChanged(FormVm sender, string status)
        {
            lblStatus.Text = status;
            RefreshList();
        }

        private void _vm_OnProgerss(FormVm sender, int siteProgress, int siteTotal, int totalProgress, int totalTotal)
        {
            Invoke(new MethodInvoker(delegate
            {
                pbCurrent.Maximum = siteTotal;
                pbTotal.Maximum = totalTotal;
                pbCurrent.Value = siteProgress;
                pbTotal.Value = totalProgress;
            }));
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            _vm?.Resume();
        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {
            _vm?.Suspend();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (_vm != null)
            {
                var selected = lbQueue.SelectedItem as string;
                if (selected != null)
                {
                    _vm.RemoveAddress(selected);
                    lbQueue.Items.Remove(selected);
                }
            }
            RefreshList();
        }
    }
}
