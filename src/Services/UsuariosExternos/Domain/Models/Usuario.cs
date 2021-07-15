using SharedKernel.Models;

namespace Domain.Models
{
    public class Usuario : Entity
    {
        public virtual Documento Documento { get; set; }
    }
}
