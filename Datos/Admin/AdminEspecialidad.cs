using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades.Models;
using System.Data.SqlClient;
using System.Data;
using Datos.Servidor;

namespace Datos.Admin
{
    public static class AdminEspecialidad
    {
        public static DataTable Listar()
        {
            //TODO listar especialidades- modo desconectado - dataTable
            string querySQL = "SELECT DISTINCT Id, Nombre FROM dbo.Especialidad";
            SqlDataAdapter adapter = new SqlDataAdapter(querySQL, AdminDB.ConectarBase());
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Nombre");

            return ds.Tables["Nombre"];
        }

        public static DataTable TraerUno(int id)
        {
            //TODO listar Especialidad por ID - desconectado - dataTable

            string querySQL = "select Id,Nombre FROM dbo.Especialidad where Id=@Id";

            SqlDataAdapter adapter = new SqlDataAdapter(querySQL, AdminDB.ConectarBase());

           adapter.SelectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            DataSet ds = new DataSet();

            adapter.Fill(ds, "Id");

            return ds.Tables["Id"];
        }

        public static DataTable TraerUno(string nombre)
        {
            //TODO listar Especialidad por ID - desconectado - dataTable

            string querySQL = "select Id,Nombre FROM dbo.Especialidad where Nombre=@Nombre";

            SqlDataAdapter adapter = new SqlDataAdapter(querySQL, AdminDB.ConectarBase());

            adapter.SelectCommand.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = nombre;

            DataSet ds = new DataSet();

            adapter.Fill(ds, "Nombre");

            return ds.Tables["Nombre"];
        }

        public static int Crear(Especialidad especialidad)
        {
            //TODO crear especialidad - Operacion de modificacion

            string querySQL = "INSERT dbo.Especialidad(Nombre) VALUES (@Nombre)";

            SqlCommand command = new SqlCommand(querySQL, AdminDB.ConectarBase());

            command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = especialidad.Nombre;

            int filasAfectadas = command.ExecuteNonQuery();

            AdminDB.ConectarBase().Close();

            return filasAfectadas;
        }
    }
}
