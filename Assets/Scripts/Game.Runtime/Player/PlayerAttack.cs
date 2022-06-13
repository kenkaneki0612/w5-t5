using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackCD;
    private float cdTimer;
    private Animator anim;
    
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float range; 
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] GameObject atkBuff;
    [SerializeField] GameObject atkBuffUI;
    private HealthEnemy enemyHealth;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && cdTimer > attackCD)
        {
            Attack();

        }
        EnemyInSight();
        cdTimer += Time.deltaTime;
    }
    private void Attack()
    {
        anim.SetTrigger("attack");

        cdTimer = 0;

    }
    private bool EnemyInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
        , 0, Vector2.left, 0, enemyLayer); 
        if (hit.collider != null)
            enemyHealth = hit.transform.GetComponent<HealthEnemy>();
        return hit.collider != null;

    }
    private void DamageEnemy()
    {
        if (EnemyInSight())
            enemyHealth.TakeDamage(damage);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "atkBuff")
        {
            damage ++;
            atkBuff.SetActive (false);
            atkBuffUI.SetActive(true);

        }
    }
}
