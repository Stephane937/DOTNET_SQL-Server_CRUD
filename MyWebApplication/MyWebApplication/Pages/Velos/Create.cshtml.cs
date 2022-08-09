using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyWebApplication.Pages.Velos
{
    public class Index1Model : PageModel
    {
        public VeloInfo veloInfo = new VeloInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            veloInfo.station = Request.Form["station"];
            veloInfo.nbreBornettesLibres = Request.Form["nbreBornettesLibres"];
            veloInfo.velosDispo = Request.Form["velosDispo"];
            veloInfo.veloManuelle = Request.Form["veloManuelle"];
            veloInfo.veloElectriques = Request.Form["veloElectriques"];
            veloInfo.bornePaiement = Request.Form["bornePaiement"];

            if(veloInfo.station.Length == 0 || veloInfo.velosDispo.Length == 0 || veloInfo.nbreBornettesLibres.Length == 0 || veloInfo.veloManuelle.Length == 0 || veloInfo.veloElectriques.Length == 0 || veloInfo.bornePaiement.Length == 0)
            {
                errorMessage = "All the fields are required";
                return ;
            }

            // enregistrer la nouvelle station de vélo dans la base de données
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=cyclestation;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO velo " +
                        "(station,nbreBornettesLibres,velosDispo,veloManuelle,veloElectriques,bornePaiement) VALUES" +
                        "(@station,@nbreBornettesLibres,@velosDispo,@veloManuelle,@veloElectriques,@bornePaiement);";

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@station", veloInfo.station);
                        command.Parameters.AddWithValue("@nbreBornettesLibres", veloInfo.nbreBornettesLibres);
                        command.Parameters.AddWithValue("@velosDispo", veloInfo.velosDispo);
                        command.Parameters.AddWithValue("@veloManuelle", veloInfo.veloManuelle);
                        command.Parameters.AddWithValue("@veloElectriques", veloInfo.veloElectriques);
                        command.Parameters.AddWithValue("@bornePaiement", veloInfo.bornePaiement);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            
            veloInfo.station = "";
            veloInfo.nbreBornettesLibres = "";
            veloInfo.velosDispo = "";
            veloInfo.veloManuelle = "";
            veloInfo.veloElectriques = "";
            veloInfo.bornePaiement = "";
            successMessage = "New Cycle Station Added Correctly";

            Response.Redirect("/Velos/Index");



        }

    }
}
