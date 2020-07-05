using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static int maxHealth = 3;
    public static int playerHealth;
    public static bool hasGun = false;
    public static bool hasSword = false;
    public static bool isAttacking = false;
    public bool playerInSword = false;
    public bool playerInGun = false;

    public float swordCooldown = 1.0f;
    public float gunCooldown = 1.0f;

    public GameObject swordGround;
    public GameObject gunGround;

    private static bool isInvincible = false;

    public GameObject sword;
    public GameObject gun;
    public GameObject swordbox;
    public GameObject bullet;
    public GameObject emitter;
    public static string Using = "";

    Animator charAnimations;
    public bool shoot =false;
    public bool swordHit = false;
    bool run;

    Character_Movement charMoveScript;

    private void Awake() {
        charMoveScript = GetComponent<Character_Movement>();
        run = charMoveScript.runnning;
        charAnimations = transform.Find("The Adventurer Blake").GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Player spawn with full HP
        playerHealth = maxHealth;
        isInvincible = false;
        hasGun = false;
        hasSword = false;
        playerInSword = false;
        playerInGun = false;
}

    // Update is called once per frame
    void Update()
    {
        run = charMoveScript.runnning;

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!isInvincible)
            {
                isInvincible = true;
                print("IM INVINCIBLE");
            }
        }

        //Make sure the player's HP does not go higher than wanted
        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }

        if(playerHealth <= 0)
        {
            SceneManager.LoadScene("Dead Screen 2");
        }

        //HP Stuff for Testing
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            playerHealth -= 1;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            playerHealth += 1;
        }*/

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (hasGun)
            {
                equipmentDisplay.ChangeCurrentEquip(1);
                gun.SetActive(true);
                sword.SetActive(false);
                Using = "gun";
            }
                
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (hasSword)
            {
                equipmentDisplay.ChangeCurrentEquip(2);
                sword.SetActive(true);
                gun.SetActive(false);
                Using = "sword";
            }
        }
        /*if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equipmentDisplay.ChangeCurrentEquip(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            equipmentDisplay.ChangeCurrentEquip(4);
        }*/
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && isAttacking == false)
        {
            Attack();
        }

        if (playerInGun && hasGun == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerStats.hasGun = true;
                equipmentDisplay.ChangeCurrentEquip(1);
                gun.SetActive(true);
                sword.SetActive(false);
                Using = "gun";
                gunGround.SetActive(false);
                pickupWeapon.gunDestroyed();

            }

        }
        if (playerInSword && hasSword == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerStats.hasSword = true;
                equipmentDisplay.currentEquip = 2;
                sword.SetActive(true);
                gun.SetActive(false);
                Using = "sword";
                swordGround.SetActive(false);
                pickupWeapon.MeleeDestroyed();
            }
        }
        
    }

    //Static function for adding health to the player (send it negative value to subtract health)
    public static void AddHealth(int amt)
    {
        if (isInvincible)
        {
            if (amt > 0)
            {
                playerHealth += amt;
            }
        }
        else
        {
        playerHealth += amt;
        }
    }

    //We die like men, here are the attacking scripts as well.

    public void Attack()
    {
        isAttacking = true;
        if(hasSword == true && Using == "sword")
        {
            swordbox.SetActive(true);
            StartCoroutine(SwordHitboxActive(0.5f));
            AttackAnimation(shoot, swordHit);
        }

        else if(hasGun == true && Using == "gun")
        {
            Shoot();
            StartCoroutine(Cooldown(gunCooldown));
            AttackAnimation(shoot, swordHit);
        }

    }

    public IEnumerator Cooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isAttacking = false;
    }

    public IEnumerator SwordHitboxActive(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        swordbox.SetActive(false);
        StartCoroutine(Cooldown(swordCooldown));
    }
    public void Shoot()
    {
        GameObject currentBullet = Instantiate(bullet, emitter.transform.position, emitter.transform.rotation) as GameObject;
        currentBullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        currentBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 700);
        Destroy(currentBullet, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("WHO HATH ENTERED");
            if (other.gameObject.CompareTag("Melee"))
            {
            print("ahh.. tis a sword");
            playerInSword = true;
            }
            if (other.gameObject.CompareTag("Ranged"))
            {
            print("ahh.. tis a gun");
            playerInGun = true;          
        }
            if (other.gameObject.CompareTag("EscapeTree"))
        {
            SceneManager.LoadScene("Victory Scene 1");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Melee"))
        {
            playerInSword = false;
        }
        if (other.gameObject.CompareTag("Ranged"))
        {
            playerInGun = false;
        }
    }

    void AttackAnimation(bool gunShot, bool swordSlice) {
        /*if(gunShot == true && swordSlice == false) {
            charAnimations.SetBool("Shooting", gunShot);
            charAnimations.SetBool("isHitting", swordSlice);
        } else {
            charAnimations.SetBool("Shooting", gunShot);
            charAnimations.SetBool("isHitting", swordSlice);
        }*/
        charAnimations.SetBool("Shooting", gunShot);
        charAnimations.SetBool("isHitting", swordSlice);
    }

}
