using Epicode_U5_W1_D5_Benchmark.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epicode_U5_W1_D5_Benchmark.Controllers
{
    public class AnagraficaController : Controller
    {

        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDb"].ConnectionString.ToString());

        // GET: Anagrafica
        public ActionResult Anagrafiche()
        {
            List<AnagraficaModel> listaAnagrafiche = new List<AnagraficaModel>();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM V_Anagrafica", conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    listaAnagrafiche.Add(new AnagraficaModel()
                    {
                        Id = Int32.Parse(sqlDataReader["IDAnagrafica"].ToString()),
                        Cognome = sqlDataReader["Cognome"].ToString(),
                        Nome = sqlDataReader["Nome"].ToString(),
                        Indirizzo = sqlDataReader["Indirizzo"].ToString(),
                        Citta = sqlDataReader["Citta"].ToString(),
                        CAP = sqlDataReader["CAP"].ToString(),
                        CodFiscale = sqlDataReader["CodFiscale"].ToString(),
                        Punti = Int32.Parse(sqlDataReader["PuntiDecurtati"].ToString()),
                    });
                conn.Close();
            }
            catch { }
            finally {  conn.Close(); }

            return View(listaAnagrafiche);
        }


        public ActionResult CreateAnagrafica()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult CreateAnagrafica(AnagraficaModel anagrafica)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Anagrafica VALUES(@Cognome, @Nome, @Indirizzo, @Citta, @CAP, @CodFiscale)", conn);
                cmd.Parameters.AddWithValue("Cognome", anagrafica.Cognome);
                cmd.Parameters.AddWithValue("Nome", anagrafica.Nome);
                cmd.Parameters.AddWithValue("Indirizzo", anagrafica.Indirizzo);
                cmd.Parameters.AddWithValue("Citta", anagrafica.Citta);
                cmd.Parameters.AddWithValue("CAP", anagrafica.CAP);
                cmd.Parameters.AddWithValue("CodFiscale", anagrafica.CodFiscale);
                cmd.ExecuteNonQuery();

                return RedirectToAction("Anagrafiche");
            }
            catch
            {
                return View();
            }
            finally { conn.Close(); }
        }
    }
}
