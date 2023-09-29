using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Epicode_U5_W1_D5_Benchmark.Models
{
    public class AnagraficaModel
    {
        public int Id { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        public string CodFiscale { get; set; }
        [Display(Name = "Punti decurtati")]
        public int Punti {  get; set; }
    }

    public class MiniAnafraficaModel
    {
        public int Value { get; set;}
        public string Text { get; set;}
    }
}