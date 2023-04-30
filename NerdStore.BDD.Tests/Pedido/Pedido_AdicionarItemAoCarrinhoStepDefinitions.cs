using System;
using TechTalk.SpecFlow;

namespace NerdStore.BDD.Tests.Pedido
{
    [Binding]
    public class Pedido_AdicionarItemAoCarrinhoStepDefinitions
    {
        [Given(@"Que um produto esteja na vitrine")]
        public void GivenQueUmProdutoEstejaNaVitrine()
        {
            // Arrange

            // Act

            // Assert
        }

        [Given(@"Esteja disponivel no estoque")]
        public void GivenEstejaDisponivelNoEstoque()
        {
            // Arrange

            // Act

            // Assert
        }

        [Given(@"O usuario esteja logado")]
        public void GivenOUsuarioEstejaLogado()
        {
            // Arrange

            // Act

            // Assert
        }

        [When(@"O usuário adicionar uma unidade ao carrinho")]
        public void WhenOUsuarioAdicionarUmaUnidadeAoCarrinho()
        {
            // Arrange

            // Act

            // Assert
        }

        [Then(@"O usuário será redirecionado ao resumo da compra")]
        public void ThenOUsuarioSeraRedirecionadoAoResumoDaCompra()
        {
            // Arrange

            // Act

            // Assert
        }

        [Then(@"O valor total do pedido será exatamente o valor do item adicionado")]
        public void ThenOValorTotalDoPedidoSeraExatamenteOValorDoItemAdicionado()
        {
            // Arrange

            // Act

            // Assert
        }

        [When(@"O usuário adicionar um item acima da quantidade máxima permitida")]
        public void WhenOUsuarioAdicionarUmItemAcimaDaQuantidadeMaximaPermitida()
        {
            // Arrange

            // Act

            // Assert
        }

        [Then(@"Receberá uma mensagem de erro mencionando que foi ultrapassada a quantidade limite")]
        public void ThenReceberaUmaMensagemDeErroMencionandoQueFoiUltrapassadaAQuantidadeLimite()
        {
            // Arrange

            // Act

            // Assert
        }

        [Given(@"O mesmo produto já tenha sido adicionado ao carrinho anteriormente")]
        public void GivenOMesmoProdutoJaTenhaSidoAdicionadoAoCarrinhoAnteriormente()
        {
            // Arrange

            // Act

            // Assert
        }

        [Then(@"A quantidade de itens daquele produto terá sido acrescida em uma unidade a mais")]
        public void ThenAQuantidadeDeItensDaqueleProdutoTeraSidoAcrescidaEmUmaUnidadeAMais()
        {
            // Arrange

            // Act

            // Assert
        }

        [Then(@"O valor total do pedido será a multiplicação da quantidade de itens pelo valor unitario")]
        public void ThenOValorTotalDoPedidoSeraAMultiplicacaoDaQuantidadeDeItensPeloValorUnitario()
        {
            // Arrange

            // Act

            // Assert
        }

        [When(@"O usuário adicionar a quantidade máxima permitida ao carrinho")]
        public void WhenOUsuarioAdicionarAQuantidadeMaximaPermitidaAoCarrinho()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
