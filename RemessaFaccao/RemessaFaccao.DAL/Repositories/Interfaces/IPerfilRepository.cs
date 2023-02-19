using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IPerfilRepository
    {
        List<Perfil> GetAll(); 
        Perfil GetById(int id);
        int Count(); 
        bool Insert(Perfil perfil); 
        bool Update(int id, Perfil perfil);
        bool Delete(int id);
    }
}
