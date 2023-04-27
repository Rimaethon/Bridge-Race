using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Rimaethon._Scripts.AI_Behavior_System.Runtime
{
    
    public class Context {
        public GameObject gameObject;
        public Transform transform;
        public Animator animator;
        public Rigidbody physics;
        public NavMeshAgent agent;
        public SphereCollider sphereCollider;
        public BoxCollider boxCollider;
        public CapsuleCollider capsuleCollider;
        public CharacterController characterController;
        public IPlatformAble characterPlatform;
        public ITypeDeterminer characterType;
        public IBrickCountProvider brickStacker;
        public bool IsGoingToDoor;
        public static Context CreateFromGameObject(GameObject gameObject) {
            // Fetch all commonly used components
            Context context = new Context();
            context.gameObject = gameObject;
            context.transform = gameObject.transform;
            context.animator = gameObject.GetComponent<Animator>();
            context.physics = gameObject.GetComponent<Rigidbody>();
            context.agent = gameObject.GetComponent<NavMeshAgent>();
            context.sphereCollider = gameObject.GetComponent<SphereCollider>();
            context.boxCollider = gameObject.GetComponent<BoxCollider>();
            context.capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
            context.characterController = gameObject.GetComponent<CharacterController>();
            context.characterPlatform = gameObject.GetComponent<IPlatformAble>();
            context.characterType = gameObject.GetComponent<ITypeDeterminer>();
            context.brickStacker = gameObject.GetComponent<IBrickCountProvider>();
            context.IsGoingToDoor = false;
            
            // Add whatever else you need here...

            return context;
        }
    }
}