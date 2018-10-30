using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe responsável por controlar os pedidos. Mantém uma lista de pedidos que será incrementada em um intervalo determinado pelo campo 'tempoNovoPedido'.
/// </summary>
public class PedidoController : MonoBehaviour
{
    [Tooltip("Lista de pedidos que serão ser instanciados")]
    public List<Pedido> pedidosPossiveis;

    [Tooltip("Tempo necessário para que um novo pedido seja adicionado.")]
    public float tempoNovoPedido = 50f;

    /// <summary>
    /// Lista de pedidos que estão em andamento.
    /// </summary>
    public List<Pedido> pedidosAndamento;

    /// <summary>
    /// Cria um novo pedido no início da execução.
    /// </summary>
    public void Start()
    {
        //Chama uma nova Corotina que gera o primeiro pedido.
        StartCoroutine(GeraPedido());
    }

    /// <summary>
    /// Corotina que gera um novo pedido em um determinado periodo de tempo.
    /// </summary>
    /// <returns></returns>;
    public IEnumerator GeraPedido()
    {
        if (FaseController.IsFaseRodando())
        {
            //Instancia um pedido e o adiciona na lista de pedidos em andamento.
            Pedido p = InstanciaPedido();
            if (p != null)
            {
                pedidosAndamento.Add(p);

                StartCoroutine(MostrarPedido(p));

                //Espera os segundos determinados no Inspector.
                yield return new WaitForSeconds(tempoNovoPedido);
                //Quando termina essa corotina, uma nova entra em execução para gerar outros pedidos.
                StartCoroutine(GeraPedido());
            }
        }
    }

    /// <summary>
    /// Corotina responsável por apresentar um novo pedido, atualizando o tempo decorrido.
    /// </summary>
    /// <param name="pedido">Pedido que será mostrado</param>
    /// <returns></returns>
    public IEnumerator MostrarPedido (Pedido pedido)
    {
        //Instancia o prefab do objeto e o adiciona no PainelPedido para mostrar o pedido ao jogador.
        var prefPedido = Instantiate(Resources.Load<GameObject>("Prefabs/Pedido"), MyCanvas.PainelPedido);
        prefPedido.GetComponentsInChildren<Text>()[0].text = "Pedido\n" + pedido.objetoNecessario.nome;

        //Iteração que atualizará o tempo decorrido de acordo com a necessidade.
        for (int i = 0; i <= pedido.tempoDeEspera; i++)
        {
            if (pedidosAndamento.Contains(pedido) && FaseController.IsFaseRodando())
            {
                prefPedido.GetComponentsInChildren<Text>()[1].text = pedido.tempoDecorrido + "/" + pedido.tempoDeEspera;
                pedido.tempoDecorrido = i;
                yield return new WaitForSeconds(1f);
            }else
            {
                break;
            }
        }

        if (FaseController.IsFaseRodando())
        {
            //Quando chega ao fim do tempo do tempo decorrido, remove o pedido da lista de pedidos em andamento e o apaga do PainelPedidos.
            pedidosAndamento.Remove(pedido);
            MyCanvas.ApagarFilhoPainelPedido(pedido.objetoNecessario.nome);
        }
    }

    /// <summary>
    /// Instancia um novo pedido aleatório, de acordo com a lista de pedidos possíveis.
    /// </summary>
    /// <returns></returns>
    private Pedido InstanciaPedido()
    {
        if (pedidosPossiveis.Count > 0)
        {
            var r = UnityEngine.Random.Range(0, pedidosPossiveis.Count);
            var pedidoRand = pedidosPossiveis[r];
            var p = Instantiate<Pedido>(pedidoRand);
            return p;
        }else
        {
            return null;
        }
        
    }
}
