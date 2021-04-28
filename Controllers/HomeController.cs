using FrontReservation.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

using System.Text;

namespace FrontReservation.Controllers
{
    public class HomeController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44341/api/hotel/";
        string search = "";
        ReserverRequest reservationReq = new ReserverRequest();

        public async Task<ActionResult> Index(String ville = "", String nomHotel = "", String categorie = "", String prixMin = "",
            String prixMax = "", String nombrePersonne = "", DateTime? dateDepart = null, DateTime? dateArrivee = null)
        {
            if (!string.IsNullOrEmpty(ville))
            {
                search += "?ville=" + ville;
            } 
            if (!string.IsNullOrEmpty(nomHotel))
            {
                search += "&nomHotel=" + nomHotel;
            } 
            if (!string.IsNullOrEmpty(nombrePersonne))
            {
                search += "&nombrePersonne=" + nombrePersonne;
            } 
            if (!string.IsNullOrEmpty(categorie))
            {
                search += "&categorie=" + categorie;
            }
            if (dateDepart != null)
            {
                search += "&dateDepart=" + dateDepart + "&dateArrivee=" + dateArrivee;

            }
            if (!string.IsNullOrEmpty(prixMin))
            {
                search += "&prixMin=" + prixMin + "&prixMax=" + prixMax;
            }
            Response hotelInfo = new Response();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("search" + search);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    hotelInfo = JsonConvert.DeserializeObject<Response>(EmpResponse);
                    

                }
                return View(hotelInfo);
            }
        }

        public ViewResult Reserver(String IdHotel = "", String Ville = "")
        {
            if (!string.IsNullOrEmpty(IdHotel))
            {
                reservationReq.IdHotel = int.Parse(IdHotel);
            }
         
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Reserver(ReserverRequest reservation)
        {
            reservation.IdHotel = reservationReq.IdHotel;
            ReserverRequest receivedReservation = new ReserverRequest();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");

                Console.WriteLine(content);
                using (var response = await httpClient.PostAsync(Baseurl + "reserver", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedReservation = JsonConvert.DeserializeObject<ReserverRequest>(apiResponse);
                }
            }
            return View(reservation);
        }
    }

}


