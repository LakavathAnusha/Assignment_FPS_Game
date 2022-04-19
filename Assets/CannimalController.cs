using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CannimalController : MonoBehaviour
{
    public Animator anim;
    public GameObject target;
    public GameObject ragdollPrefab;
    NavMeshAgent agent;
    //udioSource audio;
    public List<AudioClip> audioClips;
    public float walkingSpeed;
    public float runningSpeed;
    public enum STATE { IDLE,WALK,RUN,ATTACK,PUNCH };
    public STATE state = STATE.IDLE;//default state
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
  
        if (target == null && GameStart.isGameOver == false)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        switch (state)
        {
            case STATE.IDLE:
                if (CanSeePlayer())
                    state = STATE.ATTACK;
                else if (Random.Range(0, 1000) < 5)
                {
                    state = STATE.WALK;
                }




                break;
            case STATE.WALK:
                if (!agent.hasPath)
                {
                    float randValueX = transform.position.x + Random.Range(-5f, 5f);
                    float randValueZ = transform.position.z + Random.Range(-5f, 5f);
                    float ValueY = Terrain.activeTerrain.SampleHeight(new Vector3(randValueX, 0f, randValueZ));
                    Vector3 destination = new Vector3(randValueX, ValueY, randValueZ);
                    agent.SetDestination(destination);
                    agent.stoppingDistance = 0f;
                    agent.speed = walkingSpeed;
                    TurnOffAllTriggerAnim();
                    anim.SetBool("IsWalk", true);
                }
                if (CanSeePlayer())
                {
                    state = STATE.RUN;
                }
                else if (Random.Range(0, 1000) < 7)
                {
                    state = STATE.IDLE;
                    TurnOffAllTriggerAnim();
                  //  anim.SetBool("IsRun", true);
                    agent.ResetPath();
                }

                break;

            case STATE.RUN:
                if (GameStart.isGameOver)
                {
                    TurnOffAllTriggerAnim();
                    state = STATE.WALK;
                    return;
                }
                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 2f;
                TurnOffAllTriggerAnim();
                anim.SetBool("IsRun", true);
                agent.speed = runningSpeed;
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    state = STATE.ATTACK;
                }
                if (CannotSeePlayer())
                {
                    state = STATE.WALK;
                    agent.ResetPath();
                }

                break;

            case STATE.ATTACK:
                if (GameStart.isGameOver)
                {
                    TurnOffAllTriggerAnim();
                    state = STATE.RUN;
                    return;
                }
                TurnOffAllTriggerAnim();
                anim.SetBool("IsAttack", true);
                transform.LookAt(target.transform.position);//Zombies should look at Player
                if (DistanceToPlayer() > agent.stoppingDistance + 2)
                {
                    state = STATE.RUN;
                }
                print("Attack State");
                break;

            /*case STATE.DEAD:

                //GameObject tempRd = Instantiate(ragdollPrefab, this.transform.position, this.transform.rotation);
                //tempRd.transform.Find("Hips").GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 10000);
                Destroy(agent);
               // this.GetComponent<SinkToGround>().ReadyToSink();
                break;*/

            default:
                break;
        }
    }
    public void TurnOffAllTriggerAnim()
    {
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsAttack", false);
        anim.SetBool("IsRun", false);
        anim.SetBool("IsPunch", false);
        //anim.SetBool("IsDead", false);
    }

    public bool CanSeePlayer()
    {
        if (DistanceToPlayer() < 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float DistanceToPlayer()
    {
        if (GameStart.isGameOver)
        {
            return Mathf.Infinity;

        }
        return Vector3.Distance(target.transform.position, this.transform.position);

    }

    public bool CannotSeePlayer()
    {
        if (DistanceToPlayer() > 20f)
        {
            return true;
        }
        else
            return false;
    }

    public void KillConnimal()
    {
        TurnOffAllTriggerAnim();
        anim.SetBool("isDead", true);
        state = STATE.ATTACK;
    }

    int damageAmount = 5;
    public void DamagePlayer()
    {
        if (target != null)
        {
            target.GetComponent<PlayerController>().TakeHit(damageAmount);//create a method Random sound when player takes damage
        }



    }
}

public class GameStart
{
    public static bool isGameOver = false;
}