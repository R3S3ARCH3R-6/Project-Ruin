using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;     //enables the project to use Unity AI library
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/// <summary>
/// The following script is derived from the enemy script given from 
/// CMPS 327 and the Star Wars game
/// Website: https://docs.unity3d.com/ScriptReference/SceneManagement.Scene-name.html (used to help me know what scene I am in)
/// </summary>

//FSM States for the enemy
public enum Enemy2State { CHASE, MOVING, DEFAULT, ATTACK };
//Default - init state (when it is not moving)
//other states will be based on the player's rotation/movement

/// <summary>
/// EnemyAI ...
/// </summary>
/// 
[RequireComponent(typeof(NavMeshAgent))]
public class BearAI : MonoBehaviour {
    GameObject player;  //the player
    NavMeshAgent agent; //used to move along the navmesh

    public float chaseDistance = 20.0f; //distance the enemy must be before it chases the player
    public float attackDistance = 15.0f; //distance the enemy must be before it attacks the player

    protected Enemy2State state = Enemy2State.DEFAULT;    //init state in FSM is Default
    protected Vector3 destination = new Vector3(0, 0, 0);   //initial destination is the zero vector

    public int enemyHealth = 12;

    //Particle Sys. Explosion and blood spatter
    ParticleSystem blood;
    bool spatterStarted = false;     //says whether the explosion has started or not (prevents repeats)

    Bear_Attack attack;
    public float biteTime = 1.0f;
    public float chewTime = 1.0f;
    public int killCount = 0;
    Scene currentScene;

    //private AudioSource growl;
    //private AudioSource attackNoise;
    //private AudioSource deathSound;
    private Animator bearAnim;
    private bool isRunning = true;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Awake() {
        player = GameObject.FindWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();  //gets the NavMeshAgent component
        blood = GetComponentInChildren<ParticleSystem>();   //gets particle sys. component attached to the enemy
        bearAnim = GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene();
        
        //attackNoise = transform.Find("Attack").GetComponent<AudioSource>(); //.GetComponentInChildren<AudioSource>();
        //deathSound = transform.Find("DeathSound").GetComponent<AudioSource>(); //.GetChild(8).GetComponentInChildren<AudioSource>();
        //growl = transform.Find("Growl").GetComponent<AudioSource>(); //.GetChild(7).GetComponentInChildren<AudioSource>();
    }

