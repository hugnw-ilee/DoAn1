using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Hành vi di chuyển")]
    [SerializeField] private float speed;
    [SerializeField] private float rayDist;
    [SerializeField] private Transform groundDetect;

    [Header("Hành vi tấn công")]
    [SerializeField] private Transform sightPoint;
    [SerializeField] private float sightDist;
    private bool isMovingLeft = true;
    private Animator anim;

    private bool detecter = false;
    private Vector2 rightSight = new (1f,-1f);
    private Vector2 leftSight = new (-1f,-1f);

    [SerializeField] private GameObject flame;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    void Update(){
        MovingBehavior(detecter);
        if(isMovingLeft) AttackBehavior(leftSight);
        else AttackBehavior(rightSight);
    }
    
    private void MovingBehavior(bool detectPlayer){
        if(!detectPlayer){
            transform.Translate(speed * Time.deltaTime * Vector2.left);
            RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, rayDist);

            if (!groundCheck.collider){
                isMovingLeft = !isMovingLeft;
                transform.Rotate(0f,180f,0f);
            }
        }
    }


    private void AttackBehavior(Vector2 vector)
    {
    // Giả sử sightPoint là một Transform và vector là Vector2, sightDist là khoảng cách ray
        int ignoreRaycast = ~ ( 1 << LayerMask.NameToLayer("Enemy"));
        RaycastHit2D playerCheck = Physics2D.Raycast(sightPoint.position, vector, sightDist, ignoreRaycast);

        // Kiểm tra tia ray 
        Debug.DrawRay(sightPoint.position, vector * sightDist, Color.red);

        if (playerCheck.collider != null)
        {
            if (playerCheck.collider.CompareTag("Player"))
            {
                detecter = true;
                anim.SetBool("BlendAttack", true);
                flame.SetActive(true);
            }
        }else ResetDetection();
    }

    void ResetDetection()
    {
        flame.SetActive(false);
        anim.SetBool("BlendAttack", false);
        anim.SetFloat("Blend", 0);
        detecter = false;
    }

    public void Attack1(){
        anim.SetBool("Attack2", true);
    }

    public void Blend(){
        anim.SetFloat("Blend",1);
    }

}
