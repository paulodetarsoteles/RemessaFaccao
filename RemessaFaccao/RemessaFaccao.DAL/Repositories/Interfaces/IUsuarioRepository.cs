using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> GetAll(); 
        Usuario GetById(int id);
        int Count();
        bool Insert(Usuario usuario); 
        bool Update(int id, Usuario usuario);
        bool Delete(int id);
    }
}
