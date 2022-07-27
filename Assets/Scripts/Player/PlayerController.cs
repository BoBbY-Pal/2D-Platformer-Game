using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{  
    //   For health function 
    public int currentHealth;
    public int numOfHeart;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    //---------------------------------------------------

    private Animator _animator;
    public float speed;
    private bool isCrouched;
    public float jump;
    private bool isDead;
    
    private AudioSource footstep;
    public GameOverUI gameOverUI;
    public ScoreController scoreController;
    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D _capsuleCollider2d;
    [SerializeField] private LayerMask platformLayerMask;

    void Awake() 
    {
        footstep = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _capsuleCollider2d = gameObject.GetComponent<CapsuleCollider2D>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        
        isDead = false;
        // SoundManager.Instance.PlayEnvironmentMusic(Sounds.EnvironmentalAmbian);
    }

    void Update()
    {   
        if(isDead)
        {
            return;
        }
        //  Detecting user inputs
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        
        HealthHeart();

        //  Setting speed to zero so that player can't move while crouching.
        if(isCrouched) {
            horizontal = 0;
        }
        

        PlayerMovementAnimation(horizontal, vertical);
        MoveCharacter(horizontal, vertical);

        //  Changing isGrounded parameter in animator.
        if(IsPlayerGrounded()) { _animator.SetBool("isGrounded", true ); }
        else { _animator.SetBool("isGrounded", false ); }
        
        // DamagePlayerHealth();
    }

    // private void DamagePlayerHealth()
    // {
        
    // }
    private void Footstep()
    {
        footstep.Play();
    }

    private void MoveCharacter(float horizontal,float vertical)
    {  
        //   Move character horizontally
       
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;
       
       //   Jump
        if(IsPlayerGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            _rigidbody2D.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        }
    }
    

    public void PlayerDied() 
    {   
        if(isDead) 
            return;
        Debug.Log("Player killed by enemy");
        _animator.SetTrigger("Death");
        isDead  = true;
        SoundManager.Instance.Play(SoundTypes.MusicDeathSting);
        
        StartCoroutine(gameOverUI.GameOver());
    }

    public void KeyPickUp() 
    {
        Debug.Log("Key picked  up");
        SoundManager.Instance.Play(SoundTypes.Pickup);
        scoreController.IncreaseScore(50);
    }

    private void HealthHeart()
    {   
        if(currentHealth > numOfHeart) {
            currentHealth = numOfHeart;
        }
        
        for (int i = 0; i < hearts.Length; i++)
        {   
            if(i < currentHealth) 
            {
                hearts[i].sprite = fullHeart;    
            } 
            else 
                hearts[i].sprite = emptyHeart;

            if(i < numOfHeart ) 
            {
                hearts[i].enabled = true;
            } 
            else 
                hearts[i].enabled = false;
        }
    
    }
    
    private bool IsPlayerGrounded()         //  Checks player is on ground or not
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
            _animator.SetFloat("Speed", Mathf.Abs(horizontal));
            Vector3 scale = transform.localScale;
            if(horizontal < 0) {
                scale.x = -1f * Mathf.Abs(scale.x);
            } 
            else if(horizontal > 0) {
                scale.x = Mathf.Abs(scale.x);
            }
            transform.localScale = scale;
    
            //  JUMP
            if(IsPlayerGrounded() && Input.GetKeyDown(KeyCode.Space)) {
                _animator.SetTrigger("Jump");
            }

            //  Crouch
            if(IsPlayerGrounded() && Input.GetKeyDown(KeyCode.C)) {
                _animator.SetBool("Crouch", true);
                isCrouched = true;

            }
            else if(Input.GetKeyUp(KeyCode.C)) {
                _animator.SetBool("Crouch", false);
                isCrouched = false;
            }
    }
}
