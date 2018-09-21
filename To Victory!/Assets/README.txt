-----Mecanismos-----


Os objetos interáveis estarão de maneira genérica, ou seja, haverá, por exemplo, um fogão e uma fornalha com a mesma funcionalidade, deixar algo por um tempo para que esteja pronto.

Eu dividi por enquanto em três estados do objeto cada um:

-Inicial: Estado inicial do objeto, saído direto de sua fonte.
-EmManipulacao: Estado em que o objeto começou a ser manipulado. O jogador poderá ver esse estado apenas caso tenha o colocado para manipulação.
-PreparadoParaAperfeicoar: Estado em que o objeto está pronto para ser Aperfeiçoado. Este estado é opcional e dependerá do objeto usado.
-EmAperfeicoamento: Estado em que o objeto está em processo de Aperfeiçoamento. Este estado é opcional e dependerá do objeto usado.
-Pronto: Estado final em que o objeto está pronto para ser entregue.


Esses estados estão descritos com a enumeração EstadoObjeto.



Existem cinco tipos de objetos interáveis:

-Fonte: Aonde o jogador poderá retirar objetos em estado inicial.
-Manipulador: Local em que o jogador deixará o objeto para ser manipulado até o estado Pronto.
-Aperfeiçoador: Neste interável o jogador poderá aperfeiçoar um objeto. Para tal, ele deve 
-Armazenador: O Armazenador servirá para dois propósitos. Para guardar um objeto ou juntar dois objetos para formar uma combinação. 
-Receptor: Receberá o objeto pronto.

A classe Fonte, Manipulador, Aperfeiçoador, Armazenador e Receptor são especificações de Interavel, mas serão usadas como base para criar classes específicas de cada momento, como a Fornalha por exemplo.


Também foi criada uma classe Personagem, que contará com todas as ações padrões de personagens. Ele deverá ser especificado de acordo com cada personagem e sua particularidade.

-----Jogabilidade------

Estão sujeitos a alterações a qualquer momento, mas por enquanto:

Player 1 - Azul:

Movimentação - Setas >>> Está definido em Input -> HorizontalP1 e VerticalP1
Interagir - ENTER >>> Este é um campo dentro da classe Personagem, é facilmente trocado.


Player 2 - Verde:

Movimentação - WASD >>> Está definido em Input -> HorizontalP2 e VerticalP2
Interagir - F >>> Este é um campo dentro da classe Personagem, é facilmente trocado.


Player 3 - Amarelo:

Movimentação - IJKL >>> Está definido em Input -> HorizontalP3 e VerticalP3.
Interagir - P >>> Este é um campo dentro da classe Personagem, é facilmente trocado.


---------Mecânica-------


O cenário que o personagem Verde se encontra é a área de conserto de armaduras e armas.
As encomendas chegarão no Balcão (cubo cinza) a cada 15 segundos.
O Jogador deve pegar uma encomenda e levá-la à fornalha para deixá-la maleável (apertar uma vez o botão de interação deixará a encomenda na fornalha).
Após, deve consertar a armadura com um martelo (segurar o botão de interação fará isto) e deixá-la nova em folha.
Por fim, a encomenda pronta deve ser levada ao carrinho que levará para o campo de batalha.


-------Configuração----------

Objetos:
	Quando estiver criando objetos de itens, como Armadura, Alimentos e etc, sempre criar um Prefab com o mesmo NOME
do Objeto. Este prefab será procurado pelo código e criará o Prefab na "mão" do jogador.

Combinação:
	O jogador poderá criar combinações dos itens. Para resolver esse problema, deve-se criar um ScriptableObject de Combinacao. Esse objeto terá as configurações necessárias para criar as combinações.
	Em outras palavras, existirá um banco de Combinações, assim como terá um banco de Objetos e de Prefabs de Objetos.

Pedido:

	Pedidos serão enviados para o jogador. A barra inferior mostrará os pedido em andamento. 