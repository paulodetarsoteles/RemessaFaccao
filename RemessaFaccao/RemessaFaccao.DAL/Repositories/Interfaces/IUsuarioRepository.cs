using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<UsuarioRepository>> GetAll(); 
        Task<UsuarioRepository> GetById(int id);
        Task<int> Count();
        Task<int> Insert(Usuario usuario); 
        Task<int> Update(Usuario usuario);
        Task<int> DeleteById(int id);
    }
}
