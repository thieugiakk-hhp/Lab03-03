using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab03_03.Models;

namespace Lab03_03
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            dtpDenNgay.Value = DateTime.Now;
            dtpTuNgay.Value = DateTime.Now;

            ProductOrderContextDB contextDB = new ProductOrderContextDB();
            List<TheOrder> theOrders = contextDB.TheOrders.ToList();

            BindingDataGridView(theOrders, dtpTuNgay.Value, dtpDenNgay.Value);

            TongSo();
        }

        private void BindingDataGridView(List<TheOrder> theOrders, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            dgvTTDH.Rows.Clear();

            foreach (var item in theOrders)
            {
                if (item.Invoice.DeliveryDate >= ngayBatDau && item.Invoice.DeliveryDate <= ngayKetThuc)
                {
                    int i = dgvTTDH.Rows.Add();

                    dgvTTDH.Rows[i].Cells[0].Value = item.No;
                    dgvTTDH.Rows[i].Cells[1].Value = item.InvoiceNo;
                    dgvTTDH.Rows[i].Cells[2].Value = item.Invoice.OrderDate;
                    dgvTTDH.Rows[i].Cells[3].Value = item.Invoice.DeliveryDate;
                    dgvTTDH.Rows[i].Cells[4].Value = item.Price * item.Quantity;
                }
            }
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            ProductOrderContextDB contextDB = new ProductOrderContextDB();
            List<TheOrder> theOrders = contextDB.TheOrders.ToList();
            BindingDataGridView(theOrders, dtpTuNgay.Value, dtpDenNgay.Value);

            TongSo();
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            ProductOrderContextDB contextDB = new ProductOrderContextDB();
            List<TheOrder> theOrders = contextDB.TheOrders.ToList();
            BindingDataGridView(theOrders, dtpTuNgay.Value, dtpDenNgay.Value);

            TongSo();
        }

        private void ckbXemTrongThang_CheckedChanged(object sender, EventArgs e)
        {
            ProductOrderContextDB contextDB = new ProductOrderContextDB();
            List<TheOrder> theOrders = contextDB.TheOrders.ToList();

            DateTime ngayBatDau = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime ngayKetThuc = new DateTime(DateTime.Now.Year, DateTime.Now.Month, ngayBatDau.AddMonths(1).AddDays(-1).Day);

            dtpTuNgay.Value = ngayBatDau;
            dtpDenNgay.Value = ngayKetThuc;

            BindingDataGridView(theOrders, ngayBatDau, ngayKetThuc);

            TongSo();
        }

        private void TongSo()
        {
            txtTongCong.Text = (dgvTTDH.Rows.Count - 1).ToString();
        }
    }
}
