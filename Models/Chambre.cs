using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontReservation.Models
{
    public class Chambre
    {
        public int IdChambre { get; set; }
        public int Prix { get; set; }
        public int NombreDeLiet { get; set; }
        public bool EstLibre { get; set; }
        public int IdHotel { get; set; }

        public Chambre() { }
        public Chambre(int idChambre, int prix, int nombreDeLiet, bool estLibre, int idHotel)
        {
            IdChambre = idChambre;
            Prix = prix;
            NombreDeLiet = nombreDeLiet;
            EstLibre = estLibre;
            IdHotel = idHotel;
        }
    }
}