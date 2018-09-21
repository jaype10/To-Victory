using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Classe para manipulação de eventos.
/// </summary>
[Serializable]
public class Eventos : UnityEvent<string> { }

/// <summary>
/// Classe base para um dos objetos que serão utilizados para interação do jogador.
/// </summary>
public abstract class Interavel : MonoBehaviour {

    [Tooltip("Evento que será lançado quando houver um interação do jogador.")]
    public Eventos OnInteragiu;

    /// <summary>
    /// Mantém uma instância do personagem que está interagindo com este interável.
    /// </summary>
    public Personagem Personagem { get; set; }

    // Update is called once per frame
    public virtual void Update () { }

    /// <summary>
    /// Permite que o personagem que estiver dentro do Trigger do SphereCollider faça uma interação.
    /// </summary>
    /// <param name="other">Outro GameObject que estiver colidindo</param>
    protected virtual void OnTriggerStay (Collider other)
    {
        if (other.GetComponent<Personagem>() != null && FaseController.IsFaseRodando())
        {
            Personagem = other.GetComponent<Personagem>();
            if (Input.GetKeyDown(Personagem.botaoInteracao))
            {
                OnInteragiu.Invoke(null);
            }
        }
    }
}
