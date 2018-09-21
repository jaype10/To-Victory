using UnityEngine;

/// <summary>
/// Classe usada nos slider para que eles fiquem seguindo o personagem setado para ele.
/// </summary>
public class PersonagemSlider : MonoBehaviour {

    [Tooltip("Instancia de Personagem que será usada para deixar o Slider seguindo.")]
    public Personagem perso;
	
	/// <summary>
    /// A cada quadro, setará a posição da barra, sempre adicionando 1 de distância para não ficar na mesma posição.
    /// </summary>
	void Update () {
        try
        {
            Vector3 v = new Vector3(perso.gameObject.transform.position.x, transform.position.y, perso.gameObject.transform.position.z + 1);
            transform.position = v;
        }        
        catch (System.Exception) { }
	}
    
}
