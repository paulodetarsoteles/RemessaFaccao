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
            List<Faccao> result = new();
            SqlCommand command = new("SELECT FaccaoId, Nome, Ativo " +
                                     "FROM dbo.Faccao (NOLOCK) " +
                                     "OPTION (MAXDOP 2);");

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                    throw new Exception("Nenhum objeto não encontrado. ");

                while (reader.Read())
                {
                    result.Add(new Faccao
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
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public Faccao GetById(int id)
        {
            Faccao result = new();
            SqlCommand command = new("dbo.FaccaoGetById");

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FaccaoId", SqlDbType.Int).Value = id;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.Read())
                    throw new Exception("Objeto não encontrado. ");

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
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + e.Message);
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public int Count()
        {
            int result;
            SqlCommand command = new("SELECT COUNT(FaccaoId) result FROM Faccao (NOLOCK) OPTION (MAXDOP 2); ");

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
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
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public bool Insert(Faccao faccao)
        {
            bool result = false;
            SqlCommand command = new("dbo.FaccaoInsert");

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = faccao.Nome;
                command.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = faccao.Endereco;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = faccao.Email;
                command.Parameters.Add("@Telefone1", SqlDbType.VarChar).Value = faccao.Telefone1;
                command.Parameters.Add("@Telefone2", SqlDbType.VarChar).Value = faccao.Telefone2;
                command.Parameters.Add("@FormaDePagamento", SqlDbType.VarChar).Value = faccao.FormaDePagamento;
                command.Parameters.Add("@Qualificacao", SqlDbType.Int).Value = faccao.Qualificacao;
                command.Parameters.Add("@Ativo", SqlDbType.Bit).Value = faccao.Ativo;
                command.Parameters.Add("@Observacoes", SqlDbType.NVarChar).Value = faccao.Observacoes;

                if (Convert.ToInt32(command.ExecuteNonQuery()) != 0)
                    result = true;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + e.Message);
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public bool Update(int id, Faccao faccao)
        {
            bool result = false;

            if (GetById(id) is null)
                return result;

            else
            {
                SqlCommand command = new("dbo.FaccaoUpdate");

                try
                {
                    command.Connection = new(_connection.SQLString);
                    command.Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@FaccaoId", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = faccao.Nome;
                    command.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = faccao.Endereco;
                    command.Parameters.Add("@Email", SqlDbType.VarChar).Value = faccao.Email;
                    command.Parameters.Add("@Telefone1", SqlDbType.VarChar).Value = faccao.Telefone1;
                    command.Parameters.Add("@Telefone2", SqlDbType.VarChar).Value = faccao.Telefone2;
                    command.Parameters.Add("@FormaDePagamento", SqlDbType.VarChar).Value = faccao.FormaDePagamento;
                    command.Parameters.Add("@Qualificacao", SqlDbType.Int).Value = faccao.Qualificacao;
                    command.Parameters.Add("@Ativo", SqlDbType.Bit).Value = faccao.Ativo;
                    command.Parameters.Add("@Observacoes", SqlDbType.NVarChar).Value = faccao.Observacoes;

                    if (command.ExecuteNonQuery() != 0)
                        result = true;
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao acessar informaçções do banco de dados. " + e.Message);
                }
                finally
                {
                    if (command.Connection.State == ConnectionState.Open)
                        command.Connection.Close();
                }
                return result;
            }
        }

        public bool Delete(int id)
        {
            bool result = false;

            if (GetById(id) is null)
                return result;

            else
            {
                SqlCommand command = new("dbo.FaccaoDelete");

                try
                {
                    command.Connection = new(_connection.SQLString);
                    command.Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@FaccaoId", SqlDbType.Int).Value = id;

                    if (command.ExecuteNonQuery() != 0)
                        result = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw new Exception("Erro ao excluir a facção, verifique se ela está vinculada a alguma remessa, por favor. ");
                }
                finally
                {
                    if (command.Connection.State == ConnectionState.Open)
                        command.Connection.Close();
                }
                return result;
            }
        }
    }
}
