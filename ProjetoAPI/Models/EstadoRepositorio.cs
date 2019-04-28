using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoAPI.Models
{
    public class EstadoRepositorio : IEstadoRepositorio
    {      

        readonly string _connectionString;

        public EstadoRepositorio(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConnDB");
        } 

        public IEnumerable<Estado> GetAll()
        {

            var sqlSelect = "select est_codigo , est_nome from  estados order by est_nome  ";


            var selecaosList = new  List<Estado>();
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(sqlSelect, con)
                {
                    CommandType = CommandType.Text
                };

                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var model = new Estado
                    {
                        IdEstado = Convert.ToInt32(reader["est_codigo"]),
                        Nome = reader["est_nome"].ToString(),

                    };

                    selecaosList.Add(model);
                }
                con.Close();
            }
            return selecaosList;
        }


    }
}
