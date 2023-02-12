using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IRemessaRepository
    {
        Task<List<Remessa>> GetAll();
        Task<List<Remessa>> GetAtrasadas(); 
        Task<List<Remessa>> GetReceberHoje();
        Task<Remessa> GetById(int id);
        Task<int> Count();
        Task<int> CountAtrasadas(); 
        Task<int> CountReceberHoje();
        Task<int> Insert(Remessa remessa);
        Task<int> Update(Remessa remessa);
        Task<int> DeleteById(int id);
    }
}
