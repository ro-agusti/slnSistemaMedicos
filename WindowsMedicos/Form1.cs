using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Datos.Admin;
using System.Data;
using Entidades.Models;

namespace WindowsMedicos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mostrarMedicos();
            llenarComboFiltrarEspecialidad();
            llenarComboEspecialidad();
        }

        private void mostrarMedicos()
        {
            gridMedicos.DataSource = AdminMedico.Listar();
        }

        private void llenarComboFiltrarEspecialidad()
        {
            DataTable especialidad = AdminEspecialidad.Listar();
            cbTraerEspecialidad.DataSource = especialidad;
            cbTraerEspecialidad.DisplayMember = especialidad.Columns["Nombre"].ToString();
            cbTraerEspecialidad.ValueMember = especialidad.Columns["Id"].ToString();
            DataRow fila = especialidad.NewRow();
            fila["Id"] = 0;
            fila["Nombre"] = "[TODAS]";
            especialidad.Rows.InsertAt(fila, 0);

        }

        private void llenarComboEspecialidad()
        {
            DataTable especialidad = AdminEspecialidad.Listar();
            cbEspecialidad.DataSource = especialidad;
            cbEspecialidad.DisplayMember = especialidad.Columns["Nombre"].ToString();
            cbEspecialidad.ValueMember = especialidad.Columns["Id"].ToString();
        }

        private void cbTraerEspecialidad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int especialidad = Convert.ToInt32(cbTraerEspecialidad.SelectedValue);
            if (especialidad == 0)
            {
                mostrarMedicos();
            }
            else
            {
                // TODO hacer metodo listar(params) ---
                gridMedicos.DataSource =  AdminMedico.Listar(especialidad);
            }
        }

        

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            int nroMatricula = Convert.ToInt32(txtNroMatricula.Text);

            int especialidadID  = Convert.ToInt32( cbEspecialidad.SelectedValue);
            ////MessageBox.Show(especialidadID.ToString());
            Medico medico = new Medico(nombre, apellido, nroMatricula, especialidadID);
            AdminMedico.Crear(medico);
            mostrarMedicos();
        }
    }
}
