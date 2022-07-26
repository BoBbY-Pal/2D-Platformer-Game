using UnityEngine;

public class PatrolingEnemy : MonoBehaviour
{   
    public float speed;
    public float distance;
    private bool b_movingRight = true;
    public Transform groundDetection;
    
    void Update()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);    
        
        if(groundInfo.collider == false) 
        {
            if(b_movingRight) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                b_movingRight = false;
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                b_movingRight = true;
            }
        }
    }
}