    //creates a random position for the enemy to be in or go to
    private Vector3 RandomPosition() {
        return new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f));
        //y-coord will be 0 (not instantiating in the air)
    }

    void FaceTarget() {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Update is called once per frame
    void Update() {
        //FSM control code below
        switch (state) {
            //starts in the default state
            case Enemy2State.DEFAULT:
                destination = transform.position + RandomPosition();
                //add a random position to the enemy's position that the enemy will move to

                //if the player is less than the "chaseDistance" from the enemy, switch to the "CHASE" state
                if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance) {
                    state = Enemy2State.CHASE;
                } else    //move the enemy randomly if the enemy is too far from the enemy
                  {

                    state = Enemy2State.MOVING;
                    agent.SetDestination(destination);  //destination will be a random location
                }

                if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance) {
                    state = Enemy2State.ATTACK;
                }
                break;
            //Moving state controls random movement
            case Enemy2State.MOVING:
                isRunning = true;
                isAttacking = false;
                BearAnimations(isRunning, isAttacking);

                //Debug.Log("Dest = " + destination);
                //when enemy is < 5 from the random distance, change destination again (done in Default state)
                if (Vector3.Distance(transform.position, destination) < 5) {
                    state = Enemy2State.DEFAULT;
                    //firePoint.shoot = false;
                }
                //if the enemy gets close enough to the player, switch to the chase state and chase the player
                if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance) {
                    state = Enemy2State.CHASE;
                    //firePoint.shoot = false;
                }

                if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance) {
                    state = Enemy2State.ATTACK;
                    //firePoint.shoot = true;
                }
                break;
            //state that chases the player
            case Enemy2State.CHASE:
                isRunning = true;
                isAttacking = false;
                BearAnimations(isRunning, isAttacking);
                //FindObjectOfType<AudioManager>().Play("chase");
                //if the distance b/w the enemy and player exceeds the chase distance, switch to Default state
                if (Vector3.Distance(transform.position, player.transform.position) > chaseDistance) {
                    //FindObjectOfType<AudioManager>().Play("lost");
                    state = Enemy2State.DEFAULT;
                    //firePoint.shoot = false;
                }

                if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance) {
                    state = Enemy2State.ATTACK;
                    //firePoint.shoot = true;
                }
                FaceTarget();
                agent.SetDestination(player.transform.position);    //moves the enemy to the player's position

                break;
            //state where the enemy attacks the player
            case Enemy2State.ATTACK:
                //FindObjectOfType<AudioManager>().Play("shoot");
                isRunning = false;
                isAttacking = true;
                BearAnimations(isRunning, isAttacking);
                if (Vector3.Distance(transform.position, player.transform.position) > attackDistance) {
                    //FindObjectOfType<AudioManager>().Play("get back");
                    state = Enemy2State.CHASE;
                    //firePoint.shoot = false;
                }

                if (Vector3.Distance(transform.position, player.transform.position) > chaseDistance) {
                    state = Enemy2State.DEFAULT;
                    //firePoint.shoot = false;
                }

                if (Time.time > biteTime) {
                    //FindObjectOfType<AudioManager>().Play("blaster SFX");
                    //firePoint.FireBullet();
                    biteTime = Time.time + chewTime;
                }
                FaceTarget();
                agent.SetDestination(player.transform.position);

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// this enables the player to take damage from bullets, 
    /// ...
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col) {
        //if(currentScene.name == "Boss Room") {
            if (col.gameObject.CompareTag("PlayerAtk")) {
                enemyHealth--;
                //FindObjectOfType<AudioManager>().Play("SThit");
                //growl.Play();
                if (enemyHealth <= 0) {
                    killCount++;

                    // Disable all Renderers and Colliders
                    Renderer[] allRenderers = gameObject.GetComponentsInChildren<Renderer>();
                    foreach (Renderer c in allRenderers) c.enabled = false;

                    Collider[] allColliders = gameObject.GetComponentsInChildren<Collider>();
                    foreach (Collider c in allColliders) c.enabled = false;

                    //StartCoroutine(PlayAndDestroy(myaudio.clip.length));

                    //gameObject.GetComponent<ParticleSystemRenderer>().enabled = true;   //needed or the particle sys. won't show up
                    gameObject.GetComponentInChildren<ParticleSystemRenderer>().enabled = true;   //needed or the particle sys. won't show up
                    StartExplosion();   //makes explosion occur when the enemy is hit
                    StartCoroutine(PlayAndDestroy(chewTime));
                }
            }
        //}
    }

    //this enables the audio to play even after the object gets destroyed
    private IEnumerator PlayAndDestroy(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        StopExplosion();    //stops the explosion
        Destroy(gameObject);
    }

    /// <summary>
    /// ...
    /// </summary>
    private void StartExplosion() {
        //deathSound.Play();
        if (spatterStarted == false) {
            blood.Play();
            spatterStarted = true;
        }
    }

    /// <summary>
    /// ...
    /// </summary>
    private void StopExplosion() {
        spatterStarted = false;
        blood.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        blood.Stop();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

    private void OnDestroy() {
        //energy.addScore(10);
    }

    void BearAnimations(bool run, bool attack) {
        if(run == true) {
            bearAnim.SetBool("isRunning", run);
        }

        if(attack == true) {
            int value = Random.Range(0, 10);
            if((value % 2) == 1) {
                bearAnim.SetBool("Attack1", true);
            } else {
                bearAnim.SetBool("Attack2", true);
            }
            
        }
    }
}
