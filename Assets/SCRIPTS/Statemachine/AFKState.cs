using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using NPCCode;

[CreateAssetMenu(fileName = "AFKState", menuName = "Unity-FSM/States/AFK", order = 4)]
public class AFKState : AbstractFSMState
{
    [SerializeField]
    float _afkDuration = 3f;

    float _fullDuration;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.AFK;
    }

    public override bool EnterState()
    {
        EnteredState = base.EnterState();
        if (EnteredState)
        {
            _fullDuration = 0f;
        }
        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) <= 15f)
            {
                _fsm.EnterState(FSMStateType.AATTACK);
            }
            else
            {
                _fullDuration += Time.deltaTime;
                if (_fullDuration >= _afkDuration)
                {
                    _fsm.EnterState(FSMStateType.AFK);
                }
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }
}
