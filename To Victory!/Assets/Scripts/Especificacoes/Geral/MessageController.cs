using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;
using UnityEngine.UI;

public class MessageController
{
    public static void Show(string mensagem)
    {
        MyCanvas.Mensagem.text = mensagem;
        MyCanvas.BtnMensagemProximo.gameObject.SetActive(false);
    }

    public static void Show(string mensagem, string textoBotao, UnityAction eventoClick)
    {
        MyCanvas.Mensagem.text = mensagem;
        MyCanvas.BtnMensagemProximo.gameObject.SetActive(true);
        MyCanvas.BtnMensagemProximo.GetComponentInChildren<Text>().text = textoBotao;
        MyCanvas.BtnMensagemProximo.onClick.RemoveAllListeners();
        MyCanvas.BtnMensagemProximo.onClick.AddListener(eventoClick);
    }
}
