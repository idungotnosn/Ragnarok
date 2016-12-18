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
using Ragnarok.mediator;
using Ragnarok.GUI;
using MarketplaceWebService.Model;

namespace Ragnarok.userinteraction
{
    public partial class GUIForm : Form, UserInteraction
    {
        public GUIForm(NoSqlMediator mediator)
        {
            InitializeComponent();
            mediator.syncAmazonOrders(this);
        }

        public void setStatus(String message)
        {
            label2.Text = message;
        }

        public String getCustomMessageFromUrl(String url)
        {
            CustomUrlModal dialog = new CustomUrlModal(url);
            dialog.ShowDialog(this);
            return dialog.CustomResult;
        }

        public void showListOfReports(ICollection<ReportInfo> reports)
        {

        }

        public void showListOfOrders(ICollection<Order> orders)
        {

        }

        public void showError(String errorMessage)
        {
            label2.ForeColor = Color.Red;
            label2.Text = errorMessage;
        }

        public void stopAndShowMessage(String message)
        {

        }

        public bool confirmOrder(Order order)
        {
            ConfirmOrderModal modal = new ConfirmOrderModal(order);
            modal.ShowDialog(this);
            return modal.DialogResult == DialogResult.OK;
        }

        private void GUIForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}
