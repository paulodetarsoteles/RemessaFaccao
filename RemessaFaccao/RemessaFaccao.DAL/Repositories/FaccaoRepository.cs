using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Setting;
using System.Data;
using System.Data.SqlClient;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public class FaccaoRepository : IFaccaoRepository
    {
        private readonly ConnectionSetting _connection;

        public FaccaoRepository(IOptions<ConnectionSetting> connection)
        {
            _connection = connection.Value;
        }

        public List<Faccao> GetAll()
        {
            List<Faccao> result= new List<Faccao>();
            SqlConnection connection = new SqlConnection(_connection.SQLString); 

            using(connection)
            {
                try
                {
                    connection.Open(); 
                    SqlCommand command = new SqlCommand("SELECT FaccaoId, Nome, Ativo FROM dbo.Faccao; "); 
                    command.Connection = connection;
                    command.CommandType = CommandType.Text; 
                    SqlDataReader reader = command.ExecuteReader(); 

                    while (reader.Read())
                    {
                        result.Add( new Faccao
                        {
                            FaccaoId = Convert.ToInt32(reader["FaccaoId"]), 
                            Nome = reader["Nome"].ToString(), 
                            Ativo = Convert.ToBoolean(reader["Ativo"])
                        }); 
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao acessar informações do banco de dados. " + e.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public Faccao GetById(int id)
        {
            Faccao result = new Faccao(); 
            SqlConnection connection = new SqlConnection(_connection.SQLString); 

            using(connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("dbo.FaccaoGetById"); 
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure; 
                    command.Parameters.Add("@FaccaoId", SqlDbType.Int).Value = id;
                    SqlDataReader reader = command.ExecuteReader(); 

                    if (reader.Read())
                    {
                        result.FaccaoId = Convert.ToInt32(reader["FaccaoId"]);
                        result.Nome = reader["Nome"].ToString();
                        result.Endereco = reader["Endereco"].ToString();
                        result.Email = reader["Email"].ToString();
                        result.Telefone1 = reader["Telefone1"].ToString();
                        result.Telefone1 = reader["Telefone2"].ToString(); 
                        result.FormaDePagamento = reader["FormaDePagamento"].ToString();
                        result.Qualificacao = Convert.ToInt32(reader["Qualificacao"]); 
                        result.Ativo = Convert.ToBoolean(reader["Ativo"]);
                        result.Observacoes = reader["Observacoes"].ToString(); 
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao acessar informações do banco de dados. " + e.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public int Count()
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT COUNT(FaccaoId) result FROM Faccao; "); 
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    result = Convert.ToInt32(reader["result"]); 
                    
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao acessar informações do banco de dados. " + e.Message);
                }
                finally
                {
                    if(connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result; 
            }
        }

        public bool Insert(Faccao faccao)
        {
            throw new NotImplementedException();
        }

        public bool Update(Faccao faccao)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
