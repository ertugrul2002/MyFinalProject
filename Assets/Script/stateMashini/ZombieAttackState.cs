using System;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttackState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;
    public float stopAttackingDistance =2.5f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Initialization
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(SoundManager.Instace.zombieChannel1.isPlaying == false)
        {
            SoundManager.Instace.zombieChannel1.PlayOneShot(SoundManager.Instace.zombieAttack);
        }
       LookAtPlayer();
       float distaceFromPlayer =Vector3.Distance(player.position,animator.transform.position);
       // cheacking if the agent should Attack
        if(distaceFromPlayer > stopAttackingDistance)
        {
            animator.SetBool("isAttacking",false);
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - agent.transform.position;
        agent.transform.rotation =Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0,yRotation,0);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instace.zombieChannel1.Stop();
    }
}
