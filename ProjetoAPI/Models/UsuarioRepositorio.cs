using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoAPI.Models
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        readonly string _connectionString;

        public UsuarioRepositorio(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConnDB");
        }

        public int Add(Usuario model)
        {

            var sqlInsert = "INSERT INTO UsuariosTeste ";
            sqlInsert += " (Nome";
            sqlInsert += ", Logradouro";
            sqlInsert += ", Telefone";
            sqlInsert += ", LogradouroNumero";
            sqlInsert += ", Bairro";
            sqlInsert += ", Email";
            sqlInsert += ", cid_codigo)";

            sqlInsert += "    VALUES";
            sqlInsert += "  (@Nome";
            sqlInsert += "  ,@Logradouro";
            sqlInsert += "  ,@Telefone";
            sqlInsert += "  ,@LogradouroNumero";
            sqlInsert += "  ,@Bairro";
            sqlInsert += "  ,@Email";
            sqlInsert += "  ,@IdCidade)";

            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(sqlInsert, con)
                    {
                        CommandType = CommandType.Text
                    };

                    cmd.Parameters.AddWithValue("@Nome", model.Nome);
                    cmd.Parameters.AddWithValue("@Logradouro", model.Logradouro);
                    cmd.Parameters.AddWithValue("@Telefone", model.@Telefone);
                    cmd.Parameters.AddWithValue("@LogradouroNumero", model.@LogradouroNumero);
                    cmd.Parameters.AddWithValue("@Bairro", model.Bairro);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@IdCidade", model.Cidade.IdCidade);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return 0;
            }


        }

        public int Delete(int Id)
        {
            try
            {
                var sqlSelect = "delete UsuariosTeste ";
                sqlSelect += " WHERE IdUsuario = @Id  ";

                using (var con = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(sqlSelect, con)
                    {
                        CommandType = CommandType.Text
                    };

                    cmd.Parameters.AddWithValue("@Id", Id);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public Usuario Get(int Id)
        {

            var sqlSelect = " select IdUsuario, Nome, Logradouro, Telefone, ";
            sqlSelect += " LogradouroNumero, Bairro, Email ,";
            sqlSelect += " cidades.cid_codigo,  cidades.cid_nome,";
            sqlSelect += " estados.est_codigo,  estados.est_nome";
            sqlSelect += " from  UsuariosTeste";
            sqlSelect += " inner join cidades on UsuariosTeste.cid_codigo = cidades.cid_codigo";
            sqlSelect += " inner join estados on cidades.est_codigo = estados.est_codigo where IdUsuario = @Id  ";


            var model = new Usuario();
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(sqlSelect, con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@Id", Id);

                con.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    model.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                    model.Nome = reader["Nome"].ToString();
                    model.Logradouro = reader["Logradouro"].ToString();
                    model.Telefone = reader["Telefone"].ToString();
                    model.LogradouroNumero = reader["LogradouroNumero"].ToString();
                    model.Bairro = reader["Bairro"].ToString();
                    model.Email = reader["Email"].ToString();

                    var estado = new Estado();
                    estado.IdEstado = Convert.ToInt32(reader["est_codigo"]);
                    model.Estado = estado;

                    var cidade = new Cidade();
                    cidade.IdCidade = Convert.ToInt32(reader["cid_codigo"]);
                    model.Cidade = cidade;

                }
                con.Close();
            }
            return model;
        }

        public IEnumerable<Usuario> GetAll()
        {


            var sqlSelect = " select IdUsuario, Nome, Logradouro, Telefone, ";
            sqlSelect += " LogradouroNumero, Bairro, Email ,";
            sqlSelect += " cidades.cid_codigo,  cidades.cid_nome,";
            sqlSelect += " estados.est_codigo,  estados.est_nome";
            sqlSelect += "  from  UsuariosTeste  ";
            sqlSelect += " inner join cidades on UsuariosTeste.cid_codigo = cidades.cid_codigo";
            sqlSelect += " inner join estados on cidades.est_codigo = estados.est_codigo order by data ";


            var selecaosList = new List<Usuario>();
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
                    var model = new Usuario();
                    model.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                    model.Nome = reader["Nome"].ToString();
                    model.Logradouro = reader["Logradouro"].ToString();
                    model.Telefone = reader["Telefone"].ToString();
                    model.LogradouroNumero = reader["LogradouroNumero"].ToString();
                    model.Bairro = reader["Bairro"].ToString();
                    model.Email = reader["Email"].ToString();

                    var estado = new Estado();
                    estado.IdEstado = Convert.ToInt32(reader["est_codigo"]);
                    estado.Nome = reader["est_nome"].ToString();
                    model.Estado = estado;

                    var cidade = new Cidade();
                    cidade.IdCidade = Convert.ToInt32(reader["cid_codigo"]);
                    cidade.Nome = reader["cid_nome"].ToString();
                    model.Cidade = cidade;


                    selecaosList.Add(model);
                }
                con.Close();
            }
            return selecaosList;
        }

        public int Update(Usuario model)
        {
            try
            {
                var sqlSelect = "UPDATE UsuariosTeste ";
                sqlSelect += " SET Nome = @Nome";
                sqlSelect += " , Logradouro = @Logradouro";
                sqlSelect += " , Telefone = @Telefone";
                sqlSelect += " , LogradouroNumero = @LogradouroNumero";
                sqlSelect += " , Bairro = @Bairro";
                sqlSelect += " , Email = @Email";
                sqlSelect += " , cid_codigo = @IdCidade";
                sqlSelect += " WHERE IdUsuario = @Id  ";



                using (var con = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(sqlSelect, con)
                    {
                        CommandType = CommandType.Text
                    };

                    cmd.Parameters.AddWithValue("@Nome", model.Nome);
                    cmd.Parameters.AddWithValue("@Logradouro", model.Logradouro);
                    cmd.Parameters.AddWithValue("@Telefone", model.@Telefone);
                    cmd.Parameters.AddWithValue("@LogradouroNumero", model.@LogradouroNumero);
                    cmd.Parameters.AddWithValue("@Bairro", model.Bairro);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@IdCidade", model.Cidade.IdCidade);
                    cmd.Parameters.AddWithValue("@Id", model.IdUsuario);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return 0;
            }

        }
    }
}
