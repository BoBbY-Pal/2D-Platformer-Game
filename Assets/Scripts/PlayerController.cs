using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   public Animator animator;
    public float speed;
    public float jump;
    Rigidbody2D rb2d;
    void Awake() {
        
        rb2d = gameObject.GetComponent<Rigidbody2D>();
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
        if(vertical > 0) {
            rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Force);
        }
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
        if(vertical > 0)
        {
            animator.SetTrigger("Jump");
        };
    }
}
