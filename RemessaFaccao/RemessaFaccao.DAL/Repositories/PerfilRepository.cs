using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public class PerfilRepository : IPerfilRepository
    {
        public Task<List<Perfil>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Perfil> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(Perfil perfil)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Perfil perfil)
        {
            throw new NotImplementedException();
        }
        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
