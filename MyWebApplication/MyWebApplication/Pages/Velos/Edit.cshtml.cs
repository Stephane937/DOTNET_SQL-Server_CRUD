using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyWebApplication.Pages.Velos
{
    public class EditModel : PageModel
    {
        public VeloInfo veloInfo = new VeloInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=cyclestation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM velo where id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                veloInfo.id = "" + reader.GetInt32(0);
                                veloInfo.station = reader.GetString(1);
                                veloInfo.nbreBornettesLibres = "" + reader.GetInt32(2);
                                veloInfo.velosDispo = "" + reader.GetInt32(3);
                                veloInfo.veloManuelle = "" + reader.GetInt32(4);
                                veloInfo.veloElectriques = "" + reader.GetInt32(5);
                                veloInfo.bornePaiement = reader.GetString(6);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            veloInfo.id = Request.Form["id"];
            veloInfo.station = Request.Form["station"];
            veloInfo.nbreBornettesLibres = Request.Form["nbreBornettesLibres"];
            veloInfo.velosDispo = Request.Form["velosDispo"];
            veloInfo.veloManuelle = Request.Form["veloManuelle"];
            veloInfo.veloElectriques = Request.Form["veloElectriques"];
            veloInfo.bornePaiement = Request.Form["bornePaiement"];

            if (veloInfo.id.Length == 0 || veloInfo.station.Length == 0 || veloInfo.velosDispo.Length == 0 || veloInfo.nbreBornettesLibres.Length == 0 || veloInfo.veloManuelle.Length == 0 || veloInfo.veloElectriques.Length == 0 || veloInfo.bornePaiement.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            // enregistrer la nouvelle station de vélo dans la base de données
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=cyclestation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE velo " +
                        "SET station = @station,nbreBornettesLibres = @nbreBornettesLibres,velosDispo = @velosDispo ,veloManuelle = @veloManuelle,veloElectriques = @veloElectriques,bornePaiement = @bornePaiement " +
                        "WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@station", veloInfo.station);
                        command.Parameters.AddWithValue("@nbreBornettesLibres", veloInfo.nbreBornettesLibres);
                        command.Parameters.AddWithValue("@velosDispo", veloInfo.velosDispo);
                        command.Parameters.AddWithValue("@veloManuelle", veloInfo.veloManuelle);
                        command.Parameters.AddWithValue("@veloElectriques", veloInfo.veloElectriques);
                        command.Parameters.AddWithValue("@bornePaiement", veloInfo.bornePaiement);
                        command.Parameters.AddWithValue("@id", veloInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            //successMessage = "Cycle Station Updated Correctly";

            Response.Redirect("/Velos/Index");



        }

    }
}