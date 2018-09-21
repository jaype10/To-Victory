using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Classe base da mecânica do Aperfeiçoador. Este mecanismo serve para aperfeiçoar um objeto quando for permitido.
/// Esta classe herda de Interavel e faz algumas sobrecargas.
/// </summary>
public class Aperfeicoador : Interavel
{
    [Tooltip("Objeto que será aperfeiçoado")]
    public Objeto obj;

    [Tooltip("Dirá ao código se este GameObject já está aperfeiçoando ou não.")]
    public bool isAperfeicoando;

    [Tooltip("Multiplicador do tempo. Útil para aumentar a velocidade em certos momentos.")]
    public int tempoAdd = 1;

    /// <summary>
    /// Sobrecarga do método de interação do Interavel. O motivo da sobrecarga é em função da necessidade de manter o botão apertado durante a interação.
    /// </summary>
    /// <param name="collider">Colisor de outro GameObject que estiver colidindo</param>
    protected override void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<Personagem>() != null && FaseController.IsFaseRodando())
        {
            Personagem = collider.GetComponent<Personagem>();
            if (Input.GetKey(Personagem.botaoInteracao))
            {
                OnInteragiu.Invoke(null);
            }
        }
    }

    /// <summary>
    /// Método usado pelo Inspector para aperfeiçoar um objeto.
    /// </summary>
    public void Aperfeicoar ()
    {
        //Verifica se a mão não está vazia.
        if (Personagem.ObjetoNaMao != null)
        {
            //Se a mão estiver ocupada
            VerificarObjetoAperfeicoavel();
        }
        else
        {
            //Se a mão estiver vazia
            Debug.Log("Não tem nada na mão.");
        }
    }

    /// <summary>
    /// Verifica se é um objeto Aperfeiçoável ou não.
    /// </summary>
    private void VerificarObjetoAperfeicoavel()
    {
        //O operador 'is' faz um teste de compatibilidade com tipos. Nesse caso eu faço uma verificação para ver se o objeto na mão do jogador é um ObjetoAperfeicoavel
        if (Personagem.ObjetoNaMao is ObjetoAperfeicoavel)
        {
            //Se for Aperfeicoavel, ele deixa aperfeiçoando
            DeixarAperfeicoando();
        }
        else
        {
            //Se não for Aperfeicoavel, ele mostra mensagem
            Debug.Log("Esse objeto não precisa de aperfeiçoamento");
        }
    }

    /// <summary>
    /// Verifica e permite deixar o objeto que estiver na mão do jogador a ser aperfeiçoado.
    /// </summary>
    private void DeixarAperfeicoando()
    {
        //Verifica se o objeto na mão do jogador já está pronto para o Aperfeiçoamento, ou já está sendo aperfeiçoando.
        if (Personagem.ObjetoNaMao.estadoObj == EstadoObjeto.PreparadoParaAperfeicoar || Personagem.ObjetoNaMao.estadoObj == EstadoObjeto.EmAperfeicoamento)
        {
            //Faz referencia ao objeto na mão do jogador como um ObjetoAperfeicoavel.
            var obj = Personagem.ObjetoNaMao as ObjetoAperfeicoavel;

            //Verifica se o tempo decorrido do aperfeiçoamento já está completo.
            if (obj.tempoDecorridoAperfeicoamento < obj.tempoAperfeicoamento)
            {
                //Se não estiver completo, adiciona mais tempo.
                obj.estadoObj = EstadoObjeto.EmAperfeicoamento;
                obj.tempoDecorridoAperfeicoamento += tempoAdd * Time.deltaTime;

                //Pega a barra gráfica do personagem que estiver interagindo e mostra uma barra de progresso do aperfeiçoamento.
                var s = MyCanvas.BarrasTarefa.Where(x => x.name == "BarraTarefaP" + Personagem.numeroJogador).FirstOrDefault();
                s.gameObject.SetActive(true);
                s.maxValue = obj.tempoAperfeicoamento;
                s.value = obj.tempoDecorridoAperfeicoamento;
            }
            else
            {
                //Se estiver completo, apaga a barra gráfica e seta o Objeto como Pronto.
                var s = MyCanvas.BarrasTarefa.Where(x => x.name == "BarraTarefaP" + Personagem.numeroJogador).FirstOrDefault();
                s.gameObject.SetActive(false);
                s.value = 0;
                obj.estadoObj = EstadoObjeto.Pronto;
            }
        }
        else
        {
            //Caso o objeto não esteja em estado de aperfeiçoamento ou esteja sendo aperfeiçoado, ele dará um feedback dizendo que não pode aperfeiçoar.
            Debug.Log("Esse objeto não está pronto para aperfeiçoar");
        }
    }
}

