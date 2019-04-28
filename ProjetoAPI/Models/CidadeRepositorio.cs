using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoAPI.Models
{
    public class CidadeRepositorio : ICidadeRepositorio
    {      

        readonly string _connectionString;

        public CidadeRepositorio(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConnDB");
        } 

        public IEnumerable<Cidade> GetAll(int id)
        {

            var sqlSelect = "select cid_codigo , cid_nome , est_codigo from  cidades where est_codigo = @Id order by cid_nome ";


            var selecaosList = new  List<Cidade>();
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(sqlSelect, con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var model = new Cidade
                    {
                        IdCidade = Convert.ToInt32(reader["cid_codigo"]),
                        Nome = reader["cid_nome"].ToString(),
                        IdEstado = Convert.ToInt32(reader["est_codigo"])
                    };

                    selecaosList.Add(model);
                }
                con.Close();
            }
            return selecaosList;
        }


    }
}
