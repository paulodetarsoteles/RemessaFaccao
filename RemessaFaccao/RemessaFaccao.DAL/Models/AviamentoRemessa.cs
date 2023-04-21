namespace RemessaFaccao.DAL.Models
{
    public class AviamentoRemessa
    {
        public int AviamentoId { get; set; }
        public int RemessaId { get; set; }
        public Aviamento Aviamento { get; set; }
        public Remessa Remessa { get; set; }
    }
}
