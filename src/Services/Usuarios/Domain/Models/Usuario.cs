using Core.Domain.Models;

namespace Domain.Models
{
    public class Usuario : Entity
    {
        public virtual Documento Documento { get; set; }
    }
}
