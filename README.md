# Dominando os Teste de Software

**Sumário:**

- [Teste de unidade](https://github.com/GenilsonMaceno/Dominando-os-Teste-de-Software/blob/main/README.md#teste-de-unidade)  
   - [Frameworks para teste de unidadades](https://github.com/GenilsonMaceno/Dominando-os-Teste-de-Software/blob/main/README.md#frameworks-para-teste-de-unidadades)
   - [Instalações necessárias](https://github.com/GenilsonMaceno/Dominando-os-Teste-de-Software/blob/main/README.md#instala%C3%A7%C3%B5es-necess%C3%A1rias)
   - [Padrões e Nomeclaturas](https://github.com/GenilsonMaceno/Dominando-os-Teste-de-Software/blob/main/README.md#padr%C3%B5es-e-nomeclaturas)

---
> Regra 10 de myers: Grafico para ter mais agumentos sobre testes

<details open>
<summary><strong>Tipos de testes mais comuns</strong></summary>
<br>

| Rank | Tipos de testes |
|:-----|:---------------:|
|     1|Teste de unidades|
|     2|Teste de integração|
|     3|Teste de automatização|
|     4|Teste de carga|

</details>

## Teste de unidade

### Frameworks para teste de unidadades

- MsTest
- Nunit
- [XUnit](https://xunit.net/)

### Instalações necessárias

```md
- install-package xunit
- install-package unit.runner.visualstudio
```

### Padrões e Nomeclaturas

- Arrange: Gerar objetos, instancias para atuação.
- Act: Para fazer a chamada do método.
- Assert: Validar o resultados gerados através do método é valido.

### Nomeclatura de teste de Unidades

Exemplo de nomeclatura de teste:

1. ObjetosEmteste_MetodoComportamentoEmTeste_ComportamentoEsperado
   - Pedido_AdicionarPedidoItem_DeveIncrementarUnidadesSeltemJaExistente
   - Estoque_RetirarItem_DeveEnviarEmailSeAbaixoDe10Unidades

2. MetodoEmTeste_EstadoEmTeste_ComportamentoEsperado
   - AdicionarPedidoItem_ItemExistenteCaarrinho_DeveIncrementarUnidadesDoItem
   - RetirarEstoque_EstoqueAbaixoDe10Unidades_DeveEnviarEmailDeAviso

### Importância do Mock

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
> **Equal** compara o valor do **resultado** com o valor esperado. Em caso de erro informa na tela o valor retornado em resultado

**Importante:**

> *[Fact]:* Realiza testes únicos

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

### Fixtures

É um meio de **compartilhar** a mesma instancia entre outras classes, ou seja reutiliza-la para demais classes de testes. Sem necessáriamente recriar a instância a cada execução de testes, posso criar apenas uma vez e reutilizar, como no exemplo abaixo. É implementado uma **ICollectionFixture**  com a minha classe **ClienteTestsFixture** e criado **ClienteCollection** herdando da interface.

```dotnetcli
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
    {}

    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Eduardo",
                "Pires",
                DateTime.Now.AddYears(-30),
                "edu@edu.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public Cliente GerarClienteInValido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "edu2edu.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public void Dispose()
        {
        }
    }
```

Depois eu apenas implemento com a injeção de idependencia nos meus outros testes que iram reutilizar a classe que será testada, como no exemplo da **ClienteTesteValido:**

```dotnetcli
    [Collection(nameof(ClienteCollection))] // lembrar de sempre referencia que é uma collection fixture
    public class ClienteTesteValido
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteTesteValido(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }
        

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Fixture Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }
    }
```

Class: **ClienteTesteInvalido**

```dotnetcli
[Collection(nameof(ClienteCollection))]
    public class ClienteTesteInvalido
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteTesteInvalido(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Fixture Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInValido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        }
    }
```

### Ordernação de testes

Consigo ordernar qual teste vai ser executado primeiro atraves de um dos **simpson** da próprioa microsoft, exemplo:

*Simpson priority:*

```dotnetcli
[AttributeUsage(AttributeTargets.Method)]
    public class TestPriorityAttribute : Attribute
    {
        public TestPriorityAttribute(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; }
    }

    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

            foreach (var testCase in testCases)
            {
                var priority = 0;

                foreach (var attr in testCase.TestMethod.Method.GetCustomAttributes((typeof(TestPriorityAttribute).AssemblyQualifiedName)))
                    priority = attr.GetNamedArgument<int>("Priority");

                GetOrCreate(sortedMethods, priority).Add(testCase);
            }

            foreach (var list in sortedMethods.Keys.Select(priority => sortedMethods[priority]))
            {
                list.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
                foreach (var testCase in list)
                    yield return testCase;
            }
        }

        private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            if (dictionary.TryGetValue(key, out var result)) return result;

            result = new TValue();
            dictionary[key] = result;

            return result;
        }
    }
```

***Exemplo de uso:***

```dotnetcli
[TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    public class OrdemTestes
    {
        public static bool Teste1Chamado;
        public static bool Teste2Chamado;
        public static bool Teste3Chamado;
        public static bool Teste4Chamado;

        [Fact(DisplayName = "Teste 04"), TestPriority(3)]
        [Trait("Categoria", "Ordenacao Testes")]
        public void Teste04()
        {
            Teste4Chamado = true;

            Assert.True(Teste3Chamado);
            Assert.True(Teste1Chamado);
            Assert.False(Teste2Chamado);
        }

        [Fact(DisplayName = "Teste 01"), TestPriority(2)]
        [Trait("Categoria", "Ordenacao Testes")]
        public void Teste01()
        {
            Teste1Chamado = true;

            Assert.True(Teste3Chamado);
            Assert.False(Teste4Chamado);
            Assert.False(Teste2Chamado);
        }

        [Fact(DisplayName = "Teste 03"), TestPriority(1)]
        [Trait("Categoria", "Ordenacao Testes")]
        public void Teste03()
        {
            Teste3Chamado = true;

            Assert.False(Teste1Chamado);
            Assert.False(Teste2Chamado);
            Assert.False(Teste4Chamado);
        }

        [Fact(DisplayName = "Teste 02"), TestPriority(4)]
        [Trait("Categoria", "Ordenacao Testes")]
        public void Teste02()
        {
            Teste2Chamado = true;

            Assert.True(Teste3Chamado);
            Assert.True(Teste4Chamado);
            Assert.True(Teste1Chamado);
        }
    }
```

### Gerando dados humanos

- Gerador de dados fakes (Resumidamente)

```md
PM> Install-Package Bogus
```

mais informações sobre esse framework [aqui](https://github.com/bchavez/bogus)

### Realizando mock de objetos

Simula um objeto fake para que posso realizar o teste, exemplo:

```dotnetcli
        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            clienteRepo.Verify(r => r.Adicionar(cliente),Times.Once);
            mediatr.Verify(m=>m.Publish(It.IsAny<INotification>(),CancellationToken.None),Times.Once);
        }
```

No código Acima, podemos ver a simulação de implementações de objetos, no caso o **IClienteRepository** e o **IMediator**.
Posso implementar também a classe ao inves da interface, mas para que fique um cenário mais próximo da realidade, foi escolhido as interfaces.

Com o **Mock** podemos usar "métodos" do próprio framework para validar nossos dados ao invés de usar o "Assert", como é o caso do códigos abaixo que também foi mostrado acima:

`clienteRepo.Verify(r => r.Adicionar(cliente),Times.Once);`
`mediatr.Verify(m=>m.Publish(It.IsAny<INotification>(),CancellationToken.None),Times.Once);`

No caso de ***clienteRepo*** e o ***mediatr*** estou verificando se foi executado pelo menos um vez na entrada do segundo parâmetro, usando o **Times.Once**.

### AutoMock

Instalação necessária para o **AutoMock**

```md
PM> Install-Package MOQ.automoc
```

O Auto Mock cria uma instancia do seu service já realizando o mock das interface implementadas, exemplo:

```dotnetcli
        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente),Times.Once);
            mocker.GetMock<IMediator>().Verify(m=>m.Publish(It.IsAny<INotification>(),CancellationToken.None),Times.Once);
        }
```

Acima podemos ver que foi instanciado o `new AutoMocker()` em seguida, criamos uma instancia de **ClienteServices** usando o `mocker.CreateInstance<ClienteService>()` que realizou o mock também do **IClienteRepository** e do **IMediator**.

Diferente do caso abaixo, onde tivemos que realizar o mock de **IClienteRepository** e do **IMediator** separadamente.

```dotnetcli
        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            clienteRepo.Verify(r => r.Adicionar(cliente),Times.Once);
            mediatr.Verify(m=>m.Publish(It.IsAny<INotification>(),CancellationToken.None),Times.Once);
        }
```

### Utilizando Fluent Assertions

- Mais informações sobre Fluent Assertions [clicando aqui](https://fluentassertions.com/introduction)
- Repositório no [GitHub](https://github.com/fluentassertions/fluentassertions)

Com o **Fluent Assertion** consigo de deixar meus assert mais expressivos, exemplo:

```dotnetcli
        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            //Assert.True(cliente.EhValido());

            // Assert
            cliente.EhValido().Should().BeTrue();

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente),Times.Once);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m=>m.Publish(It.IsAny<INotification>(),CancellationToken.None),Times.Once);
        }
```

Como pode ver no código acima, em **Assert**, foi retirado o código e foi expressa de uma forma diferente de forma que expresse o mesmo resultado esperado:

- Antes:  `Assert.True(cliente.EhValido());`
- Depois:  `cliente.EhValido().Should().BeTrue();`

Ambos estão realizando a mesma ação, *esperando que o cliente seja válido retornando "true"*, porém com Assert Fluent expressando isso de uma forma melhor.

### Mensagens de saída nos testes

- Posso pular um teste ao inves de ajusta-lo, usando o parâmetro `Skip`, assim como posso pular e informar o motivo deixando junto no parâmetro a mensagem, exemplo:

```dotnetcli
    public class TesteNaoPassandoMotivoEspecifico
    {
        [Fact(DisplayName = "Novo Cliente 2.0", Skip = "Nova versão 2.0 quebrando")]
        [Trait("Categoria", "Escapando dos Testes")]
        public void Teste_NaoEstaPassando_VersaoNovaNaoCompativel()
        {
            Assert.True(false);
        }
    }
```

Assim como **Skip** gerar mensagem de *output*, também posso implementar mensagens de saídas (output) para os meus teste executados com sucesso, ao implementar o **ITestOutputHelper**, exemplo:

- Exemplo de implementação da inteface **ITestOutputHelper**

```dotnetcli
        private readonly ClienteTestsAutoMockerFixture _clienteTestsFixture;
        readonly ITestOutputHelper _outputHelper;

        public ClienteFluentAssertionsTests(ClienteTestsAutoMockerFixture clienteTestsFixture, 
                                            ITestOutputHelper outputHelper)
        {
            _clienteTestsFixture = clienteTestsFixture;
            _outputHelper = outputHelper;
        }
```

- usando **ITestOutputHelper** para retornar uma mensagem de saída, exemplo:

```dotnetcli
        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            //Assert.False(result);
            //Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);

            // Assert 
            result.Should().BeFalse();
            cliente.ValidationResult.Errors.Should().HaveCountGreaterOrEqualTo(1, "deve possuir erros de validação");

            _outputHelper.WriteLine($"Foram encontrados {cliente.ValidationResult.Errors.Count} erros nesta validação");
        }
```

### Playlist de testes

- Através do visual studio eu consigo adicionar uma playlist de teste conforme os meus critérios, exemplo, teste que tem menos duração ou somente adicionado nessa playlist teste que são relacionado a determinado serviço.
- Para adicionar, selecione os testes que deseja adicionar a lista e click com o botão direito do mouse, depois click **Add To Playlist**.

### Rodando os teste via linha de comando

- Instalação necessária:

```md
PM> Install-Package xunit.runner.console
```

Com isso consigo rodar os teste atravês da **dll dos testes**, usando o comando `dotnet vsteste`, no cenário atual projeto ficaria assim o commando `dotnet vsteste Features.Testes.dll`

Mais informações nas documentações de teste através da linha de comando: [dotnet vstest](https://learn.microsoft.com/dotnet/core/tools/dotnet-vstest)

### Analisando a cobertura de código dos testes

Ferrameta grátis para usar para cobertura de código dos teste: [OpenConver](https://github.com/OpenCover/opencover)

## TDD - Teste Driven Development

**Introdução:** É um pratica de testar um código que não existe

- Ciclo de vida do TDD
  - Teste passes
  - Refactor
  - testFails

- As Três leis do TDD
  - Você **não pode escrever nenhum código até ter escrito um teste** que detecte uma possível falha.
  - Você **não pode escrever mais testes de unidade do que o suficiente** para detectar a falha - não compilar é não ter efeito.
  - Você **não pode escrever mais código do que o suficiente** para passar nos testes.

- Livros que falam sobre o teste de TDD
  - Test-Driven Development - **Mauricio Aniche**
  - Test-Driven Development - **Kent Beck**
  - Growing Object-riented Software, Guided By Tests
  - Pratical Test-Driven Development using C# 7
  
Na primeira vez aplicando o TDD, o primeiro teste é o que tem mais linha de código, pois é o mesmo que vai criar basicamente a estrutura inicial. Depois fica mais fácil aplicação dos demais teste.

### Pedido Adicionar

*Exemplo:*

No código abaixo foi 1º teste criado, seguindo o roteiro que contém as informações que precisamos criar, a medida que foi surgindo a necessidade de desenvolver para ter o resultado do teste, como por exemplo: `Assert.Equal(200,pedido.ValorTotal)` foi surgindo as classes necessária para isso, como a classe Pedido, PedidoItem, o método AdicionarItem e o retorno do **valor total** atravês da classe.

```dotnetcli
        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(),"Produto Teste",2,100);

            // Act
            pedido.AdicionarItem(pedidoItem);

            // Assert
            Assert.Equal(200,pedido.ValorTotal);
        }
```

### Pedido - Aplicar voucher

instalação necessária:

```md
PM> Install-Package fluentvalidation
```

Exemplo de uso de fluentvalidation, a partir de uma class valida chamada Voucher.

```dotnetcli
    public class VoucherAplicavelValidation : AbstractValidator<Voucher>
    {
        public static string CodigoErroMsg => "Voucher sem código válido.";
        public static string DataValidadeErroMsg => "Este voucher está expirado.";
        public static string AtivoErroMsg => "Este voucher não é mais válido.";
        public static string UtilizadoErroMsg => "Este voucher já foi utilizado.";
        public static string QuantidadeErroMsg => "Este voucher não está mais disponível";
        public static string ValorDescontoErroMsg => "O valor do desconto precisa ser superior a 0";
        public static string PercentualDescontoErroMsg => "O valor da porcentagem de desconto precisa ser superior a 0";

        public VoucherAplicavelValidation()
        {
            RuleFor(c => c.Codigo)
                .NotEmpty()
                .WithMessage(CodigoErroMsg);

            RuleFor(c => c.DataValidade)
                .Must(DataVencimentoSuperiorAtual)
                .WithMessage(DataValidadeErroMsg);

            RuleFor(c => c.Ativo)
                .Equal(true)
                .WithMessage(AtivoErroMsg);

            RuleFor(c => c.Utilizado)
                .Equal(false)
                .WithMessage(UtilizadoErroMsg);

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage(QuantidadeErroMsg);

            When(f => f.TipoDescontoVoucher == TipoDescontoVoucher.Valor, () =>
            {
                RuleFor(f => f.ValorDesconto)
                    .NotNull()
                    .WithMessage(ValorDescontoErroMsg)
                    .GreaterThan(0)
                    .WithMessage(ValorDescontoErroMsg);
            });

            When(f => f.TipoDescontoVoucher == TipoDescontoVoucher.Porcentagem, () =>
            {
                RuleFor(f => f.PercentualDesconto)
                    .NotNull()
                    .WithMessage(PercentualDescontoErroMsg)
                    .GreaterThan(0)
                    .WithMessage(PercentualDescontoErroMsg);
            });
        }
    }
```

Exemplo de validação de teste usando o fluent validation com erro:

```dotnetcli
     [Fact(DisplayName = "Validar Voucher Tipo Valor Inválido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarInvalido()
        {
            // Arrange
            var voucher = new Voucher("", null, null, 0,
                TipoDescontoVoucher.Valor, DateTime.Now.AddDays(-1), false, true);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherAplicavelValidation.AtivoErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.CodigoErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.DataValidadeErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.QuantidadeErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.UtilizadoErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.ValorDescontoErroMsg, result.Errors.Select(c => c.ErrorMessage));
        }
```
## Teste de integração
