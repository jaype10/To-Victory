using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável por criar as combinações de objetos.
/// </summary>
[CreateAssetMenu(menuName = "Objetos/Combinações", fileName = "Combinacao")]
public class Combinacao : ScriptableObject
{
    /// <summary>
    /// Ingredientes que serão necessários.
    /// </summary>
    public List<Objeto> ingredientes;
    /// <summary>
    /// Objeto que será resultado caso todos os ingredientes sejam fornecidos.
    /// </summary>
    public Objeto resultado;

    /// <summary>
    /// Verifica se todos os ingredientes foram fornecidos corretamente.
    /// </summary>
    /// <param name="objetos">Array dos objetos.</param>
    /// <returns></returns>
    public bool VerificarIngredientes(Objeto[] objetos)
    {
        //Cria uma nova lista de objetos e coloca todos os ingredientes necessários.
        var ing = new List<Objeto>();
        ingredientes.ForEach(x => ing.Add(x));

        //Verifica se a lista tem o ingrediente e retira esse ingrediente da lista caso exista.
        if (ing.Contains(objetos[0]))
        {
            ing.Remove(objetos[0]);
        }

        if (ing.Contains(objetos[1]))
        {
            ing.Remove(objetos[1]);
        }


        //Se a lista de ingredientes estiver vazia ao término do algoritmo, retorna true, se não retorna false.
        if (ing.Count > 0)
        {
            return false;
        }else
        {
            return true;
        }
    }
}
