using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Setting;
using System.Reflection;

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
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
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
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
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
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
            }
        }

        public bool Insert(Aviamento aviamento)
        {
            bool result;
            try
            {
                _connectionEf.Add(aviamento);

                if (_connectionEf.SaveChanges() == 0)
                    throw new Exception("Falha ao salvar no banco de dados");

                result = true;
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
            }
            return result;
        }

        public bool Update(int id, Aviamento aviamento)
        {
            bool result;

            try
            {
                _ = GetById(id) ?? throw new Exception("Id não encontrado");

                aviamento.AviamentoId = id;

                _connectionEf.Update(aviamento);

                if (_connectionEf.SaveChanges() == 0)
                    throw new Exception("Falha ao salvar no banco de dados");

                result = true;
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
            }
            return result;
        }

        public bool Delete(int id)
        {
            bool result;

            try
            {
                Aviamento aviamento = GetById(id) ?? throw new Exception("Id não encontrado");

                _connectionEf.Aviamento.Remove(aviamento);

                if (_connectionEf.SaveChanges() == 0)
                    throw new Exception("Erro ao excluir aviamento, verifique se este aviamento está vinculado a alguma remessa, por favor.");

                result = true;
            }
            catch (Exception e)
            {
                ConfigHelper.WriteLog(ConfigHelper.PathOutLog("AviamentoRepository" + MethodBase.GetCurrentMethod().Name), $"Falha no repositório. {e.Message} - {e.StackTrace} - {DateTime.Now}");
                throw new Exception("Erro ao acessar informações do banco de dados.");
            }
            return result;
        }
    }
}
