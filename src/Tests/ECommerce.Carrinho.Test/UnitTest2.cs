﻿using Bogus;
using ECommerce.Carrinho.Domain.Models;
using System;
using Xunit;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.Carrinho;

namespace ECommerce.Carrinho.Test
{
    public class UnitTest2
    {
        private CarrinhoCliente Carrinho { get; }

        public UnitTest2()
        {
            var faker = new Faker<CarrinhoCliente>()
                .CustomInstantiator(set => new CarrinhoCliente(clienteId: Guid.NewGuid()));

            Carrinho = faker.Generate();
        }

        [Fact]
        public void AdicionarItemCarrinho_ValorDoCarrinhoDeveSer1000()
        {
            // Arrange
            var faker = new Faker<ItemCarrinho>()
                .CustomInstantiator(set => new ItemCarrinho(nome: set.Random.String(), quantidade: 5, valor: 200, imagem: set.Image.PicsumUrl(), produtoId: Guid.NewGuid()));

            var item = faker.Generate();

            // Act
            Carrinho.AtualizarItensCarrinho(item);

            // Assert
            Assert.Equal(1000, Carrinho.Valor);
        }

        [Fact]
        public void DiminuirQuantidadeItemCarrinho_QuantidadeDeItensCarrinhoDeveSer3()
        {
            // Arrange
            var produtoId = Guid.NewGuid();

            var faker = new Faker<ItemCarrinho>()
                .CustomInstantiator(set => new ItemCarrinho(nome: set.Random.String(), quantidade: 5, valor: 200, imagem: set.Image.PicsumUrl(), produtoId: Guid.NewGuid()));

            var primeiroItem = faker.Generate();

            var faker2 = new Faker<ItemCarrinho>()
                .CustomInstantiator(set => new ItemCarrinho(nome: primeiroItem.Nome, quantidade: 3, valor: primeiroItem.Valor, imagem: primeiroItem.Imagem, produtoId: primeiroItem.ProdutoId));

            var segundoItem = faker2.Generate();

            // Act
            Carrinho.AtualizarItensCarrinho(primeiroItem);
            Carrinho.AtualizarItensCarrinho(segundoItem);

            // Assert
            Assert.Contains(Carrinho.Itens, ic => ic.Quantidade == 3);
        }

        [Fact]
        public void AumentarQuantidadeItensCarrinho_DeveRetornarErro()
        {
            // Arrange
            var produtoId = Guid.NewGuid();

            var faker = new Faker<ItemCarrinho>()
                .CustomInstantiator(set => new ItemCarrinho(nome: set.Random.String(), quantidade: 5, valor: 200, imagem: set.Image.PicsumUrl(), produtoId: Guid.NewGuid()));

            var primeiraInteracao = faker.Generate();

            var faker2 = new Faker<ItemCarrinho>()
                .CustomInstantiator(set => new ItemCarrinho(nome: primeiraInteracao.Nome, quantidade: 6, valor: primeiraInteracao.Valor, imagem: primeiraInteracao.Imagem, produtoId: primeiraInteracao.ProdutoId));

            var segundaInteracao = faker2.Generate();

            // Act
            var validationResult = Carrinho.AtualizarItensCarrinho(primeiraInteracao);
            validationResult.Errors.AddRange(Carrinho.AtualizarItensCarrinho(segundaInteracao).Errors);

            // Assert
            Assert.Contains(validationResult.Errors, ic => ic.ErrorMessage.Contains($"A quantidade mínima do {primeiraInteracao.Nome} é 1 e o máxima do {primeiraInteracao.Nome} é 5."));
        }
    }
}