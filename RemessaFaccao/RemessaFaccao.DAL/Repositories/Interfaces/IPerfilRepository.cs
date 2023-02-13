using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IPerfilRepository
    {
        List<Perfil> GetAll(); 
        Perfil GetById(int id);
        int Count(); 
        int Insert(Perfil perfil); 
        int Update(Perfil perfil);
        int Delete(int id);
    }
}
