using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : MonoBehaviour
{
    SpriteRenderer sr;

    [Header("HEALTH VALUE")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] float health = 100;

    [SerializeField] bool isBoss = false;
    [SerializeField] Image hpSlider;
    [SerializeField] AudioClip hitSfx;
    [SerializeField] GameObject deathParticle;
    [SerializeField] AudioClip deathSfx;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (isBoss)
        {
            hpSlider.fillAmount = health / maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        SoundSystem.Instance.PlayOneShot(hitSfx);

        if (health <= 0)
            Die();
    }

    public void Heal(int heal)
    {
        health += heal;
    }

    [ContextMenu("KILL")]
    void Die()
    {
        print("Enemy Died");
        if (isBoss)
        {
            GameController.Instance.gameEnd = true;
        }
        else
        {
            GameController.Instance.AddKillCount();
            Player_Health.Instance.AddExp(3);
            SoundSystem.Instance.PlayOneShot(deathSfx);
            ObjectPooler.Instance.SpawnFromPool("deathParticle", transform.position, Quaternion.identity);
            //Destroy(Instantiate(deathParticle, transform.position, Quaternion.identity, GameController.Instance.sack), 2f);
            gameObject.SetActive(false);
        }
    }
}
