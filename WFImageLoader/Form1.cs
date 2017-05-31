using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WFImageLoader
{
    public partial class Form1 : Form
    {
        private string _folder;

        private List<WebsiteImagesDownloader> _queue = new List<WebsiteImagesDownloader>();
        private WebsiteImagesDownloader _current;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var address = tbAddressToAdd.Text;
            _queue.Add(new WebsiteImagesDownloader(address, _folder));
            lbQueue.Items.Add(address);
            Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _folder = tbFolder.Text;
            Directory.CreateDirectory(_folder);
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            if (_current != null)
            {
                _current.Resume();
                lblStatus.Text = _current.Address + " in progress";
            }
        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {
            if (_current != null)
            {
                _current.Suspend();
                lblStatus.Text = _current.Address + " suspended";
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var selected =
                _queue.FirstOrDefault(
                    item =>
                        string.Equals(lbQueue.SelectedItem as string, item.Address, StringComparison.InvariantCultureIgnoreCase));
            if (selected != null)
            {
                _queue.Remove(selected);
                lbQueue.Items.RemoveAt(lbQueue.SelectedIndex);
            }
        }

        private void Update()
        {
            if (_current == null)
            {
                if (_queue.Count != 0)
                {
                    _current = _queue[0];
                    _queue.Remove(_current);
                    lbQueue.Items.RemoveAt(0);
                    lblStatus.Text = _current.Address + " in progress";
                    _current.OnProgress += _current_OnProgress;
                    _current.Start();
                }
            }
        }

        private delegate void InvokeDelegate();

        private void _current_OnProgress(WebsiteImagesDownloader sender, int current, int total)
        {
            Invoke(new MethodInvoker(delegate
            {
                pbCurrent.Maximum = total;
                pbCurrent.Value = current;
                if (total == current)
                {
                    _current = null;
                    Update();
                }
            }));
        }
    }
}
