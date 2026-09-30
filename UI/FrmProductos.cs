using System;
using System.Globalization;
using System.Windows.Forms;
using BE;
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
            ListarProductos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para el producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            string precioTexto = txtPrecio.Text.Trim().Replace(',', '.');
            if (!decimal.TryParse(precioTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal precio))
            {
                MessageBox.Show("El precio ingresado no es un número decimal válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

            if (!int.TryParse(txtStock.Text.Trim(), out int stock))
            {
                MessageBox.Show("El stock ingresado no es un número entero válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return;
            }

            Producto nuevoProducto = new Producto
            {
                Nombre = txtNombre.Text,
                Precio = precio,
                Stock = stock
            };

            try
            {
                int filasAfectadas = _productoBLL.Add(nuevoProducto);

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Producto registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    ListarProductos();
                }
                else
                {
                    MessageBox.Show("No se pudo insertar el producto en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Debe seleccionar un producto válido de la grilla para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para el producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            string precioTexto = txtPrecio.Text.Trim().Replace(',', '.');
            if (!decimal.TryParse(precioTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal precio))
            {
                MessageBox.Show("El precio ingresado no es un número decimal válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

            if (!int.TryParse(txtStock.Text.Trim(), out int stock))
            {
                MessageBox.Show("El stock ingresado no es un número entero válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return;
            }

            Producto productoAModificar = new Producto
            {
                Id = id,
                Nombre = txtNombre.Text,
                Precio = precio,
                Stock = stock
            };

            try
            {
                int filasAfectadas = _productoBLL.Update(productoAModificar);

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Producto modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarProductos();
                }
                else
                {
                    MessageBox.Show("No se encontró el producto en la base de datos (pudo haber sido eliminado previamente).", "Registro no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ListarProductos();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Debe seleccionar un producto de la grilla para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                $"¿Está seguro de que desea eliminar el producto '{txtNombre.Text}' (Id: {id})?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int filasAfectadas = _productoBLL.Delete(id);

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Producto eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    ListarProductos();
                }
                else
                {
                    MessageBox.Show("No se encontró el producto en la base de datos (pudo haber sido eliminado previamente).", "Registro no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarCampos();
                    ListarProductos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarSeleccion();
        }

        private void CargarSeleccion()
        {
            if (dgvProductos.CurrentRow != null && dgvProductos.CurrentRow.DataBoundItem is Producto producto)
            {
                txtId.Text = producto.Id.ToString();
                txtNombre.Text = producto.Nombre;
                txtPrecio.Text = producto.Precio.ToString(CultureInfo.CurrentCulture);
                txtStock.Text = producto.Stock.ToString();

                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                LimpiarCampos();
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void ListarProductos()
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
