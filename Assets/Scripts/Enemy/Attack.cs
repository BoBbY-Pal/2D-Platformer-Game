using System;
using System.Collections;
using UnityEngine;


public class Attack : MonoBehaviour
{
    private PlayerController _playerController;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D enemyCollision) 
    {  
        
        _playerController = enemyCollision.gameObject.GetComponent<PlayerController>();
        
        if (_playerController == null) return;

        _animator.SetTrigger("Attack");     // PLays chomper attack animation.
        SoundManager.Instance.Play(SoundTypes.ChomperAttack);

        StartCoroutine(HurtPlayer());
    }

    IEnumerator HurtPlayer()
    {
        yield return new WaitForSeconds(.5f);
        _playerController.Hurt();
    }
} 
