using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace MyWebApplication.Pages.Velos
{
    public class IndexModel : PageModel
    {
        public List<VeloInfo> listVelos = new List<VeloInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=cyclestation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM velo ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VeloInfo veloInfo = new VeloInfo();
                                veloInfo.id = "" + reader.GetInt32(0);
                                veloInfo.station = reader.GetString(1);
                                veloInfo.nbreBornettesLibres = "" + reader.GetInt32(2);
                                veloInfo.velosDispo = "" + reader.GetInt32(3);
                                veloInfo.veloManuelle = "" + reader.GetInt32(4);
                                veloInfo.veloElectriques = "" + reader.GetInt32(5);
                                veloInfo.bornePaiement = reader.GetString(6);

                                listVelos.Add(veloInfo);

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: "+ ex.ToString());
            }
        }
    }

    public class VeloInfo
    {
        public String? id;
        public String? station;
        public String? nbreBornettesLibres;
        public String? velosDispo;
        public String? veloManuelle;
        public String? veloElectriques;
        public String? bornePaiement;

    }
}
