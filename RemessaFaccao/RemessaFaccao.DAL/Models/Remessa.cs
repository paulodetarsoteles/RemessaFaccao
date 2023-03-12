using RemessaFaccao.DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace RemessaFaccao.DAL.Models
{
    public class Remessa
    {
        [Key]
        [Display(Name = "Código")]
        public int RemessaId { get; set; }

        [Display(Name = "Código da Facção")]
        public int FaccaoId { get; set; }

        [Display(Name = "Referência")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Referencia { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int Quantidade { get; set; }

        [Display(Name = "Valor Unitário")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public decimal ValorUnitario { get; set; }

        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [Display(Name = "Envio p/ Facção")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataDeEntrega { get; set; } = new(2001, 01, 01);

        [Display(Name = "Dia do Prazo")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataPrazo { get; set; } = DateTime.MaxValue;

        [Display(Name = "Recebimento")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataRecebimento { get; set; } = DateTime.MaxValue; 

        [Display(Name = "Status da Remessa")]
        public StatusRemessa StatusRemessa = StatusRemessa.Preparada;  

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }
    }
}
