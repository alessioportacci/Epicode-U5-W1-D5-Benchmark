using Epicode_U5_W1_D5_Benchmark.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epicode_U5_W1_D5_Benchmark.Controllers
{
    public class VerbaleController : Controller
    {

        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDb"].ConnectionString.ToString());

        // GET: Verbale
        public ActionResult Verbali(string id, bool? punti, bool? importo)
        {
            List<VerbaleModel> listaVerbali = new List<VerbaleModel>();

            string command = "SELECT * FROM Verbale WHERE 1=1";
            if (id != null && id != "")
                command += String.Concat(" AND FKAnagrafica = ", id);
            if (punti.HasValue)
                command += String.Concat(" AND DecurtamentoPunti > ", 10);
            if (importo.HasValue)
                command += String.Concat(" AND Importo > ", 400);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    listaVerbali.Add(new VerbaleModel()
                    {
                        Id = Int32.Parse(sqlDataReader["IDVerbale"].ToString()),
                        DataViolazione = DateTime.Parse(sqlDataReader["DataViolazione"].ToString()),
                        Indirizzo = sqlDataReader["IndirizzoViolazione"].ToString(),
                        Agente = sqlDataReader["NominativoAgente"].ToString(),
                        DataTrascrizione = DateTime.Parse(sqlDataReader["DataTrascrizioneVerbale"].ToString()),
                        Importo = Double.Parse(sqlDataReader["Importo"].ToString()),
                        Decurtamento = Int32.Parse(sqlDataReader["DecurtamentoPunti"].ToString()),
                        Contestata = Boolean.Parse(sqlDataReader["Contestata"].ToString()),
                    });
                conn.Close();
            }
            catch { }
            finally { conn.Close(); }

            return View(listaVerbali);
        }


        public ActionResult CreateVerbale()
        {

            ViewBag.Tipologie = new List<TipoViolazioneModel>();
            ViewBag.Utenti = new List<MiniAnafraficaModel>();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM TipoViolazione", conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    ViewBag.Tipologie.Add(new TipoViolazioneModel()
                    {
                        Value = Int32.Parse(sqlDataReader["IDViolazione"].ToString()),
                        Text = sqlDataReader["Descrizione"].ToString()
                    }); ;


                cmd = new SqlCommand("SELECT IdAnagrafica, Nome, Cognome FROM Anagrafica", conn);
                sqlDataReader.Close();
                sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    ViewBag.Utenti.Add(new MiniAnafraficaModel()
                    {
                        Value = Int32.Parse(sqlDataReader["IDAnagrafica"].ToString()),
                        Text = String.Concat(sqlDataReader["Nome"].ToString(), " ", sqlDataReader["Cognome"].ToString())
                    });
            }
            catch { }
            finally { conn.Close(); }

            return View();
        }

        [HttpPost]
        public ActionResult CreateVerbale(VerbaleModel verbale)
        {

            ViewBag.Tipologie = new List<TipoViolazioneModel>();
            ViewBag.Utenti = new List<MiniAnafraficaModel>();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Verbale VALUES(@Data, @Indirizzo, @Nominativo, @Trascrizione, @Importo, @Decurtamento, @Contestata, @FkTipoViolazione, @FkAnagrafica)", conn);
                cmd.Parameters.AddWithValue("Data", verbale.DataViolazione);
                cmd.Parameters.AddWithValue("Indirizzo", verbale.Indirizzo);
                cmd.Parameters.AddWithValue("Nominativo", verbale.Agente);
                cmd.Parameters.AddWithValue("Trascrizione", verbale.DataTrascrizione);
                cmd.Parameters.AddWithValue("Importo", verbale.Importo);
                cmd.Parameters.AddWithValue("Decurtamento", verbale.Decurtamento);
                cmd.Parameters.AddWithValue("Contestata", verbale.Contestata);
                cmd.Parameters.AddWithValue("FkTipoViolazione", verbale.FkTipologia);
                cmd.Parameters.AddWithValue("FkAnagrafica", verbale.FkUtente);

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT * FROM TipoViolazione", conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    ViewBag.Tipologie.Add(new TipoViolazioneModel()
                    {
                        Value = Int32.Parse(sqlDataReader["IDViolazione"].ToString()),
                        Text = sqlDataReader["Descrizione"].ToString()
                    }); ;


                cmd = new SqlCommand("SELECT IdAnagrafica, Nome, Cognome FROM Anagrafica", conn);
                sqlDataReader.Close();
                sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    ViewBag.Utenti.Add(new MiniAnafraficaModel()
                    {
                        Value = Int32.Parse(sqlDataReader["IDAnagrafica"].ToString()),
                        Text = String.Concat(sqlDataReader["Nome"].ToString(), " ", sqlDataReader["Cognome"].ToString())
                    });


                return RedirectToAction("Verbali");
            }
            catch { return View(); }
            finally { conn.Close(); }
        }
    }
}
