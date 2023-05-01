Funcionalidade: Usuário - Login
	Como um usuario
	Eu desejo realizar o login
	Para que eu possa acessa as demais funcionalidades

Cenário: Realizar login com sucesso
Cenario: Cadastro com senha sem caractere especial
Dado Que o visitante está acessando o site da loja
Quando Ele clicar em login
E Preecher os dados do formulario de login
	| Dados                |
	| E-mail               |
	| Confirmação da Senha |
E Clicar no botão login
Então Ele será redirecionado para a vitrine
E Uma saudação com seu e-mail será exibida no menu superior