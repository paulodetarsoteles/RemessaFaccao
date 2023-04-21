using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IAviamentoRepository
    {
        List<Aviamento> GetAll();
        Aviamento GetById(int id);
        int Count();
        bool Insert(Aviamento aviamento);
        bool Update(int id, Aviamento aviamento);
        bool Delete(int id);
    }
}
