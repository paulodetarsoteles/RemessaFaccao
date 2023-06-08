using RemessaFaccao.DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RemessaFaccao.DAL.Models
{
    public class Remessa
    {
        [Key]
        [Display(Name = "Código")]
        public int RemessaId { get; set; }

        [Display(Name = "Código da Facção")]
        [AllowNull]
        public int? FaccaoId { get; set; }

        [Display(Name = "Referência")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Referencia { get; set; }

        public string Piloto { get; set; }

        public string Modelo { get; set; }

        public string Tecido { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Insira um valor inteiro (sem pontos, vírgulas ou espaços)")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Quantidade { get; set; }

        [Display(Name = "Tamanho 1 ano")]
        public int Tamanho1 { get; set; }

        [Display(Name = "Tamanho 2 ano")]
        public int Tamanho2 { get; set; }

        [Display(Name = "Tamanho 4 ano")]
        public int Tamanho4 { get; set; }

        [Display(Name = "Tamanho 6 ano")]
        public int Tamanho6 { get; set; }

        [Display(Name = "Tamanho 8 ano")]
        public int Tamanho8 { get; set; }

        [Display(Name = "Tamanho 10 ano")]
        public int Tamanho10 { get; set; }

        [Display(Name = "Tamanho 12 ano")]
        public int Tamanho12 { get; set; }

        [Display(Name = "Valor Unitário")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public decimal ValorUnitario { get; set; }

        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [Display(Name = "Envio p/ Facção")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataDeEntrega { get; set; } = new(2023, 01, 01);

        [Display(Name = "Dia do Prazo")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataPrazo { get; set; } = new(2023, 01, 01);

        [Display(Name = "Recebimento")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataRecebimento { get; set; } = new(2023, 01, 01);

        [Display(Name = "Status da Remessa")]
        public StatusRemessa StatusRemessa { get; set; } = StatusRemessa.Preparada; 

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [NotMapped]
        [Display(Name = "Facção")]
        public virtual Faccao Faccao { get; set; }

        [NotMapped]
        public virtual List<Aviamento> Aviamentos { get; set; } = new();
    }
}
