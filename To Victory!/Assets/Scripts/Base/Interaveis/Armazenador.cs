using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Classe base da mecânica do Armazenador. Este mecanismo serve para armazenar um objeto e combiná-lo caso seja possível.
/// Esta classe herda de Interavel para reaproveitar código.
/// </summary>
public class Armazenador : Interavel
{
    [Tooltip("Local do objeto que ficará armazenado")]
    public Objeto objetoArmazenado;

    /// <summary>
    /// Método usado pelo Inspector para armazenar algum objeto.
    /// </summary>
    public void ArmazenarObjeto()
    {
        //Verifica se já existe objeto armazenado neste GameObject.
        if (objetoArmazenado == null)
        {
            //Se não existir nenhum objeto armazenado, ele deixa um objeto armazenado.
            DeixarObjeto();
        }
        else
        {
            //Se existir objeto armazenado, ele verifica se o objeto na mão do jogador é Combinável e faz a combinação
            if (Personagem.ObjetoNaMao != null && Personagem.ObjetoNaMao.isCombinavel && objetoArmazenado.isCombinavel)
            {
                CombinarObjetos();
            }
            else
            {                
                VoltarObjetoArmazenado();
            }
        }
    }

    /// <summary>
    /// Esse método irá voltar o objeto armazenado para a mão do jogador.
    /// </summary>
    private void VoltarObjetoArmazenado()
    {
        //Verifica se o personagem tem algo na mão.
        if (Personagem.ObjetoNaMao != null)
        {
            //Se tiver, dará um feedback mostrando que a mão do jogador está ocupada.
            Debug.Log("A mão do jogador está ocupada.");
        }
        else
        {
            //Se não tiver, colocará o objeto na mão do jogador e limpará o campo 'objetoArmazenado'.
            Personagem.ObjetoNaMao = objetoArmazenado;
            Personagem.ObjetoNaMao.SetarMaoJogador(Personagem.name);
            objetoArmazenado = null;
        }
    }

    /// <summary>
    /// Deixa o objeto que estiver na mão do usuário no campo 'objetoArmazenado' e limpará a mão do jogador.
    /// </summary>
    public void DeixarObjeto()
    {
        objetoArmazenado = Personagem.ObjetoNaMao;
        Personagem.ObjetoNaMao.LimparMaoJogador(Personagem.name);
        Personagem.ObjetoNaMao = null;
    }

    /// <summary>
    /// Combina dois objetos, o que estiver na mão do jogador com o objeto que estiver armazenado.
    /// Este método sempre será usado quando os dois objetos forem diferentes de nulo. Caso contrário uma exceção será lançada.
    /// </summary>
    public void CombinarObjetos()
    {
        //Faz a combinação dos objetos e verifica se é possível combinar
        //(para mais detalhes sobre o operador de soma (+), veja a classe Objeto) 
        if ((Personagem.ObjetoNaMao + objetoArmazenado) != null)
        {
            //Se for possível, o objeto combinado ficará no campo 'objetoArmazenado' e os outros dois objetos que serviram de ingrediente somirão.
            Debug.Log("Objeto combinado");
            objetoArmazenado = Personagem.ObjetoNaMao + objetoArmazenado;

            //Limpa a mão do jogador após a combinação.
            Personagem.ObjetoNaMao.LimparMaoJogador(Personagem.name);
            Personagem.ObjetoNaMao = null;
        }
        else
        {
            //Se não for possível, dá um feedback sobre a incompatibilidade.
            Debug.Log("Não é possível combinar estes objetos");
        }
    }

}
