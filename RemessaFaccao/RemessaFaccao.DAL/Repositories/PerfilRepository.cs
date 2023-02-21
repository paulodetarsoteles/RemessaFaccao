using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Setting;
using System.Data;
using System.Data.SqlClient;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly ConnectionSetting _connection;

        public PerfilRepository(IOptions<ConnectionSetting> connection)
        {
            _connection = connection.Value;
        }

        public List<Perfil> GetAll()
        {
            List<Perfil> result = new();
            SqlConnection connection = new(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("SELECT * FROM Perfil;");

                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Perfil
                        {
                            PerfilId = Convert.ToInt32(reader["PerfilId"]),
                            Nome = reader["Nome"].ToString()
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

        public Perfil GetById(int id)
        {
            Perfil perfil = new();
            SqlConnection connection = new(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("dbo.PerfilGetById");

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("PerfilId", SqlDbType.Int).Value = id;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        perfil.PerfilId = Convert.ToInt32(reader["PerfilId"]);
                        perfil.Nome = reader["Nome"].ToString();
                    }

                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao acessar as informações do banco de dados. " + e.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open) connection.Close();
                }
                return perfil;
            }
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

                    SqlCommand command = new("SELECT COUNT(PerfilId) result FROM Perfil;");

                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();

                    result = Convert.ToInt32(reader["result"]);
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

        public bool Insert(Perfil perfil)
        {
            bool result = false;
            SqlConnection connection = new(_connection.SQLString);

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("dbo.PerfilInsert");

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = perfil.Nome;

                    if (Convert.ToInt32(command.ExecuteNonQuery()) != 0)
                        result = true;
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

        public bool Update(int id, Perfil perfil)
        {
            bool result = false;

            if (GetById(id) is null)
            {
                return result;
            }
            else
            {
                SqlConnection connection = new(_connection.SQLString);

                using (connection)
                {
                    try
                    {
                        connection.Open();

                        SqlCommand command = new("dbo.PerfilUpdate");

                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PerfilId", SqlDbType.Int).Value = id;
                        command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = perfil.Nome;

                        if (Convert.ToInt32(command.ExecuteNonQuery()) != 0)
                            result = true;
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
        }

        public bool Delete(int id)
        {
            bool result = false;

            if (GetById(id) is null)
            {
                return result;
            }
            else
            {
                SqlConnection connection = new(_connection.SQLString);

                using (connection)
                {
                    try
                    {
                        connection.Open();

                        SqlCommand command = new("dbo.PerfilDelete");

                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PerfilId", SqlDbType.Int).Value = id;

                        if (Convert.ToInt32(command.ExecuteNonQuery()) != 0)
                            result = true;
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erro ao acessar o banco de dados. " + e.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                    return result;
                }
            }
        }
    }
}
