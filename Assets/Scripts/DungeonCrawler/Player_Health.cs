using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public static Player_Health Instance;
    Rigidbody2D rb;

    [Header("HEALTH VALUE")]
    public float maxHealth = 100;
    public float health = 100;
    [SerializeField] AudioClip hitSfx;
    Color hitColor = Color.red;
    Color defColor = Color.green;
    bool ableDamaged = true;
    float iframeDur = .5f;
    float iframeTimer;

    [Header("STAMINA VALUE")]
    [SerializeField] float maxStamina = 100;
    [SerializeField] float stamina = 100;
    [SerializeField] float staminaRegenRate = 15;

    [Header("EXP VALUE")]
    [SerializeField] float requiredExp = 10;
    [SerializeField] float currentExp = 0;
    public int Level = 1;
    [SerializeField] AudioClip gainExpSfx;

    [Header("UI")]
    [SerializeField] Image hpSlider;
    [SerializeField] Image staminaSlider;
    [SerializeField] Image expSlider;



    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (stamina < maxStamina)
        {
            stamina = Mathf.MoveTowards(stamina, maxStamina, staminaRegenRate * Time.deltaTime);
        }

        HandleUI(hpSlider, maxHealth, health);
        HandleUI(staminaSlider, maxStamina, stamina);
        HandleUI(expSlider, requiredExp, currentExp);

        if (currentExp >= requiredExp)
            LevelUp();

        if (!ableDamaged)
        {
            iframeTimer += Time.deltaTime;
            if (iframeTimer > iframeDur)
            {
                ableDamaged = true;
                iframeTimer = 0;
            }
        }
    }

    void HandleUI(Image image, float max, float current)
    {
        image.fillAmount = current / max;
    }

    public void TakeDamage(int damage)
    {
        if (ableDamaged)
        {
            health -= damage;
            SoundSystem.Instance.PlayOneShot(hitSfx);
            StartCoroutine(DamageTaken());
            ableDamaged = false;
        }

        if (health <= 0)
            Die();
    }

    IEnumerator DamageTaken()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float startTime = 0;

        while (true)
        {
            startTime += Time.deltaTime;
            if (startTime > .5f)
                break;

            sr.color = Color.Lerp(hitColor, defColor, startTime / .5f);
            yield return null;
        }


    }

    public void Heal(int heal)
    {
        health += heal;
    }

    public bool SpendStamina(float exhaust)
    {
        if (exhaust > stamina)
        {
            return false;
        }
        else
        {
            stamina -= exhaust;
            return true;
        }
    }

    [ContextMenu("KILL")]
    void Die()
    {
        print("Player Died");
        GameController.Instance.playerDied = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            currentExp += 10;
            SoundSystem.Instance.PlayOneShot(gainExpSfx);
            collision.gameObject.SetActive(false);
            Exp_Spawner.Instance.ShouldSpawn = true;
        }
    }

    void LevelUp()
    {
        Level++;
        currentExp -= requiredExp;
        requiredExp += 5;
        GameController.Instance.IncreasingLevel();
    }

    public void AddExp(int exp)
    {
        currentExp += exp;
    }
}
