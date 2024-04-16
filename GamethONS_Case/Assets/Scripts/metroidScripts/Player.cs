/*
 * Dash, Coyote Time, Jump Buffer vieram deste canal:
 * https://www.youtube.com/@bendux
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public int vidaMax = 10;
    public int vidaAtual;
    
    private const float Speed = 8f;
    private const float JumpPower = 16f;

    private bool _canDash = true;
    private bool _isDashing;
    private const float DashPower = 24f;
    private const float DashTime = .2f;
    private const float DashCooldown = 1f;
    
    private float _horizontal;
    private bool _isFacingRight = true;

    private bool _isJumping = false;
    private const float JumpCooldown = .4f;
    
    private float _coyoteTimer;
    private const float CoyoteTimerMax = .2f;

    private float _jumpBufferTimer;
    private const float JumpBufferTimerMax = .2f;
    
    [SerializeField] private bool hasDash; // Player possui a habilidade dash
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer trailRenderer;
    
    
    private void Start()
    {
        vidaAtual = vidaMax;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    
    private void Movement()
    {
        if (_isDashing) return;
        
        _horizontal = Input.GetAxisRaw("Horizontal");

        // Pulo --
        if (IsGrounded()) _coyoteTimer = CoyoteTimerMax;
        else _coyoteTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump")) _jumpBufferTimer = JumpBufferTimerMax;
        else _jumpBufferTimer -= Time.deltaTime;
        
        if (_jumpBufferTimer > 0f && _coyoteTimer > 0f && !_isJumping && Time.timeScale > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);

            _jumpBufferTimer = 0f;

            StartCoroutine(Co_JumpCooldown());
        }
        
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            // O tanto que vai diminuir da velocidade.y, para q o pulo seja maior quando se segura o botao de pulo.
            const float lowerVSpeed = .5f;
            Vector2 velocity = rb.velocity;
            velocity.y *= lowerVSpeed;
            rb.velocity = velocity;

            _coyoteTimer = 0f;
        }
        // -- Pulo
        
        rb.velocity = new Vector2(_horizontal * Speed, rb.velocity.y);
        Flip();
        
        // Dash --
        if (hasDash && Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Co_Dash());
        }
        // -- Dash
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

    private IEnumerator Co_JumpCooldown()
    {
        _isJumping = true;
        yield return new WaitForSeconds(JumpCooldown);
        _isJumping = false;
    }

    private IEnumerator Co_Dash()
    {
        _canDash = false;
        _isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        // localScale.x é -1 se player estiver olhando para a esquerda, +1 para a direita
        rb.velocity = new Vector2(transform.localScale.x * DashPower, 0f);
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(DashTime);
        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        _isDashing = false;

        yield return new WaitForSeconds(DashCooldown);
        _canDash = true;
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
    }

    [Serializable]
    public enum PowerUps
    {
    }
}
