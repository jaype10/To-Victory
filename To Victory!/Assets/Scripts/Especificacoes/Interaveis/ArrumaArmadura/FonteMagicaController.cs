using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// A fonte mágica permite pegar objetos infinitamente.
/// </summary>
public class FonteMagicaController : Fonte
{
    public void Awake()
    {
        isInfinito = true;
    }
}
