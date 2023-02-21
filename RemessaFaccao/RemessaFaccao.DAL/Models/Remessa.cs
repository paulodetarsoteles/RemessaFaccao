using System.ComponentModel.DataAnnotations;
using static RemessaFaccao.DAL.Models.Enums;

namespace RemessaFaccao.DAL.Models
{
    public class Remessa
    {
        [Display(Name = "Código")]
        public int RemessaId { get; set; }

        [Display(Name = "Código da Facção")]
        public int FaccaoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Referencia { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int Quantidade { get; set; }

        [Display(Name = "Valor Unitário")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public decimal ValorUnitario { get; set; }

        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [Display(Name = "Data de Entrega")]
        public DateTime? DataDeEntrega { get; set; }

        [Display(Name = "Dia do Prazo")]
        public DateTime? DataPrazo { get; set; }

        [Display(Name = "Recebimento")]
        public DateTime? DataRecebimento { get; set; }

        [Display(Name = "Status da Remessa")]
        public StatusRemessa StatusRemessa;  

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }
    }
}
