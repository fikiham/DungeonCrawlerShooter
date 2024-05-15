using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [HideInInspector] float bulletSpd;
    [HideInInspector] float damage;

    bool playerHitter = false;

    // Update is called once per frame
    void Update()
    {
        transform.position += bulletSpd * Time.deltaTime * -transform.right;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && !playerHitter)
        {
            collision.GetComponent<Enemy_Health>().TakeDamage((int)damage);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

        if (playerHitter && collision.gameObject.layer == 3)
        {
            collision.GetComponent<Player_Health>().TakeDamage((int)damage);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

        switch (collision.gameObject.layer)
        {
            case 7:
                gameObject.SetActive(false);
                //Destroy(gameObject);
                break;
            default: break;

        }
    }

    public void SetBullet(float bulletSpd, float damage, bool playerHitter = false)
    {
        this.bulletSpd = bulletSpd;
        this.damage = damage;
        this.playerHitter = playerHitter;
    }
}
