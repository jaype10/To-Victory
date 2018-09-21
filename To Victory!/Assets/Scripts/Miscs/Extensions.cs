using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe extensiva para algumas classes. Servirão para adicionar métodos a classe que não podemos modificar.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Faz uma verificação de igualdade com o tipo float.
    /// Por conta das casas decimais variar muito, esse método se faz necessário, principalmente para calcular distâncias.
    /// </summary>
    /// <param name="num1">Primeiro valor</param>
    /// <param name="num2">Segundo valor</param>
    /// <returns></returns>
    public static bool IsIgual(this float num1, float num2)
    {
        //Verifica se o número é maior do que o outro com uma margem de erro de 0.1.
        if (num1 >= num2 - 0.1f && num1 <= num2 + 0.1f)
        {
            return true;
        }
        return false;
    }

}
