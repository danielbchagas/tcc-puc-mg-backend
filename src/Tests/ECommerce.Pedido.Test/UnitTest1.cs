using Bogus;
using ECommerce.Pedido.Domain.Enums;
using ECommerce.Pedido.Domain.Models;
using System.Collections.Generic;
using Xunit;
using PedidoCliente = ECommerce.Pedido.Domain.Models.Pedido;

namespace ECommerce.Pedido
{
    public class UnitTest1
    {
        [Fact]
        public void FecharPedido_ValorDoPedidoDeveSer2500()
        {
            // Arrange
            var documentoFaker = new Faker<Documento>().CustomInstantiator(set => new Documento(set.Random.String()));
            var emailFaker = new Faker<Email>().CustomInstantiator(set => new Email(set.Person.Email));
            var telefoneFaker = new Faker<Telefone>().CustomInstantiator(set => new Telefone(set.Person.Phone));
            var enderecoEndereco = new Faker<Endereco>().CustomInstantiator(set => new Endereco(logradouro: set.Person.Address.Street, bairro: set.Random.String(), cidade: set.Person.Address.City, cep: set.Person.Address.ZipCode, Estados.ES));
            var clienteFaker = new Faker<Cliente>().CustomInstantiator(set => new Cliente(nome: set.Person.FirstName, sobrenome: set.Person.LastName, documento: documentoFaker, email: emailFaker, telefone: telefoneFaker, endereco: enderecoEndereco));
            var pedidoFaker = new Faker<PedidoCliente>().CustomInstantiator(set => new PedidoCliente(StatusPedido.Processando, clienteFaker, new List<Produto>()));

            var produto1Faker = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto2Faker = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto3Faker = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto4Faker = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto5Faker = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));

            var pedido = pedidoFaker.Generate();
            var produto1 = produto1Faker.Generate();
            var produto2 = produto2Faker.Generate();
            var produto3 = produto3Faker.Generate();
            var produto4 = produto4Faker.Generate();
            var produto5 = produto5Faker.Generate();

            pedido.Produtos.Add(produto1);
            pedido.Produtos.Add(produto2);
            pedido.Produtos.Add(produto3);
            pedido.Produtos.Add(produto4);
            pedido.Produtos.Add(produto5);

            // Act
            pedido.CalcularTotalPedido();

            // Assert
            Assert.True(pedido.Valor == 2500);
        }

        [Fact]
        public void FecharPedido_ValorDoPedidoDeveSer3000()
        {
            // Arrange
            var documentoFaker = new Faker<Documento>().CustomInstantiator(set => new Documento(set.Random.String()));
            var emailFaker = new Faker<Email>().CustomInstantiator(set => new Email(set.Person.Email));
            var telefoneFaker = new Faker<Telefone>().CustomInstantiator(set => new Telefone(set.Person.Phone));
            var enderecoEndereco = new Faker<Endereco>().CustomInstantiator(set => new Endereco(logradouro: set.Person.Address.Street, bairro: set.Random.String(), cidade: set.Person.Address.City, cep: set.Person.Address.ZipCode, Estados.ES));
            var clienteFaker = new Faker<Cliente>().CustomInstantiator(set => new Cliente(nome: set.Person.FirstName, sobrenome: set.Person.LastName, documento: documentoFaker, email: emailFaker, telefone: telefoneFaker, endereco: enderecoEndereco));
            var pedidoFaker = new Faker<PedidoCliente>().CustomInstantiator(set => new PedidoCliente(StatusPedido.Processando, clienteFaker, new List<Produto>()));

            var produto1Faker = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto2Faker = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto3Faker = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto4Faker = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto5Faker = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto6Faker = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));

            var pedido = pedidoFaker.Generate();
            var produto1 = produto1Faker.Generate();
            var produto2 = produto2Faker.Generate();
            var produto3 = produto3Faker.Generate();
            var produto4 = produto4Faker.Generate();
            var produto5 = produto5Faker.Generate();
            var produto6 = produto6Faker.Generate();

            // Act
            pedido.Produtos.Add(produto1);
            pedido.Produtos.Add(produto2);
            pedido.Produtos.Add(produto3);
            pedido.Produtos.Add(produto4);
            pedido.Produtos.Add(produto5);
            pedido.Produtos.Add(produto6);
            pedido.CalcularTotalPedido();

            // Assert
            Assert.True(pedido.Valor == 3000);
        }
    }
}
