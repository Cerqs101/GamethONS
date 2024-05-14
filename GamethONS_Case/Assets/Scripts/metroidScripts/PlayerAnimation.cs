using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;

    private string _currentState;
    private const string PlayerIdle = "Player_Idle";
    private const string PlayerWalk = "Player_Walk";
    private const string PlayerJump = "Player_Jump";
    private const string PlayerFall = "Player_Fall";
    private const string PlayerDash = "Player_Dash";
    private const string PlayerDed = "Player_Ded";
    private const string PlayerOnWall= "Player_OnWall";


    private void Update()
    {
        if (player.IsWallSliding)
        {
            ChangeAnimationState(PlayerOnWall);
            Vector3 ls = transform.localScale;
            ls.x = ls.x < 0 ? ls.x : -ls.x;
            transform.localScale = ls;
            return;
        }
        else
        {
            Vector3 ls = transform.localScale;
            ls.x = ls.x < 0 ? -ls.x : ls.x;
            transform.localScale = ls;
        }
        
        
        if (!player.IsAlive)
        {
            ChangeAnimationState(PlayerDed);
            return;
        }


        if (player.IsGrounded() && player.Horizontal == 0)
        {
            ChangeAnimationState(PlayerIdle);
            return;
        }

        if (player.IsGrounded() && player.Horizontal != 0 && !player.IsDashing)
        {
            ChangeAnimationState(PlayerWalk);
            return;
        }

        if (player.IsDashing)
        {
            ChangeAnimationState(PlayerDash);
            return;
        }

        if (player.Vertical < 0 && !player.IsDashing && !player.IsWallSliding)
        {
            ChangeAnimationState(PlayerFall);
            return;
        }

        if (player.Vertical > 0 && !player.IsGrounded())
        {
            ChangeAnimationState(PlayerJump);
            return;
        }
    }


    private void ChangeAnimationState(string newState)
    {
        if (newState == _currentState) return;

        animator.Play(newState);
        _currentState = newState;
    }

    bool IsAnimationPlaying(Animator animator, string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f;
    }
}
