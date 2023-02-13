using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        List<UsuarioRepository> GetAll(); 
        UsuarioRepository GetById(int id);
        int Count();
        int Insert(Usuario usuario); 
        int Update(Usuario usuario);
        int DeleteById(int id);
    }
}
