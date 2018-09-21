using UnityEngine;
/// <summary>
/// Especificação da classe Aperfeicoador. A Forja será algo concreto e compreesível ao jogador, por isso é uma classe que serve apenas para herdar de Aperfeicoador.
/// </summary>
public class ForjaController : Aperfeicoador
{
    /// <summary>
    /// Sobrecarga do método OnTriggerStay. Essa sobrecarga serve para fazer uma verificação da classe do Personagem antes de executar os métodos da classe Aperfeicoador.  
    /// </summary>
    /// <param name="collider">Outro GameObject que estiver colidindo</param>
    protected override void OnTriggerStay(Collider collider)
    {
        //Verifica se o Personagem pode ser um BarbaroController.
        if (Personagem is BarbaroController)
        {
            //Se for, adiciona um multiplicador de 3 para forjar uma armadura.
            tempoAdd = 3;
        }
        else
        {
            //Se não, mantém o tempo padrão de 1.
            tempoAdd = 1;
        }
        
        //Executa o mesmo método, mas da classe Aperfeicoador.
        base.OnTriggerStay(collider);
    }

}
