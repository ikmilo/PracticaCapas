using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BLL;

namespace UI
{
    public partial class FrmProductos : Form
    {
        private ProductoBLL _productoBLL = new ProductoBLL();

        public FrmProductos()
        {
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = _productoBLL.Getall();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al listar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
