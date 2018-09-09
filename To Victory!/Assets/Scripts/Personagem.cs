using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour {

    //========================Variaveis============
    public Rigidbody rigid;

    public Animator anim;

    public int numeroPlayer = 1;

    [SerializeField] private float velocidade = 5f;
    [SerializeField] private float velorcidadeRotacao = 10f;

    private Objeto objetoNaMao;


    
	// Use this for initialization
	protected virtual void Start () {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
        if (FaseController.IsFaseRodando)
        {
            Mover();
        }
	}

    public void Mover()
    {
        float y = Input.GetAxis("Vertical") * velocidade;
        float x = Input.GetAxis("Horizontal") * velocidade;
        transform.Translate(x, y, 0);

    }
}
