using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Models;
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
            SqlConnection connection = new(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("SELECT TOP 100 RemessaId, Referencia, Quantidade, ValorTotal " +
                                             "FROM Remessa (NOLOCK) " +
                                             "WHERE StatusRemessa <> 4; ");

                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Remessa
                        {
                            RemessaId = Convert.ToInt32(reader["RemessaId"]),
                            Referencia = reader["Referencia"].ToString(),
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
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public List<Remessa> GetNaoEnviadaParaProducao()
        {
            List<Remessa> result = new();
            SqlConnection connection = new(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("dbo.RemessaGetNaoEnviadaParaProducao");

                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();

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
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public List<RemessaFaccaoViewModel> GetAtrasadas()
        {
            List<RemessaFaccaoViewModel> result = new();
            SqlConnection connection = new(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("dbo.RemessaGetAtrasadas");

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();

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
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public List<RemessaFaccaoViewModel> GetEmProducao()
        {
            List<RemessaFaccaoViewModel> result = new();
            SqlConnection connection = new(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("dbo.RemessaGetEmProducao");

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();

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
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public List<RemessaFaccaoViewModel> GetReceberHoje()
        {
            List<RemessaFaccaoViewModel> result = new();
            SqlConnection connection = new(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("dbo.RemessaGetReceberHoje");

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();

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
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public Remessa GetById(int id)
        {
            Remessa result = new();
            SqlConnection connection = new(_connection.SQLString);
            SqlCommand command = new("dbo.RemessaGetById; ");

            try
            {
                command.Connection = connection;
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@RemessaID", SqlDbType.Int).Value = id; 

                SqlDataReader reader = command.ExecuteReader(); 

                if (reader.Read())
                {
                    result.RemessaId = Convert.ToInt32(reader["RemessaId"]);
                    result.FaccaoId = Convert.ToInt32(reader["FaccaoId"]);
                    result.Referencia = reader["Referencia"].ToString();
                    result.Quantidade = Convert.ToInt32(reader["Quantidade"]);
                    result.ValorUnitario = Convert.ToDecimal(reader["ValorUnitario"]);
                    result.ValorTotal = Convert.ToDecimal(reader["ValorTotal"]); 
                    result.DataDeEntrega = Convert.ToDateTime(reader["DataDeEntrega"]);
                    result.DataPrazo = Convert.ToDateTime(reader["DataPrazo"]); 
                    result.DataRecebimento = Convert.ToDateTime(reader["DataPrazo"]);
                    result.StatusRemessa = (Enums.StatusRemessa)Convert.ToInt16(reader["StatusRemessa"]); 
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

        public int Count()
        {
            int result = 0;
            SqlConnection connection = new(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("SELECT COUNT(RemessaId) result FROM Remessa; ");

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
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public int CountAtrasadas()
        {
            int result = 0;
            SqlConnection connection = new(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("SELECT COUNT(RemessaID) result " +
                                             "FROM Faccao " +
                                             "WHERE r.DataDeEntrega < GETDATE(); ");

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();

                    result = Convert.ToInt32(reader["result"]);

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

        public int CountReceberHoje()
        {
            int result = 0;
            SqlConnection connection = new(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("SELECT COUNT(RemessaID) result " +
                                             "FROM Faccao " +
                                             "WHERE r.DataDeEntrega = GETDATE(); ");

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();

                    result = Convert.ToInt32(reader["result"]);

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

        public bool Insert(Remessa remessa)
        {
            bool result = false;
            SqlConnection connection = new(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("dbo.RemessaInsert");

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@FaccaoId", SqlDbType.VarChar).Value = remessa.FaccaoId;
                    command.Parameters.Add("@Referencia", SqlDbType.VarChar).Value = remessa.Referencia;
                    command.Parameters.Add("@Quantidade", SqlDbType.VarChar).Value = remessa.Quantidade;
                    command.Parameters.Add("@ValorUnitario", SqlDbType.VarChar).Value = remessa.ValorUnitario;
                    command.Parameters.Add("@ValorTotal", SqlDbType.VarChar).Value = remessa.ValorTotal;
                    command.Parameters.Add("@DataDeEntrega", SqlDbType.VarChar).Value = remessa.DataDeEntrega;
                    command.Parameters.Add("@DataPrazo", SqlDbType.Int).Value = remessa.DataPrazo;
                    command.Parameters.Add("@DataRecebimento", SqlDbType.Bit).Value = remessa.DataRecebimento;
                    command.Parameters.Add("@StatusRemessa", SqlDbType.Bit).Value = remessa.StatusRemessa;
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
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public bool Update(Remessa remessa)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus()
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
