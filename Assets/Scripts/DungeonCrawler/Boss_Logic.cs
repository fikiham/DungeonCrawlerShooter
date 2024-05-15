using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Boss_Logic : MonoBehaviour
{
    [Header("MOVING")]
    [SerializeField] Transform[] spots;
    bool goingLeft;
    [SerializeField] float moveDestinationDur = 5;
    float moveTimer;

    [Header("AIMING")]
    [SerializeField] Transform spiralingAim;
    [SerializeField] float spinMult = 1;
    [SerializeField] Transform playerAim;
    Transform player;


    [Header("SHOOTING")]
    [SerializeField] float bulletSpd = 10;
    [SerializeField] float bulletdamage = 5;
    [SerializeField] float shootingsDelay = 20;
    bool doneShooting = true;
    float shootingsTimer;
    [SerializeField] float shootDelay = .5f;
    [SerializeField] float shootDur = 5;
    float shootTimer;

    [Header("LASER")]
    [SerializeField] AudioClip laserSfx;
    [SerializeField] GameObject targetLaser;
    [SerializeField] GameObject actualLaser;
    [SerializeField] float laserDamage = 15;
    [SerializeField] float laserDelay = 15;
    [SerializeField] float laserAimDur = 3;
    [SerializeField] float laserDur = 2;
    bool doneLaser = true;
    float lasersTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lasersTimer = Time.time + laserDelay;
        shootingsTimer = Time.time + shootingsDelay;
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += (goingLeft ? -1 : 1) * Time.deltaTime;
        if (moveTimer >= moveDestinationDur)
            goingLeft = true;
        if (moveTimer <= 0)
            goingLeft = false;
        transform.position = Vector2.Lerp(spots[0].position, spots[1].position, moveTimer / moveDestinationDur);


        // Shooting Spiral
        HandleSpiralAim();
        if (Time.time > shootingsTimer && doneShooting)
        {
            doneShooting = false;
            StartCoroutine(Shooting(Time.time));
        }

        // Shooting Laser
        HandlePlayerAim();
        if (Time.time > lasersTimer && doneLaser)
        {
            doneLaser = false;
            StartCoroutine(Lasering(Time.time));
        }


    }

    void HandleSpiralAim()
    {
        spiralingAim.RotateAround(transform.position, Vector3.forward, spinMult * Time.deltaTime);
    }
    IEnumerator Shooting(float startTime)
    {
        while (Time.time < startTime + shootDur)
        {
            if (Time.time > shootTimer)
            {
                shootTimer = Time.time + shootDelay;
                Vector2 rotation = transform.position - spiralingAim.position;
                float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                GameObject bullet = ObjectPooler.Instance.SpawnFromPool("EnemyBullet", transform.position, Quaternion.Euler(0, 0, rot));
                bullet.GetComponent<BulletLogic>().SetBullet(bulletSpd, bulletdamage, true);
            }
            yield return null;
        }
        doneShooting = true;
        shootingsTimer = Time.time + shootingsDelay;
    }

    void HandlePlayerAim()
    {
        Vector2 targetPos = player.position - transform.position;
        playerAim.localPosition = targetPos.normalized;
    }

    IEnumerator Lasering(float startTime)
    {
        float laserAimTimer = 0;
        targetLaser.SetActive(true);
        actualLaser.GetComponent<LaserLogic>().damage = laserDamage;
        SoundSystem.Instance.PlayOneShotWithDelay(laserSfx, laserAimDur + .3f);
        while (Time.time < startTime + laserDur + laserAimDur)
        {
            laserAimTimer += Time.deltaTime;
            if (laserAimTimer > laserAimDur)
            {
                yield return new WaitForSeconds(.3f);
                actualLaser.SetActive(true);
                //actualLaser.GetComponent<SpriteRenderer>().color = new(1, 0, 0, Mathf.Lerp(1, 0, (laserAimTimer - laserAimDur) / laserDur));
            }
            else
            {
                Vector2 rotation = playerAim.position - transform.position;
                float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                targetLaser.transform.GetChild(0).localScale = new(10, Mathf.Lerp(0.02f, 0, laserAimTimer / laserAimDur), 0);
                targetLaser.transform.eulerAngles = new(0, 0, rot);
            }
            yield return null;
        }
        actualLaser.SetActive(false);
        targetLaser.SetActive(false);
        doneLaser = true;
        lasersTimer = Time.time + laserDelay;

    }
}
