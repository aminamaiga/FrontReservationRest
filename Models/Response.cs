using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontReservation.Models
{
    public class Response
    {
        public String Message { get; set; }
        public Hotel[] Responses { get; set; }

        public Response() { }
    }
}