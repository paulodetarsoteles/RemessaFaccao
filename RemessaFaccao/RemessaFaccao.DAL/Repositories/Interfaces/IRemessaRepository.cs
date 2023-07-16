using RemessaFaccao.DAL.Models;
using RemessaFaccao.DAL.Models.ViewModels;

namespace RemessaFaccao.DAL.Repositories.Interfaces
{
    public interface IRemessaRepository
    {
        List<Remessa> GetAll();
        List<RemessaFaccaoViewModel> GetEmFaseDeCorte();
        List<RemessaFaccaoViewModel> GetNaoEnviadaParaProducao();
        List<RemessaFaccaoViewModel> GetAtrasadas();
        List<RemessaFaccaoViewModel> GetEmProducao();
        List<RemessaFaccaoViewModel> GetReceberHoje();
        List<Remessa> GetRecebidas(DateTime fromDate, DateTime toDate, int? faccaoId);
        Remessa GetById(int id);
        int Count();
        int CountEmFaseDeCorte();
        int CountEnviarParaProducao();
        int CountEmProducao();
        int CountAtrasadas();
        int CountReceberHoje();
        bool Insert(Remessa remessa);
        bool Update(int id, Remessa remessa);
        void UpdateStatus();
        bool Delete(int id);
        List<Faccao> GetFaccoes();
        List<Faccao> GetFaccoesAtivas();
        List<Aviamento> GetAviamentosParaRemessa();
        bool ValidateReferencia(string referencia);
    }
}
