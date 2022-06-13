using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;

    [SerializeField] private float rangeAttack;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;
    private Health playerHealth;

    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private bool stop;
    private bool move;
    private Vector3 destination; 
    private Vector3[] directions = new Vector3[4];

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        attackCooldown += Time.deltaTime;
        if (Attack())
        {
            if (cooldownTimer >= attackCooldown)
            {
                attackCooldown = 0;
                anim.SetTrigger("Attack_3");
            }
        }


        if (move & !stop)
        {
            transform.Translate(destination * Time.deltaTime * speed);           
        }
        CheckForPlayer();
        directions[1] = new Vector3(-transform.localScale.x, 0, 0) * range;
    }
    private bool Attack()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rangeAttack * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * rangeAttack, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        else
        {
            stop = false;
        }
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rangeAttack * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * rangeAttack, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void CheckForPlayer()
    {          

            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1f), directions[1], Color.red); 
            RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x,transform.position.y +1f), directions[1], range, playerLayer); 
         
            if (hit.collider != null && !move)
            {
                move = true;
                destination = directions[1];          

            }
            else
            {
                move = false;
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            stop = true;
        }
    }

    private void DamagePlayer()
    {
        if (Attack())
            playerHealth.TakeDamage(damage);
    }
}
