using System;
using TechTalk.SpecFlow;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    public class CadastroStepDefinitions
    {
        [When(@"Ele clicar em registrar")]
        public void WhenEleClicarEmRegistrar()
        {
            throw new PendingStepException();
        }

        [When(@"Preecher os dados do formulario")]
        public void WhenPreecherOsDadosDoFormulario(Table table)
        {
            throw new PendingStepException();
        }

        [When(@"Clicar no botão registrar")]
        public void WhenClicarNoBotaoRegistrar()
        {
            throw new PendingStepException();
        }

        [Then(@"Ele receberá uma mensagem de erro que a senha precisa conter uma letra maisucula")]
        public void ThenEleReceberaUmaMensagemDeErroQueASenhaPrecisaConterUmaLetraMaisucula()
        {
            throw new PendingStepException();
        }

        [Then(@"Ele receberá uma mensagem de erro que a senha precisa conter um caractere especial")]
        public void ThenEleReceberaUmaMensagemDeErroQueASenhaPrecisaConterUmCaractereEspecial()
        {
            throw new PendingStepException();
        }
    }
}
