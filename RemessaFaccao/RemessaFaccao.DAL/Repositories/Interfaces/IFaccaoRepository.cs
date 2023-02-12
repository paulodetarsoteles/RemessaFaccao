using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IFaccaoRepository
    {
        Task<List<Faccao>> GetAll(); 
        Task<Faccao> GetById(int id); 
        Task<int> Count();
        Task<int> Insert(Faccao faccao); 
        Task<int> Update(Faccao faccao); 
        Task<int> Delete(int id);
    }
}
