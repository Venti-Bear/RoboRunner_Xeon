using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRangedAttack : MonoBehaviour
{
    public Transform firePointRight;
    public Transform firePointLeft;
    private Transform bulletSpawn;
    public GameObject bulletPrefab;
    private Animator anim;
    private SpriteRenderer sprite;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("shotBullet", true);
            StartCoroutine(ShootBullet());
        }
    }

    IEnumerator ShootBullet()
    {
        bulletSpawn = (sprite.flipX ? firePointLeft : firePointRight);
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        newBullet.GetComponent<BulletBaseBehavior>().speed = (sprite.flipX ? -1 : 1) * 10f;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("shotBullet", false);
    }
}