using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades.Models;
using System.Data;
using System.Data.SqlClient;
using Datos.Servidor;

namespace Datos.Admin
{
    public static class AdminMedico
    {
        public static List<Medico> Listar()
        {
            //TODO listar medicos - modelo conectado - reader
            string querySql = "SELECT Id,Nombre,Apellido,NroMatricula,EspecialidadId FROM dbo.Medico";

            SqlCommand command = new SqlCommand(querySql, AdminDB.ConectarBase());

            SqlDataReader reader;

            reader = command.ExecuteReader();

            List<Medico> medicos = new List<Medico>();

            while (reader.Read())
            {
                medicos.Add(
                    new Medico()
                    {
                        //,,,,
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        NroMatricula = (int)reader["NroMatricula"],
                        EspecialidadId = (int)reader["EspecialidadId"],
                    }
                    );
                    }
            reader.Close();
            AdminDB.ConectarBase().Close();

            return medicos;
        }

        public static DataTable Listar(int idEspecialidad)
        {
            //TODO listar medicos por especialidad - desconectado - dataTable
            string querySql = "SELECT Id,Nombre,Apellido,NroMatricula,EspecialidadId FROM dbo.Medico where EspecialidadId = @EspecialidadId";

            SqlDataAdapter adapter = new SqlDataAdapter(querySql, AdminDB.ConectarBase());

            adapter.SelectCommand.Parameters.Add("@EspecialidadId", SqlDbType.Int).Value = idEspecialidad;

            DataSet ds = new DataSet();

            adapter.Fill(ds, "EspecialidadId");

            return ds.Tables["EspecialidadId"];
        }

        public static DataTable TraerUno(int id)
        {
            //TODO listar medicos por especialidad - desconectado - dataTable

            string querySQL = "SELECT Id,Nombre,Apellido,NroMatricula,EspecialidadId FROM dbo.Medico where Id=@Id";

            SqlDataAdapter adapter = new SqlDataAdapter(querySQL, AdminDB.ConectarBase());

            adapter.SelectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            DataSet ds = new DataSet();

            adapter.Fill(ds, "Id");

            return ds.Tables["Id"];
        }

        public static int Crear(Medico medico)
        {
            //TODO crear medico - Operacion de modificacion
            string querySQL = "INSERT dbo.Medico(Nombre,Apellido,NroMatricula,EspecialidadId) VALUES (@Nombre, @Apellido, @NroMatricula,@EspecialidadId)";

            SqlCommand command = new SqlCommand(querySQL, AdminDB.ConectarBase());

            command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = medico.Nombre;

            command.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = medico.Apellido;

            command.Parameters.Add("@NroMatricula", SqlDbType.Int).Value = medico.NroMatricula;

            command.Parameters.Add("@EspecialidadId", SqlDbType.Int).Value = medico.EspecialidadId;

            int filasAfectadas = command.ExecuteNonQuery();

            AdminDB.ConectarBase().Close();

            return filasAfectadas;
        }
    }
}
