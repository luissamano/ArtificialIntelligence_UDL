using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowDesktop
{
    class Get : Servicio
    {
        int res = 0;
        List<Conocimiento> conocimientos = new List<Conocimiento>();

        public async Task<List<Conocimiento>> GetConocimientos(string id)
        {
            String connStr = GetConnSQL();

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("spConocimiento", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Opc", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("Nombre", SqlDbType.VarChar).Value = id;
                    connection.Open();
                    //await cmd.ExecuteNonQueryAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Conocimiento alumno = new Conocimiento();
                            alumno.Id_Obj = reader.GetInt32(0);
                            alumno.Nombre = reader.GetString(1);
                            alumno.Definicion = reader.GetString(2);
                            alumno.Color = reader.GetString(3);
                            alumno.Genero = reader.GetString(4);
                            
                            conocimientos.Add(alumno);
                        }
                    }
                }
            }
            return conocimientos;
        }

    
        public async Task<int> PostConocimiento(string obj, string des, string col, string ani, string gen, int cont)
        {
            String connStr = GetConnSQL();

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("spConocimiento", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Opc", SqlDbType.Int).Value = 2;
                    cmd.Parameters.Add("Nombre", SqlDbType.VarChar).Value = obj;
                    cmd.Parameters.Add("Def", SqlDbType.VarChar).Value = des;
                    cmd.Parameters.Add("Col", SqlDbType.VarChar).Value = col;
                    cmd.Parameters.Add("Ani", SqlDbType.VarChar).Value = ani;
                    cmd.Parameters.Add("Gen", SqlDbType.VarChar).Value = gen;
                    cmd.Parameters.Add("Cont", SqlDbType.Int).Value = cont;
                    connection.Open();
                    return await cmd.ExecuteNonQueryAsync();                    
                }
            }
        }

        public async Task<int> GetUltimoNun()
        {
            string conStr = GetConnSQL();
            int retu = 0;

            using (SqlConnection connetion = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("spConocimiento", connetion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Opc", SqlDbType.Int).Value = 0;
                    await connetion.OpenAsync();

                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            retu = rd.GetInt32(0);
                        }
                    }
                }
            }
            return retu + 1;
        }



        public string GetConnSQL()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"DESKTOP-JS2FOQ0\SQLEXPRESS";
            builder.InitialCatalog = "dbConocimiento";
            builder.UserID = "sa";
            builder.Password = "sysserver";
            builder.IntegratedSecurity = true;
            return builder.ConnectionString;
        }

       
    }
}
