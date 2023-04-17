using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Core.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Rimaethon._Scripts.AI_Behavior_System
{
    public class AIReferences {
        public GameObject GameObject;
        public Transform Transform;
        public Animator Animator;
        public Rigidbody Physics;
        public NavMeshAgent Agent;
        public SphereCollider SphereCollider;
        public BoxCollider BoxCollider;
        public CapsuleCollider CapsuleCollider;
        public CharacterController CharacterController;
        public IPlatformAble CharacterPlatform;
        public ITypeDeterminer CharacterType;

       
        public static AIReferences CreateFromGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                Debug.Log("fucking gameobject is null");
            }
            AIReferences reference = new AIReferences();
            reference.GameObject = gameObject;
            if (reference.GameObject==null)
            {
                Debug.Log("gameobject is null");
            }
            reference.Transform = gameObject.transform;
            if (reference.Transform==null)
            {
                Debug.Log("gameobject is null");
            }
            else
            {
                Debug.Log("transform is "+reference.Transform);
            }
            reference.Animator = gameObject.GetComponentInChildren<Animator>();
            if (reference.Animator==null)
            {
                Debug.Log("animator is null");
            }
            reference.Agent = gameObject.GetComponent<NavMeshAgent>();
            if (reference.Agent==null)
            {
                Debug.Log("agent is null");
            }
            reference.CapsuleCollider = gameObject.GetComponent<CapsuleCollider>();
            reference.CharacterController = gameObject.GetComponent<CharacterController>();
            reference.CharacterPlatform = gameObject.GetComponent<IPlatformAble>();
            if (reference.CharacterPlatform==null)
            {
                Debug.Log("characterplatform is null");
            }
            reference.CharacterType = gameObject.GetComponent<ITypeDeterminer>();
            if (reference.CharacterType==null)
            {
                Debug.Log("charactertype is null");
            }
            
       //     Debug.Log("I get reference of "+ reference.CharacterType.ColorType+"which is on this platform: "+reference.CharacterPlatform.PlatformState);
            
            
            return reference;
        }
        
    }
}
