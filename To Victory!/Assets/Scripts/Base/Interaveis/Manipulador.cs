using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável pelo Manipulador. Este mecanismo é usado como base para fornalhas e fogões. Serve para deixar um Objeto sendo manipulado, 
/// sem a necessidade do personagem estar próximo.
/// </summary>
public class Manipulador : Interavel
{
    [Tooltip("Objeto que será manipulado")]
    public Objeto objeto;
    [Tooltip("Trigger que verifica se o Manipulador está em uso ou não")]
    public bool isManipulando;
    /// <summary>
    /// Cor padrão para a troca de cor (Temporário, imagino que usaremos outra forma de dar um feedback ao usuário).
    /// </summary>
    private Color corNormal;

    /// <summary>
    /// Cor diferente para a troca de cor (Temporário, imagino que usaremos outra forma de dar um feedback ao usuário).
    /// </summary>
    [SerializeField]
    protected Color corUsando = Color.red;

    /// <summary>
    /// Método para setar a cor inicial quando iniciar o código.
    /// </summary>
    private void Awake()
    {
        corNormal = GetComponent<Renderer>().material.color;
    }

    /// <summary>
    /// Sobrecarga do método Update da classe Interável.
    /// </summary>
    public override void Update()
    {
        //Verifica se o Manipulador está sendo usado e muda a cor.
        if (isManipulando)
        {
            //Cor para quando está sendo usado.
            GetComponent<Renderer>().material.color = corUsando;
        }
        else
        {
            //Cor para quando não está sendo usado.
            GetComponent<Renderer>().material.color = corNormal;
        }
    }

    /// <summary>
    /// Guarda um objeto no campo 'objeto'
    /// </summary>
    /// <param name="obj">Objeto que será guardado</param>
    public void GuardarObjeto(Objeto obj)
    {
        objeto = obj;
    }

    /// <summary>
    /// Retira o objeto que está guardado.
    /// </summary>
    /// <returns></returns>
    public Objeto RetirarObjetoGuardado()
    {
        //Verifica se existe objeto guardado e o retorna caso exista objeto guardado.
        if (objeto != null)
        {
            return objeto;
        }
        //Caso não exista, retorna null.
        return null;
    }

    /// <summary>
    /// Responsável pela execução da manipulação. Será usado pelo Inspector.
    /// </summary>
    public void Manipulacao()
    {
        //Verifica se o personagem tem um objeto na mão.
        if (Personagem.ObjetoNaMao != null)
        {
            //Coloca para manipular
            DeixarManipulando();
        }
        else if (objeto != null)
        {
            //Senão, caso exista objeto sendo manipulado, retira este objeto.
            RetirarObjetoManipulacao();
        }
        else
        {
            //Por fim, caso a mão do jogador esteja vazia, dá um feedback de que mão está vazia.
            Debug.LogError("Não há objeto na mão");
        }
    }

    /// <summary>
    /// Verifica se um objeto pode ser manipulado no momento.
    /// </summary>
    public void DeixarManipulando()
    {
        //Verifica se tem um objeto na mão do jogador.
        if (Personagem.ObjetoNaMao != null)
        {
            //Se tiver, faz outra verificação para ver se não existe um objeto em manipulação.
            if (objeto == null)
            {
                //Se não houver objeto em manipulação, guarda o objeto e limpa a mão do jogador.
                GuardarObjeto(Personagem.ObjetoNaMao);
                Personagem.ObjetoNaMao.LimparMaoJogador(Personagem.name);
                Personagem.ObjetoNaMao = null;
                isManipulando = true;

                StartCoroutine(Manipular()); // Inicia Co-rotina de Manipulação
                Debug.Log("Colocou para manipular");
            }else
            {
                //Caso exista objeto em manipulação, avisa o jogador.
                Debug.Log("Já existe objeto em manipulação.");
            }
        }
    }

    /// <summary>
    /// Método para retirar um objeto que esteja em manipulação.
    /// </summary>
    public void RetirarObjetoManipulacao()
    {
        //Verifica se existe objeto sendo manipulado.
        if (isManipulando)
        {
            //Se existir, coloca o objeto na mão do jogador, interrompe a corotina Manipular e avisa o jogador que o objeto não está 100% manipulado.
            Personagem.ObjetoNaMao = RetirarObjetoGuardado();
            isManipulando = false;
            objeto = null;
            Debug.Log("Retirou objeto antes de terminar");
        }
        else
        {
            //Se não existir, o objeto que estiver no campo 'objeto' irá para a mão do jogador.
            Personagem.ObjetoNaMao = RetirarObjetoGuardado();
            objeto = null;
            //Apaga a BarraFornalha e mostra um feedback de que retirou um objeto pronto.
            MyCanvas.BarraFornalha.gameObject.SetActive(false);
            MyCanvas.BarraFornalha.value = 0;
            Debug.Log("Retirou objeto pronto");
        }

        //Cria o prefab do objeto na mão do jogador.
        Personagem.ObjetoNaMao.SetarMaoJogador(Personagem.name);
    }

    /// <summary>
    /// Corotina responsável pelo efeito de manipulação de objetos. Apenas uma corotina Manipular pode ser executada por vez.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Manipular()
    {
        //Adiciona o estado EmManipulação para o objeto e mostra a BarraFornalha como uma barra de progresso.
        Debug.Log("Manipulando...");
        objeto.estadoObj = EstadoObjeto.EmManipulacao;
        MyCanvas.BarraFornalha.gameObject.SetActive(true);
        MyCanvas.BarraFornalha.maxValue = objeto.tempoParaFicarPronto;

        //Loop para manipular um objeto, usando o campo 'tempoParaFicarPronto' como condição para finalização.
        for (float i = objeto.tempoDecorrido; objeto != null && i < objeto.tempoParaFicarPronto; i += 0.01f)
        {
            //Verifica se o manipulador esteja ativado. Caso ele tenha sido desativado em algum momento, para a iteração.
            if (!isManipulando || !FaseController.IsFaseRodando())
            {
                break;
            }

            //Seta o tempo decorrida da manipulação, assim como o valor da fornalha.
            objeto.tempoDecorrido = i;
            MyCanvas.BarraFornalha.value = i;

            //Espera por 0.01 segundos antes de ir para a próxima iteração.
            yield return new WaitForSeconds(0.01f);
        }

        //Verifica se existe um objeto no Manipulador.
        if (objeto != null && FaseController.IsFaseRodando())
        {
            //Se tiver, verifica se ele pode ser Aperfeiçoavel.
            if (objeto is ObjetoAperfeicoavel && objeto.estadoObj != EstadoObjeto.Pronto)
            {
                //Caso possa aperfeiçoar, adicionar o estado PreparadoParaAperfeicoar.
                objeto.estadoObj = EstadoObjeto.PreparadoParaAperfeicoar;
            }
            else
            {
                //Caso não possa aperfeiçoar, deixe o objeto como Pronto.
                objeto.estadoObj = EstadoObjeto.Pronto;
            }

            //Avisa que o Manipulador está em uso.
            isManipulando = false;
        }
    }
}
