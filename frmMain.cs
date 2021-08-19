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

            TongCong();
        }

        private void BindingDataGridView(List<TheOrder> theOrders, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            dgvTTDH.Rows.Clear();

            string invoiceNo = "";
            long thanhTien = 0;
            int viTri = 0;

            foreach (var item in theOrders)
            {
                if (item.Invoice.DeliveryDate >= ngayBatDau && item.Invoice.DeliveryDate <= ngayKetThuc)
                {
                    if (item.InvoiceNo == invoiceNo)
                    {
                        thanhTien += long.Parse(item.Price.ToString()) * long.Parse(item.Quantity.ToString());
                        dgvTTDH.Rows[viTri].Cells[4].Value = thanhTien;
                    }
                    else
                    {
                        int i = dgvTTDH.Rows.Add();

                        dgvTTDH.Rows[i].Cells[0].Value = i + 1;
                        dgvTTDH.Rows[i].Cells[1].Value = item.InvoiceNo;
                        dgvTTDH.Rows[i].Cells[2].Value = item.Invoice.OrderDate;
                        dgvTTDH.Rows[i].Cells[3].Value = item.Invoice.DeliveryDate;
                        dgvTTDH.Rows[i].Cells[4].Value = item.Price * item.Quantity;

                        invoiceNo = item.InvoiceNo;
                        thanhTien = long.Parse(item.Price.ToString()) * long.Parse(item.Quantity.ToString());
                        viTri = i;
                    }
                }
            }
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            ProductOrderContextDB contextDB = new ProductOrderContextDB();
            List<TheOrder> theOrders = contextDB.TheOrders.ToList();
            BindingDataGridView(theOrders, dtpTuNgay.Value, dtpDenNgay.Value);

            TongCong();
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            ProductOrderContextDB contextDB = new ProductOrderContextDB();
            List<TheOrder> theOrders = contextDB.TheOrders.ToList();
            BindingDataGridView(theOrders, dtpTuNgay.Value, dtpDenNgay.Value);

            TongCong();
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

            TongCong();
        }

        private void TongCong()
        {
            long tongCong = 0;

            for (int i = 0; i < dgvTTDH.Rows.Count - 1; i++)
            {
                tongCong += long.Parse(dgvTTDH.Rows[i].Cells[4].Value.ToString());
            }

            txtTongCong.Text = tongCong.ToString();
        }
    }
}
