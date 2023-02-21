using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Setting;
using System.Data;
using System.Data.SqlClient;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConnectionSetting _connection; 

        public UsuarioRepository(IOptions<ConnectionSetting> connection)
        {
            _connection = connection.Value;
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> result = new();
            SqlConnection connection = new(_connection.SQLString); 

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("SELECT UsuarioId, Nome, LoginUsuario FROM dbo.Usuario; "); 

                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Usuario
                        {
                            UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                            Nome = reader["Nome"].ToString(), 
                            LoginUsuario = reader["LoginUsuario"].ToString()
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

        public Usuario GetById(int id)
        {
            Usuario result = new();
            SqlConnection connection = new(_connection.SQLString); 

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("dbo.UsuarioGetById"); 

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = id; 

                    SqlDataReader reader = command.ExecuteReader(); 

                    if (reader.Read())
                    {
                        result.UsuarioId = Convert.ToInt32(reader["UsuarioId"]);
                        result.PerfilId = Convert.ToInt32(reader["PerfilId"]);
                        result.Nome = reader["Nome"].ToString();
                        result.LoginUsuario = reader["LoginUsuario"].ToString();
                        result.Senha = reader["Senha"].ToString();
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
            SqlConnection connection = new(_connection.SQLString); 

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("SELECT COUNT(UsuarioId) result FROM dbo.Usuario; "); 

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

        public bool Insert(Usuario usuario)
        {
            bool result = false;
            SqlConnection connection = new(_connection.SQLString); 

            using (connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new("dbo.UsuarioInsert");

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PerfilId", SqlDbType.TinyInt).Value = usuario.PerfilId;
                    command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = usuario.Nome;
                    command.Parameters.Add("@LoginUsuario", SqlDbType.VarChar).Value = usuario.LoginUsuario;
                    command.Parameters.Add("@Senha", SqlDbType.VarChar).Value = usuario.Senha;
                    command.Parameters.Add("@Observacoes", SqlDbType.NVarChar).Value = usuario.Observacoes;

                    if (command.ExecuteNonQuery() != 0)
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

        public bool Update(int id, Usuario usuario)
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

                        SqlCommand command = new("dbo.UsuarioUpdate");

                        command.Connection = connection; 
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = id; 
                        command.Parameters.Add("@PerfilId", SqlDbType.TinyInt).Value = usuario.PerfilId;
                        command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = usuario.Nome;
                        command.Parameters.Add("@LoginUsuario", SqlDbType.VarChar).Value = usuario.LoginUsuario;
                        command.Parameters.Add("@Senha", SqlDbType.VarChar).Value = usuario.Senha;
                        command.Parameters.Add("@Observacoes", SqlDbType.NVarChar).Value = usuario.Observacoes;

                        if (command.ExecuteNonQuery() != 0)
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

                        SqlCommand command = new("dbo.UsuarioDelete");

                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = id;

                        if (command.ExecuteNonQuery() != 0)
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
        }
    }
}
