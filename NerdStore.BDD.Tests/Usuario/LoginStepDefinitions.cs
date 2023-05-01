using System;
using TechTalk.SpecFlow;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    public class LoginStepDefinitions
    {
        [When(@"Ele clicar em login")]
        public void WhenEleClicarEmLogin()
        {
            throw new PendingStepException();
        }

        [When(@"Preecher os dados do formulario de login")]
        public void WhenPreecherOsDadosDoFormularioDeLogin(Table table)
        {
            throw new PendingStepException();
        }

        [When(@"Clicar no botão login")]
        public void WhenClicarNoBotaoLogin()
        {
            throw new PendingStepException();
        }
    }
}
