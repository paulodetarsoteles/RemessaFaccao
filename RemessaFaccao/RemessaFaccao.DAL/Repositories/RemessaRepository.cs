﻿using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Models.Enums;
using RemessaFaccao.DAL.Models.ViewModels;
using RemessaFaccao.DAL.Setting;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

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
            SqlCommand command = new("SELECT TOP 500 RemessaId, Referencia, StatusRemessa, Quantidade, ValorTotal FROM Remessa (NOLOCK) WHERE StatusRemessa <> 4 ORDER BY 1 DESC OPTION (MAXDOP 2);");

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                    throw new Exception("Nenhum objeto não encontrado. ");

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
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar as informações do banco de dados.");
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public List<RemessaFaccaoViewModel> GetEmFaseDeCorte()
        {
            List<RemessaFaccaoViewModel> result = new();
            SqlCommand command = new("dbo.RemessaGetEmFaseDeCorte");

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new RemessaFaccaoViewModel
                    {
                        RemessaId = Convert.ToInt32(reader["RemessaId"]),
                        Referencia = reader["Referencia"].ToString(),
                        Quantidade = Convert.ToInt32(reader["Quantidade"])
                    });
                }
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar as informações do banco de dados.");
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public List<RemessaFaccaoViewModel> GetNaoEnviadaParaProducao()
        {
            List<RemessaFaccaoViewModel> result = new();
            SqlCommand command = new("dbo.RemessaGetNaoEnviadaParaProducao");

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new RemessaFaccaoViewModel
                    {
                        RemessaId = Convert.ToInt32(reader["RemessaId"]),
                        Referencia = reader["Referencia"].ToString(),
                        Quantidade = Convert.ToInt32(reader["Quantidade"])
                    });
                }
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar as informações do banco de dados.");
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

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new RemessaFaccaoViewModel
                    {
                        RemessaId = Convert.ToInt32(reader["RemessaId"]),
                        Nome = reader["Nome"].ToString(),
                        Referencia = reader["Referencia"].ToString(),
                        Quantidade = Convert.ToInt32(reader["Quantidade"])
                    });
                }
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar as informações do banco de dados.");
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

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new RemessaFaccaoViewModel
                    {
                        RemessaId = Convert.ToInt32(reader["RemessaId"]),
                        Nome = reader["Nome"].ToString(),
                        Referencia = reader["Referencia"].ToString(),
                        Quantidade = Convert.ToInt32(reader["Quantidade"])
                    });
                }
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar as informações do banco de dados.");
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

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new RemessaFaccaoViewModel
                    {
                        RemessaId = Convert.ToInt32(reader["RemessaId"]),
                        Nome = reader["Nome"].ToString(),
                        Referencia = reader["Referencia"].ToString(),
                        Quantidade = Convert.ToInt32(reader["Quantidade"])
                    });
                }
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar as informações do banco de dados.");
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public List<Remessa> GetRecebidas(DateTime fromDate, DateTime toDate, int? faccaoId)
        {
            List<Remessa> result = new();
            SqlCommand command = new("dbo.RemessaGetRecebidasByDate");

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FaccaoId", SqlDbType.Int).Value = faccaoId;
                command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate;
                command.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate;
                command.Parameters.Add("@Status", SqlDbType.Int).Value = StatusRemessa.Recebida;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    if (command.Connection.State == ConnectionState.Open)
                        command.Connection.Close();
                    return result;
                }

                while (reader.Read())
                {
                    result.Add(new Remessa
                    {
                        RemessaId = Convert.ToInt32(reader["RemessaId"]),
                        Referencia = reader["Referencia"].ToString(),
                        DataRecebimento = Convert.ToDateTime(reader["DataRecebimento"]),
                        Quantidade = Convert.ToInt32(reader["Quantidade"]),
                        ValorTotal = Convert.ToDecimal(reader["ValorTotal"])
                    });
                }
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Filtro incorreto ou erro ao acessar informações do banco de dados.");
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

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@RemessaId", SqlDbType.Int).Value = id;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    command.Connection.Close();
                    throw new Exception("Objeto não encontrado. ");
                }

                result.RemessaId = Convert.ToInt32(reader["RemessaId"]);
                result.Referencia = reader["Referencia"].ToString();
                result.Piloto = reader["Piloto"].ToString();
                result.Modelo = reader["Modelo"].ToString();
                result.Tecido = reader["Tecido"].ToString();
                result.Descricao = reader["Descricao"].ToString();
                result.Quantidade = Convert.ToInt32(reader["Quantidade"]);
                result.ValorUnitario = Convert.ToDecimal(reader["ValorUnitario"]);
                result.ValorTotal = Convert.ToDecimal(reader["ValorTotal"]);
                result.DataDeEntrega = Convert.ToDateTime(reader["DataDeEntrega"]);
                result.DataPrazo = Convert.ToDateTime(reader["DataPrazo"]);
                result.DataRecebimento = Convert.ToDateTime(reader["DataRecebimento"]);
                result.StatusRemessa = (StatusRemessa)Convert.ToInt16(reader["StatusRemessa"]);
                result.Observacoes = reader["Observacoes"].ToString();
                result.Desenho = reader["Desenho"].ToString();
                result.Tamanho1 = Convert.ToInt32(reader["Tamanho1"]);
                result.Tamanho2 = Convert.ToInt32(reader["Tamanho2"]);
                result.Tamanho4 = Convert.ToInt32(reader["Tamanho4"]);
                result.Tamanho6 = Convert.ToInt32(reader["Tamanho6"]);
                result.Tamanho8 = Convert.ToInt32(reader["Tamanho8"]);
                result.Tamanho10 = Convert.ToInt32(reader["Tamanho10"]);
                result.Tamanho12 = Convert.ToInt32(reader["Tamanho12"]);

                //Buscar Facção
                if (reader["FaccaoId"] != DBNull.Value)
                {
                    result.FaccaoId = Convert.ToInt32(reader["FaccaoId"]);

                    Faccao resultFaccao = new();
                    SqlCommand commandFaccao = new("dbo.FaccaoGetById");

                    try
                    {
                        commandFaccao.Connection = new(_connection.SQLString);
                        commandFaccao.Connection.Open();
                        commandFaccao.CommandType = CommandType.StoredProcedure;

                        commandFaccao.Parameters.Add("@FaccaoId", SqlDbType.Int).Value = result.FaccaoId;

                        SqlDataReader readerF = commandFaccao.ExecuteReader();

                        if (readerF.Read())
                        {
                            resultFaccao.FaccaoId = Convert.ToInt32(readerF["FaccaoId"]);
                            resultFaccao.Nome = readerF["Nome"].ToString();
                            resultFaccao.Endereco = readerF["Endereco"].ToString();
                            resultFaccao.Email = readerF["Email"].ToString();
                            resultFaccao.Telefone1 = readerF["Telefone1"].ToString();
                            resultFaccao.Telefone1 = readerF["Telefone2"].ToString();
                            resultFaccao.FormaDePagamento = readerF["FormaDePagamento"].ToString();
                            resultFaccao.Qualificacao = Convert.ToInt32(readerF["Qualificacao"]);
                            resultFaccao.Ativo = Convert.ToBoolean(readerF["Ativo"]);
                            resultFaccao.Observacoes = readerF["Observacoes"].ToString();
                        }

                        result.Faccao = resultFaccao;
                    }
                    catch (Exception e)
                    {
                        ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                        throw new Exception("Erro ao acessar informações do banco de dados.");
                    }
                    finally
                    {
                        if (commandFaccao.Connection.State == ConnectionState.Open)
                            commandFaccao.Connection.Close();
                    }
                }
                else
                    result.FaccaoId = null;

                try
                {
                    List<Aviamento> resultAviamentos = new();
                    Aviamento aviamento = new();
                    SqlCommand commandAviamento = new("dbo.AviamentoGetByRemessaId")
                    {
                        Connection = new(_connection.SQLString),
                        CommandType = CommandType.StoredProcedure
                    };

                    commandAviamento.Connection.Open();
                    commandAviamento.Parameters.Add("@RemessaId", SqlDbType.Int).Value = result.RemessaId;

                    SqlDataReader readerA = commandAviamento.ExecuteReader();

                    while (readerA.Read())
                    {
                        resultAviamentos.Add(new Aviamento
                        {
                            AviamentoId = Convert.ToInt32(readerA["AviamentoId"]),
                            Nome = readerA["Nome"].ToString()
                        });
                    }

                    if (commandAviamento.Connection.State == ConnectionState.Open)
                        commandAviamento.Connection.Close();

                    result.Aviamentos = resultAviamentos;
                }
                catch (Exception e)
                {
                    ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                    throw new Exception("Erro ao acessar informações do banco de dados.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            if (command.Connection.State == ConnectionState.Open)
                command.Connection.Close();

            return result;
        }

        public int Count()
        {
            int result = 0;
            SqlCommand command = new("SELECT COUNT(RemessaId) result FROM Remessa (NOLOCK) OPTION (MAXDOP 2);");

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                result = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public int CountEmFaseDeCorte()
        {
            int result = 0;
            SqlCommand command = new SqlCommand("SELECT COUNT(RemessaID) FROM Remessa (NOLOCK) WHERE StatusRemessa = 0 OPTION (MAXDOP 2);");

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                result = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
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
            SqlCommand command = new("SELECT COUNT(RemessaID) FROM Remessa (NOLOCK) WHERE StatusRemessa = 1 OPTION (MAXDOP 2);");

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                result = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
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
            SqlCommand command = new("SELECT COUNT(RemessaID) FROM Remessa (NOLOCK) WHERE StatusRemessa = 2 OPTION (MAXDOP 2);");

            try
            {

                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                result = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
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
            SqlCommand command = new("SELECT COUNT(RemessaID) result FROM Remessa (NOLOCK) WHERE StatusRemessa = 3 OPTION (MAXDOP 2);");

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                result = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
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
            SqlCommand command = new("SELECT COUNT(RemessaID) result FROM Remessa (NOLOCK) WHERE CONVERT(date, DataPrazo) = CONVERT(date, GETDATE()) OPTION (MAXDOP 2);");

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;
                result = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
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
                command.Parameters.Add("@Piloto", SqlDbType.VarChar).Value = remessa.Piloto;
                command.Parameters.Add("@Modelo", SqlDbType.VarChar).Value = remessa.Modelo;
                command.Parameters.Add("@Tecido", SqlDbType.VarChar).Value = remessa.Tecido;
                command.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = remessa.Descricao;
                command.Parameters.Add("@Quantidade", SqlDbType.Int).Value = remessa.Quantidade;
                command.Parameters.Add("@Tamanho1", SqlDbType.Int).Value = remessa.Tamanho1;
                command.Parameters.Add("@Tamanho2", SqlDbType.Int).Value = remessa.Tamanho2;
                command.Parameters.Add("@Tamanho4", SqlDbType.Int).Value = remessa.Tamanho4;
                command.Parameters.Add("@Tamanho6", SqlDbType.Int).Value = remessa.Tamanho6;
                command.Parameters.Add("@Tamanho8", SqlDbType.Int).Value = remessa.Tamanho8;
                command.Parameters.Add("@Tamanho10", SqlDbType.Int).Value = remessa.Tamanho10;
                command.Parameters.Add("@Tamanho12", SqlDbType.Int).Value = remessa.Tamanho12;
                command.Parameters.Add("@ValorUnitario", SqlDbType.Decimal).Value = remessa.ValorUnitario;
                command.Parameters.Add("@ValorTotal", SqlDbType.Decimal).Value = remessa.ValorTotal;
                command.Parameters.Add("@DataDeEntrega", SqlDbType.DateTime).Value = remessa.DataDeEntrega;
                command.Parameters.Add("@DataPrazo", SqlDbType.DateTime).Value = remessa.DataPrazo;
                command.Parameters.Add("@DataRecebimento", SqlDbType.DateTime).Value = remessa.DataRecebimento;
                command.Parameters.Add("@StatusRemessa", SqlDbType.TinyInt).Value = remessa.StatusRemessa;
                command.Parameters.Add("@Observacoes", SqlDbType.NVarChar).Value = remessa.Observacoes;
                command.Parameters.Add("@Desenho", SqlDbType.NVarChar).Value = remessa.Desenho;

                remessa.RemessaId = (int)command.ExecuteScalar();

                if (remessa.RemessaId != 0)
                    result = true;

                if (remessa.Aviamentos.Count > 0)
                {
                    foreach (Aviamento aviamento in remessa.Aviamentos)
                    {
                        SqlCommand commandInsertAviamentos = new("dbo.AviamentoRemessaInsert")
                        {
                            Connection = new(_connection.SQLString),
                            CommandType = CommandType.StoredProcedure
                        };

                        commandInsertAviamentos.Connection.Open();
                        commandInsertAviamentos.Parameters.Add("@RemessaId", SqlDbType.Int).Value = remessa.RemessaId;
                        commandInsertAviamentos.Parameters.Add("@AviamentoId", SqlDbType.Int).Value = aviamento.AviamentoId;
                        commandInsertAviamentos.ExecuteNonQuery();
                        commandInsertAviamentos.Connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
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
                throw new Exception("Id da remessa não encontrado no banco de dados. ");

            try
            {
                command.Connection = new SqlConnection(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@RemessaId", SqlDbType.Int).Value = id;
                command.Parameters.Add("@FaccaoId", SqlDbType.Int).Value = remessa.FaccaoId;
                command.Parameters.Add("@Referencia", SqlDbType.VarChar).Value = remessa.Referencia;
                command.Parameters.Add("@Piloto", SqlDbType.VarChar).Value = remessa.Piloto;
                command.Parameters.Add("@Modelo", SqlDbType.VarChar).Value = remessa.Modelo;
                command.Parameters.Add("@Tecido", SqlDbType.VarChar).Value = remessa.Tecido;
                command.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = remessa.Descricao;
                command.Parameters.Add("@Quantidade", SqlDbType.Int).Value = remessa.Quantidade;
                command.Parameters.Add("@Tamanho1", SqlDbType.Int).Value = remessa.Tamanho1;
                command.Parameters.Add("@Tamanho2", SqlDbType.Int).Value = remessa.Tamanho2;
                command.Parameters.Add("@Tamanho4", SqlDbType.Int).Value = remessa.Tamanho4;
                command.Parameters.Add("@Tamanho6", SqlDbType.Int).Value = remessa.Tamanho6;
                command.Parameters.Add("@Tamanho8", SqlDbType.Int).Value = remessa.Tamanho8;
                command.Parameters.Add("@Tamanho10", SqlDbType.Int).Value = remessa.Tamanho10;
                command.Parameters.Add("@Tamanho12", SqlDbType.Int).Value = remessa.Tamanho12;
                command.Parameters.Add("@ValorUnitario", SqlDbType.Decimal).Value = remessa.ValorUnitario;
                command.Parameters.Add("@ValorTotal", SqlDbType.Decimal).Value = remessa.ValorTotal;
                command.Parameters.Add("@DataDeEntrega", SqlDbType.DateTime).Value = remessa.DataDeEntrega;
                command.Parameters.Add("@DataPrazo", SqlDbType.DateTime).Value = remessa.DataPrazo;
                command.Parameters.Add("@DataRecebimento", SqlDbType.DateTime).Value = remessa.DataRecebimento;
                command.Parameters.Add("@StatusRemessa", SqlDbType.TinyInt).Value = remessa.StatusRemessa;
                command.Parameters.Add("@Observacoes", SqlDbType.NVarChar).Value = remessa.Observacoes;
                command.Parameters.Add("@Desenho", SqlDbType.NVarChar).Value = remessa.Desenho;

                if (Convert.ToInt32(command.ExecuteNonQuery()) != 0)
                    result = true;

                SqlCommand commandDeleteAviamentos = new("dbo.AviamentoRemessaDeleteByRemessaId")
                {
                    Connection = new(_connection.SQLString),
                    CommandType = CommandType.StoredProcedure
                };

                commandDeleteAviamentos.Connection.Open();
                commandDeleteAviamentos.Parameters.Add("@RemessaId", SqlDbType.Int).Value = remessa.RemessaId;
                commandDeleteAviamentos.ExecuteNonQuery();
                commandDeleteAviamentos.Connection.Close();

                if (remessa.Aviamentos.Count > 0)
                {
                    foreach (Aviamento aviamento in remessa.Aviamentos)
                    {
                        SqlCommand commandInsertAviamentos = new("dbo.AviamentoRemessaInsert")
                        {
                            Connection = new(_connection.SQLString),
                            CommandType = CommandType.StoredProcedure
                        };

                        commandInsertAviamentos.Connection.Open();
                        commandInsertAviamentos.Parameters.Add("@RemessaId", SqlDbType.Int).Value = remessa.RemessaId;
                        commandInsertAviamentos.Parameters.Add("@AviamentoId", SqlDbType.Int).Value = aviamento.AviamentoId;
                        commandInsertAviamentos.ExecuteNonQuery();
                        commandInsertAviamentos.Connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
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
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
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

            if (GetById(id) is null)
                throw new Exception("Id da remessa não encontrado no banco de dados. ");

            else
            {
                SqlCommand command = new("dbo.RemessaDelete");

                try
                {
                    List<Aviamento> resultAviamentos = new();
                    Aviamento aviamento = new();
                    SqlCommand commandAviamento = new("dbo.AviamentoGetByRemessaId")
                    {
                        Connection = new(_connection.SQLString),
                        CommandType = CommandType.StoredProcedure
                    };

                    commandAviamento.Connection.Open();
                    commandAviamento.Parameters.Add("@RemessaId", SqlDbType.Int).Value = id;

                    if (commandAviamento.ExecuteReader().HasRows)
                    {
                        SqlCommand command1 = new("dbo.AviamentoRemessaDeleteByRemessaId")
                        {
                            Connection = new(_connection.SQLString),
                            CommandType = CommandType.StoredProcedure
                        };

                        command1.Connection.Open();
                        command1.Parameters.Add("@RemessaId", SqlDbType.Int).Value = id;
                        command1.ExecuteNonQuery();
                        command1.Connection.Close();
                    }

                    if (commandAviamento.Connection.State == ConnectionState.Open)
                        commandAviamento.Connection.Close();

                    command.Connection = new SqlConnection(_connection.SQLString);
                    command.Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@RemessaId", SqlDbType.Int).Value = id;

                    if (command.ExecuteNonQuery() != 0)
                        result = true;
                }
                catch (Exception e)
                {
                    ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                    throw new Exception("Erro ao acessar informações do banco de dados.");
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
            SqlCommand command = new("SELECT FaccaoId, Nome FROM dbo.Faccao (NOLOCK) ORDER BY Faccao.Nome OPTION (MAXDOP 2);");

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new Faccao
                    {
                        FaccaoId = Convert.ToInt32(reader["FaccaoId"]),
                        Nome = reader["Nome"].ToString()
                    });
                }
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public List<Faccao> GetFaccoesAtivas()
        {
            List<Faccao> result = new();
            SqlCommand command = new("SELECT FaccaoId, Nome, Ativo FROM dbo.Faccao (NOLOCK) WHERE Faccao.Ativo = 1 OPTION (MAXDOP 2);");

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.Text;

                SqlDataReader reader = command.ExecuteReader();

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
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public List<Aviamento> GetAviamentosParaRemessa()
        {
            List<Aviamento> result = new();
            SqlCommand command = new("dbo.GetAviamentosParaRemessa");

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new Aviamento
                    {
                        AviamentoId = Convert.ToInt32(reader["AviamentoId"]),
                        Nome = reader["Nome"].ToString()
                    });
                }
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
            }
            return result;
        }

        public bool ValidateReferencia(string referencia)
        {
            bool result = false;

            List<RemessaReferenciaViewModel> remessaReferencias = new();
            SqlCommand command = new("dbo.RemessaGetReferenciasAtivas");

            try
            {
                command.Connection = new(_connection.SQLString);
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    remessaReferencias.Add(new RemessaReferenciaViewModel
                    {
                        RemessaId = Convert.ToInt32(reader["RemessaId"]),
                        Referencia = reader["Referencia"].ToString()
                    });
                }

                if (remessaReferencias.Any(r => r.Referencia.ToLower().Trim() == referencia.ToLower().Trim()))
                    result = true;
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("RemessaRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
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
