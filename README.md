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
   
# Importância do Mock

São os objetos criados para testar o comportamentos de outros objetos.

### Framework MOQ

- Realiza Mock com facilidade
- Compatível com visual studio
- Utilizado pelo o time de desenvolvimento do .NET Core e ASP.NET

```md
PM> Install-Package Moq
```
