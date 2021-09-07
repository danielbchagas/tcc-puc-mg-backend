using System.Collections.Generic;

namespace ECommerce.Identidade.Api.Models
{
    public class ClienteResponseMessage
    {
        public ClienteResponseMessage()
        {
            Ok = true;
        }

        public ClienteResponseMessage(bool ok, int status)
        {
            Ok = ok;
            Status = status;
            Errors = new List<string>();
        }

        public ClienteResponseMessage(bool ok, int status, List<string> errors)
        {
            Ok = ok;
            Status = status;
            Errors = errors;
        }

        public bool Ok { get; set; }
        public int Status { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
