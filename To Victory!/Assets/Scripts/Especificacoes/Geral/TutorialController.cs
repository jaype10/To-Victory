using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static bool isEsperandoInicio, isEsperandoPegarTrigo, isEsperandoManipular, isEsperandoEntregar;
    private static Personagem player;

    public void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Personagem>();
        DesligarTodosInteraveis();
        MostrarBemVindo();
    }

    private static void MostrarBemVindo()
    {
        Time.timeScale = 0;
        ToggleMovimentoPlayer(false);
        MessageController.Show("Olá! Bem-vindo ao tutorial do jogo. Aqui você aprenderá os conceitos básicos para mandar bem." +
                    "\n\nPara se movimentar, utilize as teclas WASD. Para interagir com os objetos, utilize o botão F.", "Próximo", () =>
                    {
                        MostrarMecanica();
                    });
    }

    private static void MostrarMecanica()
    {
        Time.timeScale = 1;
        ToggleMovimentoPlayer(true);
        MessageController.Show("Pois bem, vamos começar. Neste exemplo, você deve pegar um pouco de trigo que está na Fonte.");
        isEsperandoPegarTrigo = true;
        Camera.main.GetComponent<PedidoController>().enabled = false;
        AtivarInteravel(FindObjectsOfType<Interavel>().Where(x => x.name == "FonteMagica").FirstOrDefault());
        MyCanvas.MostrarFonte.SetActive(true);
    }

    public static void MostrarPegouObjetoFonte()
    {
        Time.timeScale = 0;
        MyCanvas.MostrarFonte.SetActive(false);
        isEsperandoPegarTrigo = false;
        DesligarTodosInteraveis();
        ToggleMovimentoPlayer(false);
        MessageController.Show("Muito bem! Você tem o trigo em mãos!", "Próximo", () =>
        {
            MostrarCozinharTrigo();
        });
    }

    public static void MostrarManipulouObjeto()
    {
        Time.timeScale = 0;
        isEsperandoManipular = false;
        MyCanvas.MostrarManipulador.SetActive(false);
        DesligarTodosInteraveis();
        ToggleMovimentoPlayer(false);
        MessageController.Show("Mandou bem! O trigo agora está cozido e pronto para ser entregue na mesa!", "Próximo", () =>
        {
            MostrarEntregarObjeto();
        });
    }

    public static void MostrarEntregouObjeto()
    {
        isEsperandoEntregar = false;
        MyCanvas.MostrarManipulador.SetActive(false);
        DesligarTodosInteraveis();
        MessageController.Show("Você entregou o pedido com êxito! Parabéns!" +
            "\nEste é o básico para mandar bem neste jogo. Treine bastante e esteja preparado para mais desafio no futuro. " +            
            "\nNovos pedidos continuarão surgindo, tente completá-los por sozinho!" +
            "\nQuando quiser ir para a cena Protótipo, clique abaixo.", "Ir para Protótipo", () => {
                UnityEngine.SceneManagement.SceneManager.LoadScene("prototipo");
            });
        AtivarInteravel(FindObjectsOfType<Interavel>().Where(x => x.name.Equals("FonteMagica")).FirstOrDefault());
        AtivarInteravel(FindObjectsOfType<Interavel>().Where(x => x.name.Equals("Fornalha")).FirstOrDefault());
        AtivarInteravel(FindObjectsOfType<Interavel>().Where(x => x.name.Equals("Carrinho")).FirstOrDefault());
        Camera.main.GetComponent<PedidoController>().enabled = false;
    }

    private static void MostrarEntregarObjeto()
    {
        Time.timeScale = 1;
        MyCanvas.MostrarReceptor.SetActive(true);
        ToggleMovimentoPlayer(true);
        MessageController.Show("Para finalizar este pedido, pegue o trigo pronto que está no fogão e coloque-o na mesa.");
        isEsperandoEntregar = true;
        AtivarInteravel(FindObjectsOfType<Interavel>().Where(x => x.name.Equals("Fornalha")).FirstOrDefault());
        AtivarInteravel(FindObjectsOfType<Interavel>().Where(x => x.name.Equals("Carrinho")).FirstOrDefault());
    }

    private static void MostrarCozinharTrigo()
    {
        Time.timeScale = 1;
        MyCanvas.MostrarManipulador.SetActive(true);
        ToggleMovimentoPlayer(true);
        MessageController.Show("Agora, coloque o objeto no fogão para que ele possa cozinhar.");
        isEsperandoManipular = true;
        AtivarInteravel(FindObjectsOfType<Interavel>().Where(x => x.name.Equals("Fornalha")).FirstOrDefault());
    }

    private static void AtivarInteravel(Interavel interavel)
    {
        interavel.enabled = true;
        interavel.GetComponent<SphereCollider>().enabled = true;
    }

    private static void ToggleMovimentoPlayer(bool movimentar)
    {
        player.enabled = movimentar;
    }

    private static void DesligarTodosInteraveis()
    {
        foreach (var item in FindObjectsOfType<Interavel>())
        {
            item.enabled = false;
            item.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
