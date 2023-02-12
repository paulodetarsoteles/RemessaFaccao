using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public class FaccaoRepository : IFaccaoRepository
    {
        public Task<List<Faccao>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Faccao> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(Faccao faccao)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Faccao faccao)
        {
            throw new NotImplementedException();
        }
        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
