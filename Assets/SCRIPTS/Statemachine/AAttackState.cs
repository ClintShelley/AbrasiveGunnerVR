using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPCCode;

[CreateAssetMenu(fileName = "AAttackState", menuName = "Unity-FSM/States/AAttack", order = 5)]
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
            if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 15f)
            {
                _navMeshAgent.SetDestination(player.transform.position);
            }
            else if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) > 10f)
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
