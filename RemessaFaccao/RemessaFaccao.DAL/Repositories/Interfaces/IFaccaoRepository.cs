using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IFaccaoRepository
    {
        List<Faccao> GetAll(); 
        Faccao GetById(int id); 
        int Count();
        bool Insert(Faccao faccao); 
        bool Update(int id, Faccao faccao); 
        bool Delete(int id);
    }
}
