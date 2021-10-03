using System;
using System.Windows.Forms;
using ProductsControl.Controllers;
using ProductsControl.Models;
using System.Collections.Generic;

namespace ProductsControl
{
    public partial class Form1 : Form
    {

        List<int> newRows = new List<int>();
        List<int> updatedRows = new List<int>();

        List<Product> products = new List<Product>();

        public Form1()
        {
            InitializeComponent();
            getData();
        }

        protected override void OnLoad(EventArgs e)
        {

            dataGridView1.UserAddedRow += new DataGridViewRowEventHandler(DataGridView1_UserAddedRow);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(DataGridView1_CellValueChanged);

            base.OnLoad(e);
        }

        private void getData()
        {
            products.Clear();

            products = new ProductsController().index();

            dataGridView1.Rows.Clear();

            for (int i = 0; i < products.Count; i++)
            {

                dataGridView1.Rows.Add(products[i].Id, products[i].Name, products[i].Ean, products[i].Price, products[i].StockQuantity);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

            /// Realiza a adição de todos os novos registros que foram adicionados.

            for(int i = 0; i < newRows.Count; i++)
            {
                Product product = new Product();

                product.Name = dataGridView1.Rows[newRows[i]].Cells[1].Value.ToString();
                product.Ean = dataGridView1.Rows[newRows[i]].Cells[2].Value.ToString();
                product.Price = double.Parse(dataGridView1.Rows[newRows[i]].Cells[3].Value.ToString());
                product.StockQuantity = int.Parse(dataGridView1.Rows[newRows[i]].Cells[4].Value.ToString());

                new ProductsController().store(product);
            }

            /// Realiza o update de todos os registros que sofreram atualizações.

            for (int i = 0; i < updatedRows.Count; i++)
            {
                Product product = new Product();

                product.Id = int.Parse(dataGridView1.Rows[updatedRows[i]].Cells[0].Value.ToString());
                product.Name = dataGridView1.Rows[updatedRows[i]].Cells[1].Value.ToString();
                product.Ean = dataGridView1.Rows[updatedRows[i]].Cells[2].Value.ToString();
                product.Price = double.Parse(dataGridView1.Rows[updatedRows[i]].Cells[3].Value.ToString());
                product.StockQuantity = int.Parse(dataGridView1.Rows[updatedRows[i]].Cells[4].Value.ToString());

                new ProductsController().update(product);
            }

            newRows.Clear();
            updatedRows.Clear();

            getData();
        }

        private void deleteButton_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            {
                int deletedProductId = int.Parse(dataGridView1.Rows[item.Index].Cells[0].Value.ToString());

                new ProductsController().delete(deletedProductId);

                dataGridView1.Rows.RemoveAt(item.Index);
            }

        }

        private void DataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            newRows.Add(e.Row.Index - 1);
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            /// Registra o update apenas dos produtos que já foram adicionados no banco de dados.

            if (newRows.IndexOf(e.RowIndex) < 0)
            {
                updatedRows.Add(e.RowIndex);
            }
        }
    }
}
