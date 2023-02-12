using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IPerfilRepository
    {
        Task<List<Perfil>> GetAll(); 
        Task<Perfil> GetById(int id);
        Task<int> Count(); 
        Task<int> Insert(Perfil perfil); 
        Task<int> Update(Perfil perfil);
        Task<int> Delete(int id);
    }
}
