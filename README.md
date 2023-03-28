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
