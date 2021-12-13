using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "Unity-FSM/States/Attack", order = 3)]
//this attack state is for the patrolling enemy type
public class AttackState : AbstractFSMState
{

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.ATTACK;
    }

    public override bool EnterState()
    {
        EnteredState = base.EnterState();

        if (EnteredState)
        {

        }
        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            //if player is in this range double check and continue to chase
            if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 27f)
            {
                _navMeshAgent.SetDestination(player.transform.position);
                var lookPos = player.transform.position - _navMeshAgent.transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                _navMeshAgent.transform.rotation = Quaternion.Slerp(_navMeshAgent.transform.rotation, rotation, Time.deltaTime * 8f);
                //if player is this close begin to attack
                if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 3f)
                {
                    if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 1.5f)
                    {
                        FindObjectOfType<Enemy>().startDealDamage();
                    }
                    //if player isnt this close stop attacking and return to chasing
                    else if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) > 1.5f)
                    {
                        FindObjectOfType<Enemy>().stopDealDamage();
                        _fsm.EnterState(FSMStateType.ATTACK);
                    }
                }

            }
            //if player gets this far return to idling
            else if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) > 27f)
            {
                _fsm.EnterState(FSMStateType.IDLE);
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }
}
