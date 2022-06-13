using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private HealthEnemy enemyHealth;

    private void Update()
    {
        transform.localScale = new Vector3(enemyHealth.currentHealth * 1.3f / 5, transform.localScale.y);

    }
}

