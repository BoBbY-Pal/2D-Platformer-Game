using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   public Animator animator;
    public float speed;
    public float jump;
    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D _capsuleCollider2d;
    [SerializeField] private LayerMask platformLayerMask;

    void Awake() {
        _capsuleCollider2d = gameObject.GetComponent<CapsuleCollider2D>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {   //Detecting user inputs
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");

        //Function calling
        PlayerMovementAnimation(horizontal, vertical);
        MoveCharacter(horizontal, vertical);
    }

    private void MoveCharacter(float horizontal,float vertical)
    {  //Move character horizotally
       Vector3 position = transform.position;
       position.x += horizontal * speed * Time.deltaTime;
       transform.position = position;

       //Jump
        if(isPlayerGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            _rigidbody2D.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        }
    }
    private bool isPlayerGrounded() 
    {   
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(_capsuleCollider2d.bounds.center, _capsuleCollider2d.bounds.size, 0f, Vector2.down, .1f, platformLayerMask);
        Debug.Log(raycasthit2d.collider);
        return raycasthit2d.collider != null;
    }
    private void PlayerMovementAnimation(float horizontal,float vertical)   
    {   //Horizontal Movement Animation
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Vector3 scale = transform.localScale;
        if(horizontal < 0) {
            scale.x = -1f * Mathf.Abs(scale.x);
        } 
        else if(horizontal > 0) {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        //JUMP
        if(isPlayerGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        };
    }
}
