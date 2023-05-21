using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Setting;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public class AviamentoRepository : IAviamentoRepository
    {
        private readonly ConnectionSetting _connection;
        private readonly ConnectionDbContext _connectionEf;

        public AviamentoRepository(IOptions<ConnectionSetting> connection)
        {
            _connection = connection.Value;
            _connectionEf = new ConnectionDbContext(connection);
        }

        public List<Aviamento> GetAll()
        {
            try
            {
                return _connectionEf.Aviamento.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + e.Message);
            }
        }

        public Aviamento GetById(int id)
        {
            Aviamento result = new();

            try
            {
                result = _connectionEf.Aviamento.FirstOrDefault(x => x.AviamentoId == id);

                if (result is null)
                    return null;

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + e.Message);
            }
        }

        public int Count()
        {
            try
            {
                return _connectionEf.Aviamento.Select(a => a.AviamentoId).ToList().Count;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + e.Message);
            }
        }

        public bool Insert(Aviamento aviamento)
        {
            bool result = false;

            try
            {
                _connectionEf.Add(aviamento);

                if (_connectionEf.SaveChanges() != 0)
                    result = true;
            
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + e.Message);
            }
        }

        public bool Update(int id, Aviamento aviamento)
        {
            bool result = false;

            try
            {
                aviamento.AviamentoId = id;
                _connectionEf.Update(aviamento);

                if (_connectionEf.SaveChanges() != 0)
                    result = true;
            
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + e.Message);
            }
        }

        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                Aviamento aviamento = GetById(id);

                if (aviamento is null)
                    throw new Exception("Id não encontrado. ");

                else
                {
                    _connectionEf.Aviamento.Remove(aviamento);

                    if (_connectionEf.SaveChanges() != 0)
                        result = true;
            
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + e.Message);
            }
        }
    }
}
