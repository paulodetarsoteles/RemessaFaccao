using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IRemessaRepository
    {
        List<Remessa> GetAll();
        List<Remessa> GetAtrasadas(); 
        List<Remessa> GetReceberHoje();
        Remessa GetById(int id);
        int Count();
        int CountAtrasadas(); 
        int CountReceberHoje();
        int Insert(Remessa remessa);
        int Update(Remessa remessa);
        int DeleteById(int id);
    }
}
