using AutoFixture;
using ECommerce.Customers.Application.Commands.Customer;
using ECommerce.Customers.Application.Handlers.Commands.Customer;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using Moq;

namespace ECommerce.Customers.Application
{
    public class CreateCustomer
    {
        private readonly CreateCustomerCommandHandler _handler;
        private readonly Fixture _fixture = new Fixture();

        public CreateCustomer()
        {
            var repo = new Mock<ICustomerRepository>();
            var uow = new Mock<IUnitOfWork>();

            _handler = new CreateCustomerCommandHandler(repo.Object, uow.Object);
        }

        [Fact]
        public async Task Create_Customer_Should_Be_True()
        {
            // Arrange
            var document = _fixture.Build<CreateDocumentCommand>().Create();
            var email = _fixture.Build<CreateEmailCommand>().Create();
            var phone = _fixture.Build<CreatePhoneCommand>().Create();
            var address = _fixture.Build<CreateAddressCommand>().Create();

            var command = new CreateCustomerCommand(Guid.NewGuid(), string.Empty, string.Empty, document, email, phone, address);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(response.Item1.IsValid);
        }
    }
}