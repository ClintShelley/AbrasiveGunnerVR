using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using NPCCode;

public enum ExecutionState
{
    NONE,
    ACTIVE,
    COMPLETED,
    TERMINATED,
};

public enum FSMStateType
{
    IDLE,
    PATROL,
    ATTACK,
    AATTACK,
    PPATROL,
    IDLE2,
    AFK,
}

public abstract class AbstractFSMState : ScriptableObject
{
    protected NavMeshAgent _navMeshAgent;
    protected NPC _npc;
    protected FiniteStateMachine _fsm;
    protected GameObject player;

    public ExecutionState ExecutionState { get; protected set; }
    public FSMStateType StateType { get; protected set; }
    public bool EnteredState { get; protected set; }

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;

        player = GameObject.Find("XR Rig");
       
    }

    public virtual bool EnterState()
    {
        bool successNavMesh = true;
        bool successNPC = true;

        ExecutionState = ExecutionState.ACTIVE;

        successNavMesh = (_navMeshAgent != null);

        successNPC = (_npc != null);

        return successNavMesh & successNavMesh;
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public virtual void SetNavMeshAgent(NavMeshAgent navMeshAgent)
    {
        if(navMeshAgent != null)
        {
            _navMeshAgent = navMeshAgent;
        }
    }

    public virtual void SetExecutingFSM(FiniteStateMachine fsm)
    {
        if(fsm != null)
        {
            _fsm = fsm;
        }
    }

    public virtual void SetExecutingNPC(NPC npc)
    {
        if(npc != null)
        {
            _npc = npc;
        }
    }
}
