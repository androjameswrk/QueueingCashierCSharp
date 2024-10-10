using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace QueuingCashier 
{
    public partial class CashierWindowQueueForm : Form
    {
        CashierClass obj = new CashierClass();
        public CashierWindowQueueForm()
        {
            InitializeComponent();
           
            Timer timer = new Timer();
            timer.Interval = (1 * 1000);
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();
        }

        Boolean openForm  = false;
        CustomerView customerView = new CustomerView();
        FormCollection AllForms = Application.OpenForms;
        Form OpenedForm = new Form();

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnRefresh.PerformClick();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplayCashierQueue(CashierClass.CashierQueue);
            
        }
        public void DisplayCashierQueue(IEnumerable CashierList)
        {
            listCashierQueue.Items.Clear();
            foreach (Object obj in CashierList)
            {
                listCashierQueue.Items.Add(obj.ToString());
            }
        }

        public delegate void PassControl(object sender);
        public PassControl passControl;

        private void btnNext_Click(object sender, EventArgs e)
        {
            //CashierClass.CashierQueue.Dequeue();
            NextServing();
        }

        public void NextServing()
        {
            foreach (Form form  in AllForms)
            {
                if(form.Name == "CustomerView")
                {
                    OpenedForm = form;
                    openForm = true;
                }
            }

            if(openForm == true)
            {
                if(passControl != null)
                {
                    customerView.NowServing.Text = CashierClass.CashierQueue.Peek();
                    CashierClass.CashierQueue.Dequeue();
                    passControl(customerView.NowServing);
                }
            }
            else
            {
                customerView.ShowDialog();
                customerView.NowServing.Text = CashierClass.CashierQueue.Peek().ToString();
                CashierClass.CashierQueue.Dequeue();
            }
        }

        private void CashierWindowQueueForm_Load(object sender, EventArgs e)
        {

        }
    }
}
