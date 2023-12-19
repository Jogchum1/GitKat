using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamageable
{
    public int maxHealth;
    int currentHealth;

    public int damageAmount;
    public float attackRate;
    float nextAttackTime = 0f;
    //public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public GameObject bullet;
    //public GameObject sword;
    public GameObject respawnPoint;

    public GameManager gameManager;
    public float dieTime;
    private Door door;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        currentHealth = maxHealth;
        //sword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public void Attack()
    {
        //sword.SetActive(true);
        //StartCoroutine("ToggleSword");
        //Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);
        //foreach (Collider2D enemy in enemiesInRange)
        //{
        //    //Debug.Log(enemy.name);
        //    enemy.GetComponent<IDamageable>().TakeDamage(damageAmount);
        //}

    }
    public void Shoot()
    {
        //GameObject tmpBullet;
        //tmpBullet = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Played damaged");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    [ContextMenu("Die")]
    public void Die()
    {
        Debug.Log("Player died");
        StartCoroutine(Dying());

    }

    private IEnumerator ToggleSword()
    {
        yield return new WaitForSeconds(.1f);
        //sword.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Death")
        {
            Die();
        }

        if (collision.tag == "Respawn")
        {
            door = collision.GetComponentInParent<Door>();
            //respawnPoint.transform.position = door.goalPos;
            respawnPoint.transform.SetPositionAndRotation(door.goalDoor.goalPos, Quaternion.identity);
        }
    }

    private IEnumerator Dying()
    {
        gameManager.TogglePlayerMovement();
        gameManager.StopPlayerVelocity();

        float duration = dieTime / 3;
        yield return door.TransitionScreen(Color.clear, Color.black, duration);

        gameObject.transform.position = respawnPoint.transform.position;
        gameManager.camManager.currentCamera.ForceCameraPosition(respawnPoint.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(dieTime / 3);

        yield return door.TransitionScreen(Color.black, Color.clear, duration);

        gameManager.TogglePlayerMovement();
    }


}
