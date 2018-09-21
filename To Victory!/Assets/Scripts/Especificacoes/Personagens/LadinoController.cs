using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Especificação da classe Personagem. As especificações de personagem servirão para determinar o tipo de personagem que o jogador estará usando. 
/// Para setar os atributos, use o Inspector. No caso do Ladino, ele terá uma velocidade a mais por ser Ladino.
/// </summary>
public class LadinoController : Personagem
{
    protected override void Start()
    {
        Vel *= 1.4f;
        base.Start();
    }
}
