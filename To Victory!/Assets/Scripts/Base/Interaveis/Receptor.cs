using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe responsável pelo Receptor. Este mecanismo serve para receber objetos de acordo com os pedidos que foram requisitados.
/// Herda de Interavel para reaproveitar código.
/// </summary>
public class Receptor : Interavel
{
    [Tooltip("Lista dos objetos que foram entregues")]
    public List<Objeto> objetosEntregues;

    /// <summary>
    /// Entrega um objeto. Este método será usado pelo Inspector.
    /// </summary>
    public void EntregarObjeto()
    {
        //Verifica se a mão do personagem não está vazia.
        if (Personagem.ObjetoNaMao != null)
        {
            //Caso não esteja vazia, verifica o estado do objeto para ver se ele está pronto.
            if (Personagem.ObjetoNaMao.estadoObj == EstadoObjeto.Pronto)
            {
                VerificarEntregaSucesso();
            }
            else
            {
                //Se o objeto não estiver pronto, dar um feedback para o usuário.
                Debug.Log("Não pode entregar este objeto ainda. Ele não está pronto.");
            }
        }
        else
        {
            //Se a mão do jogador estiver vazia. Mostre isso ao usuário.
            Debug.Log("Não tem objeto na mão.");
        }

    }

    /// <summary>
    /// Verifica se existe um pedido na lista de pedidos com esses parâmetros e o apaga da lista de pedidos.
    /// </summary>
    private void VerificarEntregaSucesso()
    {
        //Pega a lista de pedidos, fazendo referência à classe PedidoController que está na Camera.
        var p = Camera.main.GetComponent<PedidoController>().pedidosAndamento;

        //Verifica se na lista de pedidos há um pedido solicitando o objeto que está na mão do jogador.
        if (p.Where(x => x.VerificarPedidoCompleto(Personagem.ObjetoNaMao)).Count() > 0)
        {
            //Caso exista, adiciona o objeto na lista de ObjetosEntregues e limpa a mão do jogador.
            objetosEntregues.Add(Personagem.ObjetoNaMao);
            Personagem.ObjetoNaMao.LimparMaoJogador(Personagem.name);

            //Pega o primeiro pedido que solicite o objeto que está na mão do jogador.
            var pedido = p.Where(x => x.VerificarPedidoCompleto(Personagem.ObjetoNaMao)).First();

            //Remove este pedido da lista de pedidos.
            p.Remove(pedido);

            //Destroi o "Panfleto" dele do painel de Pedidos. 
            //Procura todos os filhos do PainelPedido e trás uma coleção de Pedidos que tenham no texto o nome do Objeto Necessário.
            //Filtra e usa apenas o primeiro pedido da coleção.
            MyCanvas.ApagarFilhoPainelPedido(pedido.objetoNecessario.nome);

            //Apaga o objeto que estiver na mão do jogador.
            Personagem.ObjetoNaMao = null;

            //Atribui a nova lista de pedido (sem o pedido que está completo agora) no campo 'pedidos' do PedidoController.
            Camera.main.GetComponent<PedidoController>().pedidosAndamento = p;

            // Avisa o persoangem que objeto foi entregue com sucesso. 
            Debug.Log("Objeto entregue com sucesso!");
        }
        else
        {
            //Caso não tenha pedidos para o objeto que está na mão do jogador, dá um feedback avisando-o.
            Debug.Log("Não existem pedidos para esse objeto.");
        }

    }
}
