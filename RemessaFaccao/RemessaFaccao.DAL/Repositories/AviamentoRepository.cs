using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Repositories.Interfaces;
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
            Aviamento result;
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
            throw new NotImplementedException();
        }

        public bool Insert(Aviamento aviamento)
        {
            throw new NotImplementedException();
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
