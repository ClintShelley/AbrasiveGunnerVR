using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPCCode;

[CreateAssetMenu(fileName = "AAttackState", menuName = "Unity-FSM/States/AAttack", order = 5)]
//this attack state is for the AFK enemy type
public class AAttackState : AbstractFSMState
{

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.AATTACK;
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
            if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 30f)
            {
                _navMeshAgent.SetDestination(player.transform.position);
                //if player is this close begin to attack
                if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 1.5f)
                {
                    FindObjectOfType<Enemy>().startDealDamage();
                }
                //if player isnt this close stop attacking and return to chasing
                else if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) > 1.5f)
                {
                    FindObjectOfType<Enemy>().stopDealDamage();
                    _fsm.EnterState(FSMStateType.AATTACK);
                }
            }
            //if player gets this far return to idling
            else if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) > 29f)
            {
                _fsm.EnterState(FSMStateType.AFK);
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }
}
