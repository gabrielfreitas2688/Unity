using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{

    private Animator    playerAnimator;
    private Rigidbody2D playerRigidbody;    
    public Transform    groundCheck, bala;
    public bool         isGrounded;
    public bool         facingRight = true;
    private float       speed = 5;
    public float        direcao;

    public bool         jump = false;
    private float       jumpForce = 300;

    public int          numberJumps = 0;
    public int          maxJumps = 2;

    // Variáveis pertinentes ao tiro
    public float        velocidadetiro;
    public GameObject   tiroprefab;

    public float        delaytiro;

    private bool        tirodisparo;



    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody= GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se o player está tocando o chão
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        playerAnimator.SetBool("isGrounded", isGrounded);

        //Pega a direção que o personagem está
        direcao = Input.GetAxis("Horizontal");

        ExecutaMovimentos();

        //Verifica se o espaço foi apertado considerando o Axys e se o player está no chão
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1") && tirodisparo == false)
        {
            if(playerRigidbody.velocity.x != 0 && isGrounded)
            {
                Atirar();
            }
        }

    }

    void FixedUpdate()
    {
        MovePlayer(direcao);

        if(jump)
        {
            JumpPlayer();
        }
    }

    void JumpPlayer()
    {
        if (isGrounded)
        {
            numberJumps = 0;
        }

        if(isGrounded || numberJumps < maxJumps)
        {
        //Adiciona a força no eixo Y no rigidbody do player
        playerRigidbody.AddForce(new Vector2(0f, jumpForce));
        numberJumps++;
        isGrounded = false; 
        }
        
        jump = false;
    }

    void Flip()
    {
        //Transforma a condição contrária e muda a direção do objeto no eixo X
        facingRight = !facingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        velocidadetiro *= -1;
        transform.localScale = theScale;
    }

    void ExecutaMovimentos()
    {   
        playerAnimator.SetFloat("velocidadeY", playerRigidbody.velocity.y);
        
        playerAnimator.SetBool("Jump", !isGrounded);
        
        //Executa a animação Run caso o eixo "x" seja maior que 1 e a função isGrounded seja verdadeira
        playerAnimator.SetBool("Run", playerRigidbody.velocity.x != 0f && isGrounded );
        
    }

    void MovePlayer(float movimentoH)
    {   
        //Pega o valor do eixo X e multiplica pela velocidade para definir a velocidade da movimentação. Faz a condicional que se o eixo x foi menor que zero e a 
        //condição do faceinright for contrária, ele ativa a função Flip
        playerRigidbody.velocity = new Vector2(movimentoH * speed, playerRigidbody.velocity.y);

        if(movimentoH < 0 && facingRight || movimentoH > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Atirar()
    {
        tirodisparo = true;

        //chamando a função que cria o delay
        StartCoroutine("tempotiro");
        //Instancia uma variável temporária que pega o prefab da bullet e coloca na mesma posição que o objeto bala
        GameObject temp = Instantiate(tiroprefab);
        temp.transform.position = bala.position;

        if(facingRight == false)
        {
        temp.GetComponent<SpriteRenderer>().flipX = true;
        }

        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadetiro, 0);

    }

    //Função que cria o delay entre os tiros
    IEnumerator tempotiro()
    {
        yield return new WaitForSeconds(delaytiro);
        tirodisparo = false;
    }
}
