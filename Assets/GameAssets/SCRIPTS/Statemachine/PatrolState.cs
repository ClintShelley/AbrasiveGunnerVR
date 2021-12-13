using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using NPCCode;

[CreateAssetMenu(fileName="PatrolState", menuName = "Unity-FSM/States/Patrol", order = 2)]
//this patrol state is for the patrolling enemy type
public class PatrolState : AbstractFSMState
{

    NPCPatrolPoint[] _patrolPoints;
    int _patrolPointIndex;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.PATROL;
        _patrolPointIndex = -1;
    }

    public override bool EnterState()
    {
        EnteredState = false;
        if (base.EnterState())
        {
            _patrolPoints = _npc.PatrolPoints;

            if (_patrolPoints == null || _patrolPoints.Length == 0)
            {

            }
            else
            {
                if (_patrolPointIndex < 0)
                {
                    _patrolPointIndex = UnityEngine.Random.Range(0, _patrolPoints.Length);
                }
                else
                {
                    _patrolPointIndex = (_patrolPointIndex + 1) % _patrolPoints.Length;
                }

                SetDestination(_patrolPoints[_patrolPointIndex]);
                EnteredState = true;
            }
        }
        return EnteredState;
    }


    public override void UpdateState()
    {
        if (EnteredState)
        {
            //if player is this distance start to chase and attack
            if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) <= 25f)
            {
                _fsm.EnterState(FSMStateType.ATTACK);
            }
            //check range of points in this distance
            else if (Vector3.Distance(_navMeshAgent.transform.position, _patrolPoints[_patrolPointIndex].transform.position) >= 6f)
            {
                _fsm.EnterState(FSMStateType.IDLE);
            }
        }
    }

    private void SetDestination(NPCPatrolPoint destination)
    {
        if(_navMeshAgent != null && destination != null)
        {
            _navMeshAgent.SetDestination(destination.transform.position);
        }
    }
}
