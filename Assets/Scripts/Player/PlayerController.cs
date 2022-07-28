using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{  
    //   For health function 
    public int currentHealth;
    public int maxHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    
    //---------------------------------------------------
    private Animator _playerAnimator;
    public float speed;
    private bool isCrouched;
    public float jump;
    private bool isDead;
    
    private AudioSource _footstep;
    public GameOverUI gameOverUI;
    public ScoreController scoreController;
    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D _capsuleCollider2d;
    [SerializeField] private LayerMask platformLayerMask;

    // Input axis
    private float _vertical;
    private float _horizontal;
    
    void Awake()
    {
        currentHealth = maxHealth;
        
        _footstep = GetComponent<AudioSource>();
        _playerAnimator = GetComponent<Animator>();
        _capsuleCollider2d = gameObject.GetComponent<CapsuleCollider2D>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        
        isDead = false;
        
        SoundManager.Instance.PlayEnvironmentMusic(SoundTypes.EnvironmentalAmbiance);
    }

    private void Start()
    {
        ShowLives();
    }

    void Update()
    {   
        if(isDead)
        {
            return;
        }
        
        PlayerInputs();
        

        //  Setting speed to zero so that player can't move while crouching.
        if(isCrouched) {
            _horizontal = 0;
        }
        
        PlayerMovementAnimation(_horizontal, _vertical);
        MoveCharacter(_horizontal, _vertical);

        //  Changing isGrounded parameter in animator.
        if(IsPlayerGrounded()) { _playerAnimator.SetBool("isGrounded", true ); }
        else { _playerAnimator.SetBool("isGrounded", false ); }
        
        // DamagePlayerHealth();
    }

    

    // private void DamagePlayerHealth()
    // {
        
    // }
    private void Footstep()
    {
        _footstep.Play();
    }

    #region MOVEMENT
    private void PlayerInputs()
    {
        //  Detecting user inputs
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Jump");
    }
    
    private void PlayerMovementAnimation(float horizontal,float vertical)   
    {   
        //  Horizontal Movement Animation
        _playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
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
            _playerAnimator.SetTrigger("Jump");
            SoundManager.Instance.Play(SoundTypes.PlayerJump);
        }

        //  Crouch
        if(IsPlayerGrounded() && Input.GetKeyDown(KeyCode.C)) {
            _playerAnimator.SetBool("Crouch", true);
            isCrouched = true;

        }
        else if(Input.GetKeyUp(KeyCode.C)) {
            _playerAnimator.SetBool("Crouch", false);
            isCrouched = false;
        }
    }
    
    private void MoveCharacter(float horizontal,float vertical)
    {  
        //   Move character horizontally

        var transform1 = transform;
        Vector3 position = transform1.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform1.position = position;
       
       //   Jump
        if(IsPlayerGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            _rigidbody2D.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
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
    #endregion

    public void Hurt()
    {
        _playerAnimator.SetTrigger("ItHurts");
        currentHealth--;
        ShowLives();

        if (currentHealth > 0) return;
        Died();
    }
    
    public void Died() 
    {   
        if(isDead) 
            return;
        
        currentHealth = 0;
        Debug.Log("Player killed by enemy");
        _playerAnimator.SetTrigger("Death");
        _playerAnimator.SetBool("IsDead", true);
        isDead  = true;

        ShowLives();
        StartCoroutine(gameOverUI.GameOver());
    }

    private void ShowLives()
    {   
        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;

            hearts[i].enabled = i < maxHealth;
        }
    
    }
    
    
    public void KeyPickUp() 
    {
        Debug.Log("Key picked  up");
        SoundManager.Instance.Play(SoundTypes.Pickup);
        scoreController.IncreaseScore(50);
    }
    
}
