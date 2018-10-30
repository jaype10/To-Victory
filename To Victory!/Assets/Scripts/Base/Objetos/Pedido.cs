using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Classe responsável pelas informações dos pedidos.
/// </summary>
[CreateAssetMenu(fileName = "Pedido", menuName = "Objetos/Pedido...")]
public class Pedido : ScriptableObject
{
    /// <summary>
    /// Objeto necessário do pedido.
    /// </summary>
    public Objeto objetoNecessario;
    /// <summary>
    /// Tempo máximo de espera.
    /// </summary>
    public float tempoDeEspera;
    /// <summary>
    /// Tempo decorrido desde que esse pedido foi requisitado.
    /// </summary>
    public float tempoDecorrido;

    /// <summary>
    /// Verifica se o pedido está completo.
    /// </summary>
    /// <param name="objEntregue">Objeto que foi entregue</param>
    /// <returns></returns>
    public bool VerificarPedidoCompleto (Objeto objEntregue)
    {
        //Obriga que o objeto necessário esteja pronto.
        objetoNecessario = Instantiate(objetoNecessario);
        objetoNecessario.estadoObj = EstadoObjeto.Pronto;
        //Verifica se o objeto entregue é o mesmo do objeto necessário e se está dentro do tempo de espera.
        if (objEntregue.nome == objetoNecessario.nome && objEntregue.estadoObj == EstadoObjeto.Pronto)
        {
            return true;
        }else
        {
            return false;
        }
    }

}
