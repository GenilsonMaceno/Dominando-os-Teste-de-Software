Funcionalidade: Usuário - Cadastro
	Como visitante da loja
	Eu desejo me cadastrar como usuário
	Para que eu possa realizar compras na loja


Cenario: Cadastro de usuário com sucesso
Dado Que o visitante está acessando o site da loja
Quando Ele clicar em registrar
E Preecher os dados do formulario
	| Dados                |
	| E-mail               |
	| Confirmação da Senha |
E Clicar no botão registrar
Então Ele será redirecionado para a vitrine
E Uma saudação com seu e-mail será exibida no menu superior

Cenario: Cadastro com senha sem maiusculas
Dado Que o visitante está acessando o site da loja
Quando Ele clicar em registrar
E Preecher os dados do formulario
	| Dados                |
	| E-mail               |
	| Confirmação da Senha |
E Clicar no botão registrar
Então Ele receberá uma mensagem de erro que a senha precisa conter uma letra maisucula

Cenario: Cadastro com senha sem caractere especial
Dado Que o visitante está acessando o site da loja
Quando Ele clicar em registrar
E Preecher os dados do formulario
	| Dados                |
	| E-mail               |
	| Confirmação da Senha |
E Clicar no botão registrar
Então Ele receberá uma mensagem de erro que a senha precisa conter um caractere especial
