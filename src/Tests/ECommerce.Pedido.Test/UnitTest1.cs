using Bogus;
using ECommerce.Pedido.Domain.Enums;
using ECommerce.Pedido.Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;
using PedidoCliente = ECommerce.Pedido.Domain.Models.Pedido;

namespace ECommerce.Pedido
{
    public class UnitTest1
    {
        private PedidoCliente Pedido { get; }

        public UnitTest1()
        {
            var documento = new Faker<Documento>().CustomInstantiator(set => new Documento(set.Random.String()));

            var email = new Faker<Email>().CustomInstantiator(set => new Email(set.Person.Email));

            var telefone = new Faker<Telefone>().CustomInstantiator(set => new Telefone(set.Person.Phone));

            var endereco = new Faker<Endereco>().CustomInstantiator(set => new Endereco(logradouro: set.Person.Address.Street, bairro: set.Random.String(), cidade: set.Person.Address.City, cep: set.Person.Address.ZipCode, Estados.ES));

            var cliente = new Faker<Cliente>().CustomInstantiator(set => new Cliente(nome: set.Person.FirstName, sobrenome: set.Person.LastName, documento: documento, email: email, telefone: telefone, endereco: endereco));

            var produto1 = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto2 = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto3 = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto4 = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));
            var produto5 = new Faker<Produto>().CustomInstantiator(set => new Produto(nome: set.Random.String(), 5, 100, imagem: set.Image.PicsumUrl()));

            var faker = new Faker<PedidoCliente>()
                .CustomInstantiator(set => new PedidoCliente(StatusPedido.Processando, 
                cliente,
                new List<Produto> 
                {
                    produto1, produto2, produto3, produto4, produto5
                }));

            Pedido = faker.Generate();
        }

        [Fact]
        public void Test1()
        {
            // Arrange

            // Act
            Pedido.CalcularTotalPedido();

            // Assert
            Assert.True(Pedido.Valor == 2500);
        }
    }
}
