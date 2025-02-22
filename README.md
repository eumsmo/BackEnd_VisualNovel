# BackEnd VisualNovel

Repositório dedicado a um trabalho da materia de Persistência de Dados do 7º Periodo do curso de Jogos Digitais na PUC Minas.

A atividade consiste em montar uma sequência de quadros ou um visual novel, onde o conteúdo da história seja recebido de um servidor.

Neste caso, optei pela opção do formato RAW no Github e configurei um formato de pastas para que a história possa continuar por uma quantidade de quadros indeterminados. Sempre começando pela pasta 'inicio'.

O formato segue como:
- Resources/:
	- inicio/: (sempre começa por essa pasta)
		- imagem.png
		- texto.txt
		- proximo.txt [contém '2']
	- 2/:
		- imagem.png
		- texto.txt
		- proximo.txt [contém a próxima pasta]
	- .../
		- imagem.png
		- texto.txt

Dessa forma, a história só irá acabar quando não houver mais arquivos 'proximo.txt'. Optei pelo arquivo adicional de direcionamento, ao invés de apenas somar um inteiro, pois dessa forma é possível adicionar quadros no meio da história sem precisar alterar os seguintes.