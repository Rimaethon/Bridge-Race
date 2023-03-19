using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected Camera playerCamera;
    protected Animator animator;
    protected float speed = 0.075f;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Animate(bool isRunning)
    {
        animator.SetBool("IsRunning", isRunning);
    }

    protected abstract void Move();
}