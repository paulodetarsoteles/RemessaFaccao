using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Models.Enums;
using RemessaFaccao.DAL.Models.ViewModels;
using RemessaFaccao.DAL.Setting;
using System.Data;
using System.Data.SqlClient;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public class RemessaRepository : IRemessaRepository
    {
        private readonly ConnectionSetting _connection;

        public RemessaRepository(IOptions<ConnectionSetting> connection)
        {
            _connection = connection.Value;
        }

        public List<Remessa> GetAll()
        {
            List<Remessa> result = new();
            SqlCommand command = new("SELECT TOP 100 RemessaId, Referencia, StatusRemessa ,Quantidade, ValorTotal " +
                         "FROM Remessa (NOLOCK) " +
                         "WHERE StatusRemessa <> 4 " +
                         "ORDER BY StatusRemessa DESC, " +
                         "FaccaoId ASC; ");
            SqlDataReader reader;

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new Remessa
                    {
                        RemessaId = Convert.ToInt32(reader["RemessaId"]),
                        Referencia = reader["Referencia"].ToString(),
                        StatusRemessa = (StatusRemessa)Convert.ToInt16(reader["StatusRemessa"]),
                        Quantidade = Convert.ToInt32(reader["Quantidade"]),
                        ValorTotal = Convert.ToDecimal(reader["ValorTotal"])
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar as informações do banco de dados. " + e.Message);
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public List<Remessa> GetNaoEnviadaParaProducao()
        {
            List<Remessa> result = new();
            SqlCommand command = new("dbo.RemessaGetNaoEnviadaParaProducao");
            SqlDataReader reader;

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new Remessa
                    {
                        Referencia = reader["Referencia"].ToString(),
                        Quantidade = Convert.ToInt32(reader["Quantidade"])
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar as informações do banco de dados. " + e.Message);
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public List<RemessaFaccaoViewModel> GetAtrasadas()
        {
            List<RemessaFaccaoViewModel> result = new();
            SqlCommand command = new("dbo.RemessaGetAtrasadas");
            SqlDataReader reader;

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new RemessaFaccaoViewModel
                    {
                        Nome = reader["Nome"].ToString(),
                        Referencia = reader["Referencia"].ToString(),
                        Quantidade = Convert.ToInt32(reader["Quantidade"])
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar as informações do banco de dados. " + e.Message);
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public List<RemessaFaccaoViewModel> GetEmProducao()
        {
            List<RemessaFaccaoViewModel> result = new();
            SqlCommand command = new("dbo.RemessaGetEmProducao");
            SqlDataReader reader;

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new RemessaFaccaoViewModel
                    {
                        Nome = reader["Nome"].ToString(),
                        Referencia = reader["Referencia"].ToString(),
                        Quantidade = Convert.ToInt32(reader["Quantidade"])
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar as informações do banco de dados. " + e.Message);
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public List<RemessaFaccaoViewModel> GetReceberHoje()
        {
            List<RemessaFaccaoViewModel> result = new();
            SqlCommand command = new("dbo.RemessaGetReceberHoje");
            SqlDataReader reader;

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new RemessaFaccaoViewModel
                    {
                        Nome = reader["Nome"].ToString(),
                        Referencia = reader["Referencia"].ToString(),
                        Quantidade = Convert.ToInt32(reader["Quantidade"])
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar as informações do banco de dados. " + e.Message);
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public Remessa GetById(int id)
        {
            Remessa result = new();
            SqlCommand command = new("dbo.RemessaGetById");
            SqlDataReader reader;

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@RemessaID", SqlDbType.Int).Value = id;

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result.RemessaId = Convert.ToInt32(reader["RemessaId"]);
                    result.Referencia = reader["Referencia"].ToString();
                    result.Quantidade = Convert.ToInt32(reader["Quantidade"]);
                    result.ValorUnitario = Convert.ToDecimal(reader["ValorUnitario"]);
                    result.ValorTotal = Convert.ToDecimal(reader["ValorTotal"]);
                    result.DataDeEntrega = Convert.ToDateTime(reader["DataDeEntrega"]);
                    result.DataPrazo = Convert.ToDateTime(reader["DataPrazo"]);
                    result.DataRecebimento = Convert.ToDateTime(reader["DataRecebimento"]);
                    result.StatusRemessa = (StatusRemessa)Convert.ToInt16(reader["StatusRemessa"]);
                    result.Observacoes = reader["Observacoes"].ToString();

                    if (reader["FaccaoId"] != DBNull.Value)
                        result.FaccaoId = Convert.ToInt32(reader["FaccaoId"]);
                    else
                        result.FaccaoId = null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + e.Message);
            }

            if (command.Connection.State == ConnectionState.Open)
                command.Connection.Close();

            return result;
        }

        public int Count()
        {
            int result = 0;
            SqlCommand command = new("SELECT COUNT(RemessaId) result FROM Remessa");

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                result = (int)command.ExecuteScalar();
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

        public int CountEnviarParaProducao()
        {
            int result = 0;
            SqlCommand command = new("SELECT COUNT(RemessaID) " +
                                     "FROM Remessa " +
                                     "WHERE StatusRemessa = 1; ");

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                result = (int)command.ExecuteScalar();
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

        public int CountEmProducao()
        {
            int result = 0;
            SqlCommand command = new("SELECT COUNT(RemessaID) " +
                                     "FROM Remessa " +
                                     "WHERE StatusRemessa = 2; ");

            try
            {

                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                result = (int)command.ExecuteScalar();
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

        public int CountAtrasadas()
        {
            int result = 0;
            SqlCommand command = new("SELECT COUNT(RemessaID) result " +
                                     "FROM Remessa " +
                                     "WHERE StatusRemessa = 3; ");

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                result = (int)command.ExecuteScalar();
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

        public int CountReceberHoje()
        {
            int result = 0;
            SqlCommand command = new("SELECT COUNT(RemessaID) result " +
                                     "FROM Remessa " +
                                     "WHERE StatusRemessa = 2 " +
                                     "AND DataDeEntrega = GETDATE(); ");

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                result = (int)command.ExecuteScalar();
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

        public bool Insert(Remessa remessa)
        {
            bool result = false;
            SqlCommand command = new("dbo.RemessaInsert");

            try
            {
                command.Connection = new(_connection.SQLString); ;
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FaccaoId", SqlDbType.Int).Value = remessa.FaccaoId;
                command.Parameters.Add("@Referencia", SqlDbType.VarChar).Value = remessa.Referencia;
                command.Parameters.Add("@Quantidade", SqlDbType.Int).Value = remessa.Quantidade;
                command.Parameters.Add("@ValorUnitario", SqlDbType.Decimal).Value = remessa.ValorUnitario;
                command.Parameters.Add("@ValorTotal", SqlDbType.Decimal).Value = remessa.ValorTotal;
                command.Parameters.Add("@DataDeEntrega", SqlDbType.DateTime).Value = remessa.DataDeEntrega;
                command.Parameters.Add("@DataPrazo", SqlDbType.DateTime).Value = remessa.DataPrazo;
                command.Parameters.Add("@DataRecebimento", SqlDbType.DateTime).Value = remessa.DataRecebimento;
                command.Parameters.Add("@StatusRemessa", SqlDbType.TinyInt).Value = remessa.StatusRemessa;
                command.Parameters.Add("@Observacoes", SqlDbType.NVarChar).Value = remessa.Observacoes;

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

        public bool Update(int id, Remessa remessa)
        {
            bool result = false;
            SqlCommand command = new("dbo.RemessaUpdate");

            if (GetById(id) is null)
                throw new Exception("ID da remessa não encontrado no banco de dados. ");

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@RemessaId", SqlDbType.Int).Value = id;
                command.Parameters.Add("@FaccaoId", SqlDbType.Int).Value = remessa.FaccaoId;
                command.Parameters.Add("@Referencia", SqlDbType.VarChar).Value = remessa.Referencia;
                command.Parameters.Add("@Quantidade", SqlDbType.Int).Value = remessa.Quantidade;
                command.Parameters.Add("@ValorUnitario", SqlDbType.Decimal).Value = remessa.ValorUnitario;
                command.Parameters.Add("@ValorTotal", SqlDbType.Decimal).Value = remessa.ValorTotal;
                command.Parameters.Add("@DataDeEntrega", SqlDbType.DateTime).Value = remessa.DataDeEntrega;
                command.Parameters.Add("@DataPrazo", SqlDbType.DateTime).Value = remessa.DataPrazo;
                command.Parameters.Add("@DataRecebimento", SqlDbType.DateTime).Value = remessa.DataRecebimento;
                command.Parameters.Add("@StatusRemessa", SqlDbType.TinyInt).Value = remessa.StatusRemessa;
                command.Parameters.Add("@Observacoes", SqlDbType.NVarChar).Value = remessa.Observacoes;

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

        public void UpdateStatus()
        {
            SqlCommand command = new("dbo.RemessaUpdateStatus ");

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
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
        }

        public bool Delete(int id)
        {
            bool result = false;
            SqlCommand command = new("dbo.RemessaDelete");

            if (GetById(id) is null)
                throw new Exception("ID da remessa não encontrado no banco de dados. ");

            else
            {
                try
                {
                    command.Connection = new SqlConnection(_connection.SQLString);
                    command.Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@RemessaId", SqlDbType.Int).Value = id;

                    if (command.ExecuteNonQuery() != 0)
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
        }

        public List<Faccao> GetFaccoes()
        {
            List<Faccao> result = new();
            SqlCommand command = new("SELECT FaccaoId, Nome, Ativo " +
                                     "FROM dbo.Faccao (NOLOCK); ");
            SqlDataReader reader;

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader();

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
    }
}
