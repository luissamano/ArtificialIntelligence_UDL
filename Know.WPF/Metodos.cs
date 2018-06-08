using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Know.WPF.Models;

namespace Know.WPF
{
    class Metodos : IMetodos
    {

        string connStr;
        List<Knows> Knows = new List<Knows>();

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

        public async Task<List<Knows>> GetConocimientos(string nombre)
        {
            connStr = GetConnSQL();

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("spConocimiento", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Opc", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("Nombre", SqlDbType.VarChar).Value = nombre.ToLower();
                    connection.Open();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Knows knows = new Knows();
                            knows.Id_Obj = reader.GetInt32(0);
                            knows.Nombre = reader.GetString(1);
                            knows.Definicion = reader.GetString(2);
                            knows.Color = reader.GetString(3);
                            knows.Genero = reader.GetString(4);

                            Knows.Add(knows);
                        }
                    }
                }
            }
            return Knows;
        }

        public async Task<int> GetUltimoNun()
        {
            int retu = 0;
            connStr = GetConnSQL();

            using (SqlConnection connetion = new SqlConnection(connStr))
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

        public async Task<int> PostConocimiento(string obj, string des, string col, string ani, string gen, int cont)
        {
            connStr = GetConnSQL();

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

    }
}
