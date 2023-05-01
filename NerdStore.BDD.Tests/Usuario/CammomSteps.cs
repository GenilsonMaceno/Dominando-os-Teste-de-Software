using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    public class CammomSteps
    {
        [Given(@"Que o visitante está acessando o site da loja")]
        public void GivenQueOVisitanteEstaAcessandoOSiteDaLoja()
        {
            throw new PendingStepException();
        }

        [Then(@"Ele será redirecionado para a vitrine")]
        public void ThenEleSeraRedirecionadoParaAVitrine()
        {
            throw new PendingStepException();
        }

        [Then(@"Uma saudação com seu e-mail será exibida no menu superior")]
        public void ThenUmaSaudacaoComSeuE_MailSeraExibidaNoMenuSuperior()
        {
            throw new PendingStepException();
        }
    }
}
