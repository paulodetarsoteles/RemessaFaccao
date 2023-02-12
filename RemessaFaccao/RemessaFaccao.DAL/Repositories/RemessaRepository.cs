using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public class RemessaRepository : IRemessaRepository
    {
        public Task<List<Remessa>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Remessa>> GetAtrasadas()
        {
            throw new NotImplementedException();
        }

        public Task<Remessa> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Remessa>> GetReceberHoje()
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAtrasadas()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountReceberHoje()
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(Remessa remessa)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Remessa remessa)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
