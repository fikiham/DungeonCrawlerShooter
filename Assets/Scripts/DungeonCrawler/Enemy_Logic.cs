using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_Logic : MonoBehaviour
{
    GameObject player;
    AIPath aiPath;
    AIDestinationSetter aiDes;

    [SerializeField] float sightDistance = 5;
    [SerializeField] float fieldOfView = 15;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpd = 10;
    [SerializeField] float bulletdamage = 5;
    [SerializeField] float shootDel = 1;
    float shootTimer;


    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
        aiDes = GetComponent<AIDestinationSetter>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aiDes.target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        aiPath.canMove = !SeePlayer();
        if (SeePlayer())
        {
            Shoot();
            Vector2 rotation = transform.position - player.transform.position;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new(0, 0, rot + 90);
        }
    }

    void Shoot()
    {
        if (Time.time > shootTimer)
        {
            shootTimer = Time.time + shootDel;
            Vector3 rot = transform.eulerAngles - (Vector3.forward * Random.Range(80, 100));
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot), GameController.Instance.sack);
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool("EnemyBullet", transform.position, Quaternion.Euler(rot));
            bullet.GetComponent<BulletLogic>().SetBullet(bulletSpd, bulletdamage, true);
        }
    }

    bool SeePlayer()
    {
        if (player != null && player.activeInHierarchy)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector2 targetDir = player.transform.position - transform.position;
                float angleToPlayer = Vector2.Angle(targetDir, transform.up);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position + transform.up, targetDir * sightDistance);
                    if (hitInfo)
                    {
                        if (hitInfo.transform.gameObject == player)
                            return true;
                    }
                }
            }
        }
        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + transform.up, transform.up * sightDistance);
    }
#endif
}
