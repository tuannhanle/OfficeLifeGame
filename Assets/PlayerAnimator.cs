using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

public enum PlayerAnimState { Idle, Run, Walk, Wave }
public class PlayerAnimator : MonoBehaviour
{
    Animator _animator;
    [SerializeField] string _idleAnim, _runAnim, _walkAnim, _waveAnim;
    void Awake()
    {
        _animator = GetComponent<Animator>();
        this.RegisterListener(EventID.OnCastAnim, (o) => SetAnimation((PlayerAnimState)o));
    }

    private void SetAnimation(PlayerAnimState playerAnim)
    {
        switch (playerAnim)
        {
            case PlayerAnimState.Idle:
                _animator.Play(_idleAnim);

                break;
            case PlayerAnimState.Run:
                _animator.Play(_runAnim);
                break;
            case PlayerAnimState.Walk:
                _animator.Play(_walkAnim);

                break;
            case PlayerAnimState.Wave:
                _animator.Play(_waveAnim);
                break;
            default:
                break;
        }

    }

}
