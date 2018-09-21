using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe responsável por controlar a fase. Tempo e finalização são responsabilidades dela.
/// </summary>
public class FaseController : MonoBehaviour
{
    public float tempoMaximoFase;
    public float tempoDecorrido;

    public bool isFaseRodando = true;

    public Receptor receptor;

    /// <summary>
    /// Começa a contar o tempo.
    /// </summary>
    public void Start()
    {
        StartCoroutine(ContadorFase());
    }

    /// <summary>
    /// Começa a contar o tempo e o apresenta ao jogador.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ContadorFase ()
    {
        for (float i = 0; i <= tempoMaximoFase; i += 0.1f)
        {
            tempoDecorrido = i;
            MyCanvas.TxtTempo.text = string.Format("{0:00.0}", i);
            yield return new WaitForSeconds(0.1f);
        }

        FinalizarFase();
    }

    /// <summary>
    /// Finaliza a fase, calculando os pontos e apresentando os itens entregues.
    /// </summary>
    private void FinalizarFase()
    {
        Debug.Log("Acabou a fase.");
        //Para a animação de todos os personagens.
        foreach (var item in FindObjectsOfType<Personagem>())
        {
            item.GetComponent<Animator>().SetFloat("Move", 0);
        }

        //Para de rodar a fase.
        isFaseRodando = false;

        //Iteração responsável pelo cálculo de pontos finais.
        var pontosFinais = 0;
        foreach (var item in receptor.objetosEntregues)
        {
            pontosFinais += item.qtdePontos;

            //Instancia cada objeto que estiver na lista de ObjetosEntregues.
            var pref = Resources.Load<GameObject>("Prefabs/TxtItemFinal");
            pref.GetComponent<Text>().text = item.nome;
            Instantiate(pref, MyCanvas.PainelItensFinal.transform);
        }

        //Apresentação dos pontos e da janela Final.
        MyCanvas.TxtPontuacaoFinal.text = "Sua pontuação final foi " + pontosFinais;
        MyCanvas.TelaFinal.SetActive(true);

    }

    /// <summary>
    /// Método estático que verifica se uma fase está em andamento ou se já acabou.
    /// </summary>
    /// <returns>True ou False</returns>
    public static bool IsFaseRodando()
    {
        return Camera.main.GetComponent<FaseController>().isFaseRodando;
    }
}
