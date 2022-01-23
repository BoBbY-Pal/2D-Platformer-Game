using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{  
    public Animator animator;
    public ScoreScript scoreController;
    public float speed;
    private bool isCrouched;
    public float jump;
    public bool isDead;
    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D _capsuleCollider2d;
    [SerializeField] private LayerMask platformLayerMask;

    void Awake() {
        _capsuleCollider2d = gameObject.GetComponent<CapsuleCollider2D>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        isDead = false;
    }

    void Update()
    {   
        if(isDead)
            return;
        //  Detecting user inputs
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        
        //  Setting speed to zero so that player can't move while crouching.
        if(isCrouched == true) {
            horizontal = 0;
        }
        //Function calling
        PlayerMovementAnimation(horizontal, vertical);
        MoveCharacter(horizontal, vertical);

        //  Changing isGrounded parameter in animator.
        if(isPlayerGrounded()) { animator.SetBool("isGrounded", true ); }
        else { animator.SetBool("isGrounded", false ); }
        
        DamagePlayerHealth();
    }

    private void DamagePlayerHealth()
    {
        
    }

    private void MoveCharacter(float horizontal,float vertical)
    {  
        //   Move character horizotally
       
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;
       
       //   Jump
        if(isPlayerGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            _rigidbody2D.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        }
    }
    
    public void ReloadScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void KillPlayer() 
    {   
        Debug.Log("Player killed by enemy");
        animator.SetTrigger("Death");
        isDead  = true;
    }

    public void PickUpKey() 
    {
        Debug.Log("Key picked  up");
        scoreController.IncreaseScore(5);
    }
    private bool isPlayerGrounded()         //  Checks player is on ground or not
    {   
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(_capsuleCollider2d.bounds.center, _capsuleCollider2d.bounds.size, 0f, Vector2.down, .1f, platformLayerMask);
        Color rayColor;
        if(raycastHit2d.collider != null) {
            rayColor = Color.cyan;
        } else {
            rayColor = Color.red;
        }
        Debug.DrawRay(_capsuleCollider2d.bounds.center + new Vector3(_capsuleCollider2d.bounds.extents.x, 0), Vector2.down * (_capsuleCollider2d.bounds.extents.y + .1f), rayColor);
        Debug.DrawRay(_capsuleCollider2d.bounds.center - new Vector3(_capsuleCollider2d.bounds.extents.x, 0), Vector2.down * (_capsuleCollider2d.bounds.extents.y + .1f), rayColor);
        Debug.DrawRay(_capsuleCollider2d.bounds.center - new Vector3(_capsuleCollider2d.bounds.extents.x, _capsuleCollider2d.bounds.extents.y + .1f), Vector2.right * (_capsuleCollider2d.bounds.extents.x), rayColor);
        Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }

    private void PlayerMovementAnimation(float horizontal,float vertical)   
    {   //  Horizontal Movement Animation
            animator.SetFloat("Speed", Mathf.Abs(horizontal));
            Vector3 scale = transform.localScale;
            if(horizontal < 0) {
                scale.x = -1f * Mathf.Abs(scale.x);
            } 
            else if(horizontal > 0) {
                scale.x = Mathf.Abs(scale.x);
            }
            transform.localScale = scale;
    
        //  JUMP
            if(isPlayerGrounded() && Input.GetKeyDown(KeyCode.Space)) {
                animator.SetTrigger("Jump");
            }

        //  Crouch
            if(isPlayerGrounded() && Input.GetKeyDown(KeyCode.C)) {
                animator.SetBool("Crouch", true);
                isCrouched = true;

            }
            else if(Input.GetKeyUp(KeyCode.C)) {
                animator.SetBool("Crouch", false);
                isCrouched = false;
            }
    }
}
