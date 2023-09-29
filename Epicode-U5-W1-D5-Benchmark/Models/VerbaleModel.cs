using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Epicode_U5_W1_D5_Benchmark.Models
{
    public class VerbaleModel
    {
        public int Id { get; set; }
        public DateTime DataViolazione { get; set; }
        public string Indirizzo { get; set; }
        public string Agente { get; set; }
        public DateTime DataTrascrizione { get; set; }
        public double Importo { get; set; }
        public int Decurtamento { get; set; }
        public bool Contestata { get; set; }
        [Display(Name = "Tipologia")]
        public int FkTipologia { get; set; }
        [Display(Name = "Utente")]
        public int FkUtente { get; set; }
    }

    public class FiltriVerbaleModel
    {
        public string Id { get; set; }
        public int PuntiDecurtati { get; set;}
        public double Importo { get; set; }
    }
}