using System.Collections;
using System.Collections.Generic;
using CharactersModels;
using UnityEngine;

public class InputController : MonoBehaviour
{
    #region Properties
    private Animator anim;
    private Rigidbody2D rb;

    public Location locate = new ();

    // Dữ liệu chuyền động ngang.
    [Header("Run")]
    [SerializeField] private float speed;
    [SerializeField] private string runAnimator = "Runing";
    private MoveCommand moveCmd;

    // Dữ liệu nhảy
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private string jumpAnimator = "Jumping";
    [SerializeField] private string jumpInit = "yVelocity";
    private JumpCommand jumpCmd;

    // Dữ liệu tấn công
    [Header("Attack")]
    [Header("Điểm tấn công")]
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private GameObject attackPoint1;
    [SerializeField] private GameObject attackPoint2;
    [Header("Sát thương")]
    [SerializeField] private float damage;
    [SerializeField] private float damage1;
    [SerializeField] private float damage2;
    [Header("Animator")]
    [SerializeField] private string attackAnimator;
    [SerializeField] private string attackAnimator1;
    [SerializeField] private string attackAnimator2;

    private MeleeAttack normalAttack;
    private MeleeAttack attackCombo1;
    private MeleeAttack attackCombo2;

    private Queue<MeleeAttack> skill = new () ;

    #endregion

    [SerializeField]
    private LayerMask enemy;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        locate = GetComponent<Player>().location;

        moveCmd = new MoveCommand
        {
            speed = speed,
            animator = runAnimator
        };

        jumpCmd = new JumpCommand
        {
            jumpForce = jumpForce,
            jumpAnimator = jumpAnimator
        };

        normalAttack = new MeleeAttack
        {
            attackPoint = attackPoint.GetComponent<AttackFeature>(),
            damage = damage,
            attackAnimator = attackAnimator
        };

        attackCombo1 = new MeleeAttack
        {
            attackPoint = attackPoint1.GetComponent<AttackFeature>(),
            damage = damage1,
            attackAnimator = attackAnimator1
        };

    }

    // Update is called once per frame
    void Update()
    {
        #region Move
        if (Input.GetKey(KeyCode.A))
        {
            moveCmd.Left(transform, anim);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveCmd.Right(transform, anim);
        }
        else moveCmd.Animated(anim);
        #endregion
        
        #region Jump
        // Tạo giá trị cho hoạt ảnh 
        anim.SetFloat(jumpInit, rb.velocity.y);

        // Lệnh nhảy và animation nhảy
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            jumpCmd.Execute(rb, anim);
        }  
        else if (rb.velocity.y == 0f)
        {
            anim.SetBool(jumpAnimator,false);
        }
        #endregion

        UpdateLocate();

        #region Attack
        if (Input.GetKeyDown(KeyCode.Q))
        {
            skill.Enqueue(normalAttack);
        }

        if (Input.GetKeyDown (KeyCode.W)) 
        {
            skill.Enqueue(attackCombo1);
        }

        StartCoroutine(SkillExecute());
        #endregion
    }

    public void UpdateLocate(){
        locate.LocationSet(transform.position.x,transform.position.y,transform.position.z);
    }


    private IEnumerator SkillExecute()
    {
        while (skill.Count > 0)
        {
            MeleeAttack attack = skill.Dequeue();
            attack.Execute(anim, enemy);
            yield return new WaitForSeconds(2);
        }
    }
}

public class MoveCommand
{
    public float speed;
    public bool isFacingRight = true;
    public string animator;

    public void Animating(Animator anim)
    {
        anim.SetBool(animator, true);
    }

    public void Animated(Animator anim)
    {
        anim.SetBool(animator,false);
    }

    public void Right(Transform tf, Animator anim)
    {
        if (!isFacingRight) Flip(tf);
        Animating(anim);
        tf.position += speed * Time.deltaTime * Vector3.right ;
    }

    public void Left(Transform tf, Animator anim)
    {
        if (isFacingRight) Flip(tf);
        Animating(anim);
        tf.position += speed * Time.deltaTime * Vector3.left ;
    }

    public void Flip(Transform tf)
    {
        isFacingRight = !isFacingRight;
        tf.Rotate(0f, 180f, 0f);
    }
}

public class JumpCommand 
{
    public float jumpForce;
    public string jumpAnimator;

    public void Execute(Rigidbody2D rb, Animator anim)
    {
        Animating(anim);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Animating(Animator anim)
    {
        anim.SetBool(jumpAnimator, true);
    }
}

public class MeleeAttack 
{
    public AttackFeature attackPoint;
    public float damage;
    public string attackAnimator;

    public void Execute(Animator anim, LayerMask r)
    {
        Collider2D[] enemyhit = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackPoint.attackRange, r);
        Animating(anim);
        foreach (Collider2D hit in enemyhit)
        {
            hit.GetComponent<Enemy>().healthModel.curHealth -= 10;
        }
    }

    public void Animating(Animator anim) 
    { 
        anim.SetTrigger(attackAnimator);
    }
}

