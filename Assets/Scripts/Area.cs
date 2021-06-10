using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;
using MyGame;

public enum AreaType { AreaUp, AreaDown, FinishAudio}
public class Area : MonoBehaviour, ITriggerable, IAudible
{
     
    [SerializeField] private AreaType _areaType;
    private AudioSource _audioSource;

    public void EnterCollided()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
        
        DevMode.instance.Log("Enter COllided");
        this.PostEvent(EventID.OnCastAnim, PlayerAnimState.Walk);
        this.PostEvent(EventID.OnCastMovementState, PlayerMomvementState.Walk);

    }

    public void ExitCollided()
    {
        this.PostEvent(EventID.OnCastAnim, PlayerAnimState.Run);
        this.PostEvent(EventID.OnCastMovementState, PlayerMomvementState.Run);
        DevMode.instance.Log("Exit COllided");
    }

    public void PlayAudio()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();

        }
    }
    public void StopAudio()
    {
        _audioSource.Stop();
    }
    public void SelfDestroy()
    {

    }

    public void SetVisible()
    {
        throw new System.NotImplementedException();
    }

    public void StayCollided()
    {
        DevMode.instance.Log("Stay COllided");

        switch (_areaType)
        {
            case AreaType.AreaUp:
                this.PostEvent(EventID.OnAreaStayCollided, _areaType);
                break;
            case AreaType.AreaDown:
                this.PostEvent(EventID.OnAreaStayCollided, _areaType);

                break;
            default:
                break;
        }
    }


}
