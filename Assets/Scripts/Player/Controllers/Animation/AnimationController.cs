using UnityEngine;
public class AnimationController : MonoBehaviour, IAnimation
{

    [SerializeField] private Animator _animator;

    public void PlayIdle()
    {
        _animator.SetBool("isWalking", false);
        _animator.SetBool("isRunning", false);
    }

    public void PlayWalk()
    {
        _animator.SetBool("isWalking", true);
        _animator.SetBool("isRunning", false);
    }

    public void PlayRun()
    {
        _animator.SetBool("isWalking", false);
        _animator.SetBool("isRunning", true);
    }

    public void PlayIdleCrouch()
    {
        _animator.SetBool("isCrouch", true);
    }

    public void StopCrouch()
    {
        _animator.SetBool("isCrouch", false);
    }
}
