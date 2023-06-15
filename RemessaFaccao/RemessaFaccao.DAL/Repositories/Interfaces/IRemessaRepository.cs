using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Models.ViewModels;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IRemessaRepository
    {
        List<Remessa> GetAll();
        List<Remessa> GetNaoEnviadaParaProducao();
        List<RemessaFaccaoViewModel> GetAtrasadas();
        List<RemessaFaccaoViewModel> GetEmProducao();
        List<RemessaFaccaoViewModel> GetReceberHoje();
        List<Remessa> GetRecebidas(DateTime fromDate, DateTime toDate, int? faccaoId); 
        Remessa GetById(int id);
        int Count();
        int CountEnviarParaProducao();
        int CountEmProducao();
        int CountAtrasadas();
        int CountReceberHoje();
        bool Insert(Remessa remessa);
        bool Update(int id, Remessa remessa);
        void UpdateStatus();
        bool Delete(int id);
        public List<Faccao> GetFaccoes(); 
        public List<Faccao> GetFaccoesAtivas();
        public List<Aviamento> GetAviamentosParaRemessa();
    }
}
