using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int vidaMax = 10;
    public int vidaAtual;
    
    private float _horizontal;
    private bool _isFacingRight = true;
    
    private float _coyoteTimer;
    private const float CoyoteTimerMax = .2f;

    private float _jumpBufferTimer;
    private const float JumpBufferTimerMax = .2f;
    
    private const float Speed = 8f;
    private const float JumpForce = 16f;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    
    private void Start()
    {
        vidaAtual = vidaMax;
    }

    private void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_horizontal * Speed, rb.velocity.y);
    }

    private void Movement()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded()) _coyoteTimer = CoyoteTimerMax;
        else _coyoteTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump")) _jumpBufferTimer = JumpBufferTimerMax;
        else _jumpBufferTimer -= Time.deltaTime;
        
        if (_jumpBufferTimer > 0f && _coyoteTimer > 0f && Time.timeScale > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);

            _jumpBufferTimer = 0f;
        }
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            // O tanto que vai diminuir da velocidade.y, para q o pulo seja maior quando se segura o botao de pulo.
            const float lowerVSpeed = .5f;
            Vector2 velocity = rb.velocity;
            velocity = new Vector2(velocity.x, velocity.y * lowerVSpeed);
            rb.velocity = velocity;

            _coyoteTimer = 0f;
        }
        
        Flip();
    }
    
    private void Flip()
    {
        if (!(_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)) return;
        
        _isFacingRight = !_isFacingRight;
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
        }
    }

    [Serializable]
    public enum PowerUps
    {
    }
}
