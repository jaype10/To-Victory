using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A classe Fonte serve como base para interáveis que darão os recursos ao jogador. Também herda de Interável para reaproveitamento de código.
/// </summary>
public class Fonte : Interavel
{
    [Tooltip("Lista com os objetos que estarão disponíveis.")]
    public List<Objeto> objetos;
    protected bool isInfinito = false;
    /// <summary>
    /// Pega um objeto que esteja disponível na fonte.
    /// </summary>
    public virtual void PegarObjeto ()
    {
        //Verifica se o personagem está com a mão ocupada.
        if (Personagem.ObjetoNaMao == null)
        {
            //Se não estiver, verifica se a fonte tem recursos para dar ao jogador.
            if (objetos.Count > 0)
            {
                //Caso tenha recursos, adiciona um Objeto na propriedade ObjetoNaMao de Personagem e cria um prefab para mostrar isto.
                Objeto a = objetos.ElementAt(0);

                //Se for infinito, essa fonte sempre terá a mesma quantidade de objetos até o final. Também, só permite que 1 objeto seja pego por ela.
                //Caso não seja, remove o primeiro objeto da lista.
                if (!isInfinito)
                {
                    objetos.Remove(a);
                }

                if (TutorialController.isEsperandoPegarTrigo)
                {
                    TutorialController.MostrarPegouObjetoFonte();
                }

                Personagem.ObjetoNaMao = Instantiate(a);
                a.SetarMaoJogador(Personagem.name);
            }
            else
            {
                //Caso não tenha recursos disponíveis, dá um feedback de Fonte Vazia.
                Debug.Log("Acabou os objetos desta fonte.");
            }
        }
        else
        {
            //Caso tenha um objeto na mão, dar feedback sobre mão ocupada.
            Debug.Log("Já tem um objeto na mão.");
        }
    }

}
