using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [Header("Health")]
    private float startingHealth = 10;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    public GameObject lost;
    public GameObject win;
    public GameObject enemy1;
    public GameObject enemy2;

    private float x;
    private float y;
    private bool h;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;  
    [SerializeField] private int numberOfFlashes;  
    private SpriteRenderer spriteRend;  
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();


    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);  
         
        if (currentHealth > 0.01)
        {
            StartCoroutine(Invunerability()); 
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerAttack>().enabled = false;
                dead = true;
                lost.SetActive(true);
            }
        }
    }
    public void AddHealth(float _value)
    {
        anim.SetTrigger("health");
        h = true;
        y = _value;
    }
    private void Update()
    {
        if (enemy1.GetComponent<HealthEnemy>().currentHealth == 0)
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            win.SetActive(true);
        }

        if (h == true)
        {
            x += 0.01f;
            if (x < y)
            {
                currentHealth = Mathf.Clamp(currentHealth + 0.01f, 0, startingHealth);
            }
            else
            {
                x = 0;
                h = false;
            }
        }
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}
