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
            catch (Exception ex)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + ex.Message);
            }
        }

        public Aviamento GetById(int id)
        {
            Aviamento result = new(); 

            try
            {
                result = _connectionEf.Aviamento.FirstOrDefault(x => x.AviamentoId == id);

                if (result == null)
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + ex.Message);
            }
            return result;
        }

        public int Count()
        {
            int result = 0;

            try
            {
                result = _connectionEf.Aviamento.Select(a => a.AviamentoId).ToList().Count;
                
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + ex.Message);
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
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao acessar informações do banco de dados. " + ex.Message);
            }
            return result;
        }

        public bool Update(int id, Aviamento aviamento)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
