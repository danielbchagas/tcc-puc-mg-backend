using System.Collections.Generic;
using System.Net;

namespace ECommerce.Compras.Gateway.Models.Cliente
{
    public class ClienteResponseMessage
    {
        public ClienteResponseMessage()
        {
            Ok = true;
        }

        public ClienteResponseMessage(bool ok, HttpStatusCode status)
        {
            Ok = ok;
            Status = status;
            Errors = new List<string>();
        }

        public ClienteResponseMessage(bool ok, HttpStatusCode status, List<string> errors)
        {
            Ok = ok;
            Status = status;
            Errors = errors;
        }

        public bool Ok { get; set; }
        public HttpStatusCode Status { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
