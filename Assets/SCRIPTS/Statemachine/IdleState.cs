using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "Unity-FSM/States/Idle", order = 1)]
public class IdleState : AbstractFSMState
{
    [SerializeField]
    float _idleDuration = 1f;

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
            _totalDuration += Time.deltaTime;
            if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) <= 8f)
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
