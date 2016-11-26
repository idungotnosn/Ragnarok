using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ragnarok.model;

namespace Ragnarok.GUI
{
    public partial class ConfirmOrderModal : Form
    {
        private AmazonOrder order;

        public DialogResult DialogResult
        {
            get;
            set;
        }

        public ConfirmOrderModal(AmazonOrder order)
        {
            InitializeComponent();
            this.order = order;
            this.populateListView(this.listView1, order);
            this.populateIndexListView(order.OrderItems.Count);
        }

        private void populateIndexListView(int orderItemLength)
        {
            for (int i = 1; i <= orderItemLength; i++)
            {
                this.listView2.Items.Add(new ListViewItem(new string[] {i.ToString()}));
            }
        }

        private void populateListView(ListView view, ExtensibleDataModel model){
            view.Items.Clear();
            foreach (KeyValuePair<String, String> item in model.ColumnStringMappings)
            {
                view.Items.Add(new ListViewItem(new string[] { item.Key, item.Value }));
            }
            foreach (KeyValuePair<String, int> item in model.ColumnIntegerMappings)
            {
                view.Items.Add(new ListViewItem(new string[] { item.Key, item.Value.ToString() }));
            }
            foreach (KeyValuePair<String, decimal> item in model.ColumnDecimalMappings)
            {
                view.Items.Add(new ListViewItem(new string[] { item.Key, item.Value.ToString() }));
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedIndices.Count > 0) { 
                int selectedIndex = listView2.SelectedIndices[0];
                if (selectedIndex < this.order.OrderItems.Count) { 
                    this.populateListView(listView3, order.OrderItems[selectedIndex]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
