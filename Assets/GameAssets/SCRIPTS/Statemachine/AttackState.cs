using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "Unity-FSM/States/Attack", order = 3)]
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
            if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 15f)
            {
                _navMeshAgent.SetDestination(player.transform.position);
                var lookPos = player.transform.position - _navMeshAgent.transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                _navMeshAgent.transform.rotation = Quaternion.Slerp(_navMeshAgent.transform.rotation, rotation, Time.deltaTime * 8f);
                
          
            }
            else if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) > 10f)
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
