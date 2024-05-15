using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Action : MonoBehaviour
{
    public static Player_Action Instance;

    KeyCode normalInput = KeyCode.Mouse0;
    KeyCode specialInput = KeyCode.Mouse1;
    KeyCode actionInput = KeyCode.F;

    KeyCode quickSlot1 = KeyCode.Q;
    KeyCode quickSlot2 = KeyCode.E;

    [SerializeField] Transform aim;


    [Header("SHOOT")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpd = 10;
    public float bulletdamage = 50;
    public float defaultFirerate = 2;
    float fireTimer;
    bool canAttack = true;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        HandleAim();

        if (Input.GetKey(normalInput))
        {
            // Do something normal like attacking
            if (canAttack && Time.time > fireTimer)
            {
                fireTimer = Time.time + (1 / defaultFirerate);
                Attack();
            }
        }

        if (Input.GetKeyDown(specialInput))
        {
            // Do something special like special attacking
            if (canAttack)
            {
                //SpecialAttack();
            }
        }
    }

    void HandleAim()
    {
        Vector2 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.localPosition = aimPos.normalized;
    }

    void Attack()
    {
        Vector2 rotation = transform.position - aim.position;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, rot), GameController.Instance.sack);
        GameObject bullet = ObjectPooler.Instance.SpawnFromPool("Bullet", transform.position, Quaternion.Euler(0, 0, rot));
        bullet.GetComponent<BulletLogic>().SetBullet(bulletSpd, bulletdamage);

    }


}
