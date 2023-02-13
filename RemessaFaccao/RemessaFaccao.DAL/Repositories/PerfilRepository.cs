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
                    throw new Exception("Erro ao conectar no banco. " + e.Message);
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

            using(connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new("dbo.PerfilGetById"); 
                    command.Connection = connection; 
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("PerfilId", id); 
                    SqlDataReader reader = command.ExecuteReader();
                    
                    if(reader.Read())
                    {
                        perfil.PerfilId = Convert.ToInt32(reader["PerfilId"]);
                        perfil.Nome = reader["Nome"].ToString(); 
                    }

                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao conectar com o banco. " + e.Message);
                }
                finally
                {
                    if(connection.State == ConnectionState.Open) connection.Close();
                }
                return perfil;
            }
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(Perfil perfil)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Perfil perfil)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
