using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int vidaMax = 10;
    public int vidaAtual;
    
    private float horizontal;
    private bool isFacingRight = true;

    private bool hasPacoca = false;
    
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 16f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    
    private void Start()
    {
        vidaAtual = vidaMax;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            // O tanto que vai diminuir da velocidade.y, para q o pulo seja maior quando se segura o botao de pulo.
            const float lowerVSpeed = .5f;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * lowerVSpeed);
        }

        if (hasPacoca)
        {
            Vector3 ls = transform.localScale;
            ls.x = Mathf.Sign(ls.x) * 1.5f;
            transform.localScale = ls;
        }
        
        
        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (!(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)) return;
        
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private bool IsGrounded()
    {
        const float radius = .2f;
        return Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);
    }
    
    public void AplicaDano(int dano)
    {
        vidaAtual -= dano;
        Debug.Log("Chegamo aqui" + dano);   
        if( vidaAtual <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetPowerUp(PowerUps powerUp)
    {
        switch (powerUp)
        {
            case PowerUps.Pacoca:
                hasPacoca = true;
                Debug.Log("Pegou paçoca!");
                break;
        }
    }

    [Serializable]
    public enum PowerUps
    {
        Pacoca
    }
}
