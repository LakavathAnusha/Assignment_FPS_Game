using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CannimalController : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    Animator animator;
    public Transform target;
    PlayerController player;
    public enum STATE
    {
        IDLE, RUN,WALK,ATTACK
    }
    public STATE state = STATE.IDLE;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATE.IDLE:
                Idle();
                break;
            case STATE.RUN:
                Run();
                break;
            case STATE.WALK:
                Walk();
                break;
            case STATE.ATTACK:
                Attack();
                break;
            default:
                break;
        }
    }

    public void Walk()
    {
        AllAnimationFalse();
        if (Vector3.Distance(target.position, this.transform.position) > 4f)
        {
            state = STATE.ATTACK;
            animator.SetBool("IsWalk",true);
        }

    }

    public void Idle()
    {
        AllAnimationFalse();
        if (Vector3.Distance(target.position, this.transform.position) < 15f)
        {
            state = STATE.WALK;
        }
        if (Vector3.Distance(target.position, this.transform.position) > 15f)
        {
            state = STATE.RUN;
        }
    }
    public void Run()
    {
        AllAnimationFalse();
        animator.SetBool("IsRun", true);
        agent.stoppingDistance = 5f;
        agent.SetDestination(target.transform.position);

        if (GetDistance() < agent.stoppingDistance + 1f)
        {
            state = STATE.ATTACK;
            animator.SetBool("IsAttack", true);
        }
        if (GetDistance() > 20f)
        {
            state = STATE.IDLE;
        }


    }
    public void Attack()
    {
        AllAnimationFalse();
        animator.SetBool("IsAttack", true);
        player.health--;
        if (GetDistance() > agent.stoppingDistance)
        {
            state = STATE.IDLE;
        }
    }
   
    public void AllAnimationFalse()
    {
        animator.SetBool("IsAttack", false);
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsRun", false);
        //animator.SetBool("IsPunch", false);
      
    }
   
    public float GetDistance()
    {
        return (Vector3.Distance(target.position, this.transform.position));
    }
}