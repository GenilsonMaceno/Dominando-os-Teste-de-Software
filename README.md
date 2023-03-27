# Dominando os Teste de Software

> [!NOTE]
> Regra 10 de myers: Grafico para ter mais agumentos sobre testes

## Tipos de testes mais comuns

- Teste de unidades
- Teste de integração
- Teste de automatização
- Teste de carga

## Teste de unidade

### Frameworks para teste de unidadades

- MsTest
- Nunit
- [XUnit](https://xunit.net/)

## Instalações necessárias

```md
- install-package xunit
- install-package unit.runner.visualstudio
```

## Padrões e Nomeclaturas

- Arrange: Gerar objetos, instancias para atuação.
- Act: Para fazer a chamada do método.
- Assert: Validar o resultados gerados através do método é valido.

## Nomeclatura de teste de Unidades

### Exemplo de nomeclatura de teste

1. ObjetosEmteste_MetodoComportamentoEmTeste_ComportamentoEsperado
   - Pedido_AdicionarPedidoItem_DeveIncrementarUnidadesSeltemJaExistente
   - Estoque_RetirarItem_DeveEnviarEmailSeAbaixoDe10Unidades

2. MetodoEmTeste_EstadoEmTeste_ComportamentoEsperado
   - AdicionarPedidoItem_ItemExistenteCaarrinho_DeveIncrementarUnidadesDoItem
   - RetirarEstoque_EstoqueAbaixoDe10Unidades_DeveEnviarEmailDeAviso

## Importância do Mock

São os objetos criados para testar o comportamentos de outros objetos.

### Framework MOQ

- Realiza Mock com facilidade
- Compatível com visual studio
- Utilizado pelo o time de desenvolvimento do .NET Core e ASP.NET

```md
PM> Install-Package Moq
```

### O Básico sobre testes de unidade

- É necessário o pacote **xunit** e o **xunit.runner.visualstudio** para reconhecer os teste no VS

    A Importância de usar a função **Equal**:

```dotnetcli
namespace Demo.Tests
{
    public class CalculadoraTests
    {
        [Fact]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            //Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(2, 2);

            // Assert
            Assert.Equal(4, resultado);
        }

        [Theory]
        [InlineData(1,1,2)]
        [InlineData(2, 2, 4)]
        [InlineData(4, 4, 8)]
        [InlineData(8, 8, 16)]
        [InlineData(9, 9, 18)]
        public void Calculadora_Somar_RetornarValoresSomaCorretos(double v1, double v2, double total)
        {
            //Arrange
            var calculadora = new Calculadora()
            // Act
            var resultado = calculadora.Somar(v1, v2)
            // Assert
            Assert.Equal(total, resultado);
        }
    }
}
```

> [!NOTE]
> **Equal** compara o valor do **resultado** com o valor esperado. Em caso de erro informa na tela o valor retornado em resultado

**Importante:**

> [!NOTE]
> *[Fact]:* Realiza testes únicos

> [!NOTE]
> *[Theory]:* Realiza uma sequencia de testes

### Asserções

```dotnetcli
public class AssertingObjectTypesTests
    {
        [Fact]
        public void FuncionarioFactory_Criar_DeveRetornarTipoFuncionario()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            // Assert
            Assert.IsType<Funcionario>(funcionario);
        }
        
        [Fact]
        public void FuncionarioFactory_Criar_DeveRetornarTipoDerivadoPessoa()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            // Assert
            Assert.IsAssignableFrom<Pessoa>(funcionario);
        }
    }
```

- **Assert.IsType<Funcionario>(funcionario):** Válida se o tipo **funcionario** é da class **Funcionario**
- **Assert.IsAssignableFrom<Pessoa>(funcionario):** Válida se o tipo **funcionario** herda da classe **Pessoa**

```dotnetcli
[Fact]
        public void Funcionario_Habilidades_NaoDevePossuirHabilidadesVazias()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            // Assert
            Assert.All(funcionario.Habilidades, habilidade => Assert.False(string.IsNullOrWhiteSpace(habilidade)));
        }
```

- **Assert.All:** Válida todo item da coleção, portanto na lógica usada, está verificando se os valores da coleção é nullo ou contem espaço,atravês do outro **Assert** que válida a operação esperando o retorno falso.

### Trait

 Com **trait** eu posso "categorizar" e renomear o nome do teste, exemplo de código:

```dotnetcli
        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria","Cliente Trait Testes")] // renomeado o teste de "Cliente_NovoCliente_DeveEstarValido" para "Categoria [Cliente Trait Testes]"
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Eduardo",
                "Pires",
                DateTime.Now.AddYears(-30),
                "edu@edu.com",
                true,
                DateTime.Now);

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "edu2edu.com",
                true,
                DateTime.Now);

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        }
    }
```

Veja como fica na imagem:

:::image type="content" source="traits.png" alt-text="Visualização do uso do trait":::

   Também é possível "Renomear" o nome do método como foi feito usando **Fact**, ao invés de ficar como nome padrão gerado,
utilizei outro meio de ficar mais identificavel, exemplo:

:::image type="content" source="Fact.png" alt-text="Uso do Fact para organizar melhor a forma de visualizacao do teste atraves do método":::