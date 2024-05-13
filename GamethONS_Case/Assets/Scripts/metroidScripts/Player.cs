/*
 * Dash, Coyote Time, Jump Buffer, Wall Jump, Wall Slide vieram deste canal:
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

    public bool _isAlive { private set; get; } = true;
    public bool _canMove = true;
    private bool _canDash = true;
    public bool _isDashing { private set; get; }
    private const float DashPower = 24f;
    private const float DashTime = .2f;
    private const float DashCooldown = .2f;

    private bool _doubleJump;
    private bool _jump;

    public bool _isWallSliding { private set; get; }
    private const float WallSlidingSpeed = 2f;

    public bool _isWallJumping { private set; get; }
    private float _wallJumpingDirection;
    private const float WallJumpingTime = .2f;
    private float _wallJumpingCounter;
    private const float WallJumpingDuration = .2f;
    private readonly Vector2 _wallJumpingPower = new(Speed, JumpPower);


    public float _horizontal { private set; get; }
    public float Vertical { private set; get; }
    private bool _isFacingRight = true;

    public bool _isJumping { private set; get; }
    private const float JumpCooldown = .4f;
    
    private float _coyoteTimer;
    private const float CoyoteTimerMax = .2f;

    private float _jumpBufferTimer;
    private const float JumpBufferTimerMax = .2f;
    
    [SerializeField] private bool hasDash; // Player possui a habilidade dash
    [SerializeField] private bool has2Jump; // Player possui a habilidade pulo duplo
    [SerializeField] private bool hasWallJump; // Player possui a habilidade pulo na parede
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private TrailRenderer trailRenderer;
    
    
    private void Start()
    {
        vidaAtual = vidaMax;
    }

    private void Update()
    {
        if(_isAlive && _canMove)
            Movement();
    }

    
    private void Movement()
    {
        if (_isDashing) return;
        
        _horizontal = Input.GetAxisRaw("Horizontal");
        if (!_isWallJumping)
        {
            rb.velocity = new Vector2(_horizontal * Speed, rb.velocity.y);
            if (Time.timeScale != 0 && (_isFacingRight && _horizontal < 0f) || (!_isFacingRight && _horizontal > 0f))
                Flip();
        }
        Jump();
        WallSlide();
        WallJump();
        // Dash --
        if (hasDash && Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Co_Dash());
        }
        // -- Dash

        Vertical = rb.velocity.y == 0 ? 0 : rb.velocity.y < 0 ? -1 : 1;
    }

    private void Jump()
    {
        if (IsGrounded()) _coyoteTimer = CoyoteTimerMax;
        else _coyoteTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump")) _jumpBufferTimer = JumpBufferTimerMax;
        else _jumpBufferTimer -= Time.deltaTime;

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            _doubleJump = false;
            _jump = false;
        }
        if (!IsGrounded() && !_doubleJump && !_jump) _doubleJump = true;
        
        if (_jumpBufferTimer > 0f) // Equivalente a if (Input.GetButtonDown("Jump"))
        {
            if ((_coyoteTimer > 0f && !_isJumping && Time.timeScale > 0f)
                || (_doubleJump && has2Jump))
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpPower);

                _jumpBufferTimer = 0f;
                _doubleJump = !_doubleJump;
                _jump = true;

                StartCoroutine(Co_JumpCooldown());
            }
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
    }
    
    private void WallSlide()
    {
        if (!hasWallJump) return;
        
        if (IsWalled() && !IsGrounded() && _horizontal != 0f)
        {
            _isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x,
                Mathf.Clamp(rb.velocity.y, -WallSlidingSpeed, float.MaxValue));
        }
        else
        {
            _isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (!hasWallJump) return;
        
        if (_isWallSliding)
        {
            _isWallJumping = false;
            // localScale.x é -1 se player estiver olhando para a esquerda, +1 para a direita
            _wallJumpingDirection = -transform.localScale.x;
            _wallJumpingCounter = WallJumpingTime;
            
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            _wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && _wallJumpingCounter > 0f)
        {
            _isWallJumping = true;
            rb.velocity = new Vector2(_wallJumpingDirection, 1) * _wallJumpingPower;
            _wallJumpingCounter = 0f;
            
            if (Time.timeScale != 0 && transform.localScale.x != _wallJumpingDirection)
                Flip();
            
            Invoke(nameof(StopWallJumping), WallJumpingDuration);
            _doubleJump = !_doubleJump;
        }
    }

    private void StopWallJumping()
    {
        _isWallJumping = false;
    }
    
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public bool IsGrounded()
    {
        const float radius = .2f;
        return Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);
    }

    private bool IsWalled()
    {
        const float radius = .2f;
        return Physics2D.OverlapCircle(wallCheck.position, radius, wallLayer);
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

        yield return new WaitUntil(IsGrounded); // Dash só reseta qnd pisa no chão
        yield return new WaitForSeconds(DashCooldown);
        _canDash = true;
    }
    
    public void AplicaDano(int dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMax);
        Debug.Log("Chegamo aqui" + dano);   
        if( vidaAtual <= 0)
            Morrer();
        
    }

    public void GetPowerUp(PowerUps powerUp)
    {
    }

    [Serializable]
    public enum PowerUps
    {
    }


    public void Morrer()
    {
        _isAlive = false;
        StartCoroutine(LevelManager.Instance.EndGame());
    }

    public void StopMovemnt()
    {
        _canMove = false;
        rb.velocity = new Vector2(0,0);
    }
}
