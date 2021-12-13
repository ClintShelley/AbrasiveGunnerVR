using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "Unity-FSM/States/Idle", order = 1)]
//this idle state is for the patrolling enemy type
public class IdleState : AbstractFSMState
{
    [SerializeField]
    float _idleDuration = 0.5f;

    float _totalDuration;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.IDLE;
    }

    public override bool EnterState()
    {
        EnteredState = base.EnterState();

        if (EnteredState)
        {
            _totalDuration = 0f;
        }
        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            //if this much time has passed or enemy is close change to attack or patrol
            _totalDuration += Time.deltaTime;
            if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) <= 25f)
            {
                _fsm.EnterState(FSMStateType.ATTACK);
            }
            if (_totalDuration >= _idleDuration)
            {
                _fsm.EnterState(FSMStateType.PATROL);
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }
}
