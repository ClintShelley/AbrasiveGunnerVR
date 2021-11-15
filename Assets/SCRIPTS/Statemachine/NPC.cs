using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace NPCCode
{
    [RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]

    public class NPC : MonoBehaviour
    {
        [SerializeField]
        NPCPatrolPoint[] _patrolPoints;
        NavMeshAgent _navMeshAgent;
        FiniteStateMachine _finiteStateMachine;

        public void Awake()
        {
            _navMeshAgent = this.GetComponent<NavMeshAgent>();
            _finiteStateMachine = this.GetComponent<FiniteStateMachine>();
        }

        public void Start()
        {

        }

        public void Update()
        {

        }

        public NPCPatrolPoint[] PatrolPoints
        {
            get
            {
                return _patrolPoints;
            }
        }
    }
}
