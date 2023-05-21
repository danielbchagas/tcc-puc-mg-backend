using FluentValidation;

namespace ECommerce.Customer.Api.Services.gRPC.Validators
{
    public class CustomerGrpcValidator : AbstractValidator<Customers.Api.Protos.CreateUserRequest>
    {
        public CustomerGrpcValidator()
        {
            RuleFor(u => u.Id).NotEmpty().NotNull();
            RuleFor(u => u.Firstname).NotEmpty().NotNull();
            RuleFor(u => u.Lastname).NotEmpty().NotNull();
            RuleFor(u => u.Document).NotNull();
            RuleFor(u => u.Email).NotNull();
            RuleFor(u => u.Phone).NotNull();
        }
    }
}
