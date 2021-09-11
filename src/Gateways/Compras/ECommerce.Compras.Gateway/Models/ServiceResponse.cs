using System.Collections.Generic;

namespace ECommerce.Compras.Gateway.Models
{
    public class ServiceResponse
    {
        public bool IsValid { get; set; } = true;
        public IEnumerable<Error> Errors { get; set; }
    }

    public class Error
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
