using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Classe base para todos os Objetos que serão usados no projeto.
/// </summary>
[CreateAssetMenu(fileName = "Objeto", menuName = "Objetos/Objeto base")]
public class Objeto : ScriptableObject
{
    /// <summary>
    /// Nome do Objeto
    /// </summary>
    public string nome;
    /// <summary>
    /// Estado que se encontra
    /// </summary>
    public EstadoObjeto estadoObj;
    /// <summary>
    /// Quantidade de pontos que serão adicionados caso esse item esteja na lista de objetos entregues.
    /// </summary>
    public int qtdePontos;
     /// <summary>
     /// Tempo necessário para que ele fique pronto.
     /// </summary>
    public float tempoParaFicarPronto;
    /// <summary>
    /// Tempo decorrido.
    /// </summary>
    public float tempoDecorrido;

    /// <summary>
    /// Verifica se pode combinar
    /// </summary>
    public bool isCombinavel;

    /// <summary>
    /// Lista de combinações possíveis.
    /// </summary>
    public List<Combinacao> combinacoes;


    /// <summary>
    /// Adiciona um prefab na mão do jogador.
    /// </summary>
    /// <param name="nomeJogador">Nome, no Hierarchy, do jogador.</param>
    public void SetarMaoJogador(string nomeJogador)
    {
        //Pega o número do jogador do nome dele.
        var numJogador = int.Parse(nomeJogador.ElementAt(nomeJogador.Length - 1).ToString());

        //Instancia o prefab, procura a 'Mao' do player e cria o prefab na mão do player.
        var prefab = Resources.Load<GameObject>("Prefabs/" + this.nome);
        var mao = GameObject.Find("Player" + numJogador).transform.Find("Mao");
        GameObject.Instantiate(prefab, mao.position, Quaternion.Euler(0, 0, 90), mao);
    }

    /// <summary>
    /// Retira o prefab da mão do jogador.
    /// </summary>
    /// <param name="nomeJogador">Nome, no Hierarchy, do jogador.</param>
    public void LimparMaoJogador(string nomeJogador)
    {
        //Pega o número do jogador do nome dele.
        var numJogador = int.Parse(nomeJogador.ElementAt(nomeJogador.Length - 1).ToString());

        //Procura a mão do jogador e faz uma iteração que destroi todos os GameObjects que estiverem na 'Mao' do jogador.
        var mao = GameObject.Find("Player" + numJogador).transform.Find("Mao");
        for (int i = 0; i < mao.childCount; i++)
        {
            GameObject.Destroy(mao.GetChild(i).gameObject);
        }
    }


    /// <summary>
    /// Sobrecarga do operador + para facilitar na hora de combinar objetos. Retorna um objeto combinado, ou NULL caso não possa combinar.
    /// </summary>
    /// <param name="o1">Primeiro objeto</param>
    /// <param name="o2">Segundo objeto</param>
    /// <returns>Objeto combinado</returns>
    public static Objeto operator +(Objeto o1, Objeto o2)
    {
        //Faz uma verificação para testar se todos ingredientes foram fornecidos corretamente.
        if (o1.combinacoes.Where(x => x.VerificarIngredientes(new Objeto[] { o1, o2 })).Count() > 0)
        {
            //Retorna o objeto 'resultado' da combinação.
            return o1.combinacoes.Where(x => x.VerificarIngredientes(new Objeto[] { o1, o2 })).First().resultado;
        }
        //Caso os ingredientes não batam, retorna NULL.
        return null;
    }

    /// <summary>
    /// Sobrecarga do operador == para facilitar a igualdade de instancias.
    /// </summary>
    /// <param name="o1">Primeiro objeto</param>
    /// <param name="o2">Segundo objeto</param>
    /// <returns></returns>
    public static bool operator ==(Objeto o1, Objeto o2)
    {
        //Primeiro verifica se é um referência nula.
        if (ReferenceEquals(o1, null) || ReferenceEquals(o2, null))
        {
            // Caso seja, verifica se ambos são referências nulas.
            return ReferenceEquals(o1, o2);
        }

        //Verifica se um objeto é igual ao outro pelo Nome, IsCombinavel, EstadoObj e TempoParaFicarPronto 
        if (o1.nome == o2.nome &&
            o1.isCombinavel == o2.isCombinavel &&
            o1.estadoObj == o2.estadoObj &&
            o1.tempoParaFicarPronto == o2.tempoParaFicarPronto)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Sobrecarga do operador != para facilitar a igualdade de instancias.
    /// </summary>
    /// <param name="o1">Primeiro objeto</param>
    /// <param name="o2">Segundo objeto</param>
    /// <returns></returns>
    public static bool operator !=(Objeto o1, Objeto o2)
    {
        //Primeiro verifica se é um referência nula.
        if (ReferenceEquals(o1, null) || ReferenceEquals(o2, null))
        {
            // Caso seja, verifica se ambos são referências nulas.
            return !(ReferenceEquals(o1, o2));
        }

        //Verifica se um objeto não é igual ao outro pelo Nome, IsCombinavel, EstadoObj e TempoParaFicarPronto 
        if (o1.nome != o2.nome ||
            o1.isCombinavel != o2.isCombinavel ||
            o1.estadoObj != o2.estadoObj ||
            o1.tempoParaFicarPronto != o2.tempoParaFicarPronto)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Sobrecarga do Equals em função do ==
    /// </summary>
    /// <param name="other">Qualquer objeto</param>
    /// <returns></returns>
    public override bool Equals(object other)
    {
        return this == (other as Objeto);
    }

    /// <summary>
    /// Sobrecarga do GetHashCode em função do Equals
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return nome.GetHashCode() * 412;
    }

}
