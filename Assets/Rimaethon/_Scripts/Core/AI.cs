using System;
using PlasticGui.Gluon.WorkspaceWindow;
using Rimaethon._Scripts.Core.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Rimaethon._Scripts.Core
{
    public  class AI : Character
    {
        private NavMeshAgent _agent;
        private IAnimateAble _animator;
        

        private void Start()
        {

            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<IAnimateAble>();

        }


        private void Update()
        {
            _animator.HandleAnimationRequest(IAnimateAble.IsRunning, _agent.hasPath);
        }
    }
}


