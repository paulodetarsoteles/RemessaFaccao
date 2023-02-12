using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public Task<List<UsuarioRepository>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioRepository> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
