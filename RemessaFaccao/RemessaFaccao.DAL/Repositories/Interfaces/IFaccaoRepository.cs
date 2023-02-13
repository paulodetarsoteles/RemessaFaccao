using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IFaccaoRepository
    {
        List<Faccao> GetAll(); 
        Faccao GetById(int id); 
        int Count();
        int Insert(Faccao faccao); 
        int Update(Faccao faccao); 
        int Delete(int id);
    }
}
