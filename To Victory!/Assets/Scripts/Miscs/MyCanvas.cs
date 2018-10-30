using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe Helper para usar as interfaces gráficas. Essa classe contém apenas métodos e propriedades static pois serão acessadas sem necessidade de instância da classe MyCanvas.
/// Isso é bastante útil para não precisar ficar codificando a busca do GameObject sempre que necessitar da interface gráfica. 
/// </summary>
public static class MyCanvas
{
    /// <summary>
    /// Essa propriedade buscará todas as barras de tarefas do jogador 1, jogador 2 e jogador 3. Essa barra de tarefa será usada para mostrar quando um aperfeiçoamento está pronto.
    /// </summary>
    public static Slider[] BarrasTarefa
    {
        get
        {
            var g = Resources.FindObjectsOfTypeAll<Slider>().Where(x => x.CompareTag("BarraTarefa")).ToArray();            
            return g;
        }
    }

    /// <summary>
    /// Essa propriedade buscará a barra de progresso da fornalha. Essa barra é usado para mostrar o status de uma manipulação.
    /// </summary>
    public static Slider BarraFornalha
    {
        get
        {
            var g = GameObject.Find("CanvasWorld").transform.Find("BarraFornalha");
            return g.GetComponent<Slider>();
        }
    }

    /// <summary>
    /// Essa propriedade buscará o PainelPedido. Especificamente, buscará o filho Content deste ScrollView, pois é no Content que o conteúdo de um ScrollView deve ficar.
    /// </summary>
    public static RectTransform PainelPedido
    {
        get
        {
            var scrollView = GameObject.Find("Canvas").transform.Find("Painel_Pedidos");
            return scrollView.Find("Viewport").Find("Content") as RectTransform;
        }
    }

    /// <summary>
    /// Essa propriedade buscará todos os filhos de PainelPedido. Basicamente, pegará todos os pedidos (em relação a interface gráfica) que estiverem em progresso.
    /// </summary>
    public static List<GameObject> FilhosPainelPedido
    {
        get
        {
            var lista = new List<GameObject>();

            for (int i = 0; i < PainelPedido.childCount; i++)
            {
                var g = PainelPedido.GetChild(i).gameObject;
                lista.Add(g);
            }

            return lista;
        }
    }

    /// <summary>
    /// Essa propriedade buscará o TxtTempo para mostrar o decorrer da fase. 
    /// </summary>
    public static Text TxtTempo
    {
        get
        {
            var g = GameObject.Find("Canvas").transform.Find("TxtTempo");
            return g.GetComponent<Text>();
        }
    }

    /// <summary>
    /// Essa propriedade buscará a TelaFinal.
    /// </summary>
    public static GameObject TelaFinal
    {
        get
        {
            return GameObject.Find("Canvas").transform.Find("TelaFinal").gameObject;
        }
    }

    /// <summary>
    /// Essa propriedade buscará o PainelItensFinal.
    /// </summary>
    public static GameObject PainelItensFinal
    {
        get
        {
            return TelaFinal.transform.Find("Painel_Items").Find("Viewport").Find("Content").gameObject;
        }
    }

    /// <summary>
    /// Essa propriedade buscará o TxtItensEntregues para mostrar a pontuação final do jogador.
    /// </summary>
    public static Text TxtPontuacaoFinal
    {
        get
        {
            return TelaFinal.transform.Find("TxtPontuacaoFinal").GetComponent<Text>();
        }
    }

    private static GameObject PainelMensagem
    {
        get
        {
            return GameObject.Find("Canvas").transform.Find("Painel_Mensagens").gameObject;
        }
    }

    public static Text Mensagem
    {
        get
        {
            return PainelMensagem.transform.Find("TxtMensagem").GetComponent<Text>();
        }
    }

    public static Button BtnMensagemProximo
    {
        get
        {
            return PainelMensagem.transform.Find("BtnNext").GetComponent<Button>();
        }
    }

    public static GameObject MostrarReceptor
    {
        get
        {
            return GameObject.Find("CanvasWorld").transform.Find("MostrarReceptor").gameObject;
        }
    }

    public static GameObject MostrarManipulador
    {
        get
        {
            return GameObject.Find("CanvasWorld").transform.Find("MostrarManipulador").gameObject;
        }
    }

    public static GameObject MostrarFonte
    {
        get
        {
            return GameObject.Find("CanvasWorld").transform.Find("MostrarFonte").gameObject;
        }
    }

    /// <summary>
    /// Procura e apaga o primeiro filho que encontrar. O algoritmo procurará pelo nome do Objeto Necessário do pedido.
    /// </summary>
    /// <param name="nomeObjetoNecessario">Nome do objeto que será procurado</param>
    public static void ApagarFilhoPainelPedido (string nomeObjetoNecessario)
    {
        if (FilhosPainelPedido.Where(x => x.GetComponentInChildren<Text>().text.Contains(nomeObjetoNecessario)).Count() > 0)
        {
            GameObject.Destroy(FilhosPainelPedido
                .Where(x => x.GetComponentInChildren<Text>().text.Contains(nomeObjetoNecessario))
                .First());
        }
    }    

}
