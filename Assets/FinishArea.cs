using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

public class FinishArea : MonoBehaviour, ITriggerable, IAudible
{
    public void EnterCollided()
    {
        this.PostEvent(EventID.OnCastMovementState, PlayerMomvementState.Idle);
        this.PostEvent(EventID.OnCastAnim, PlayerAnimState.Idle);
        this.PostEvent(EventID.OnGameLose);
    }

    public void ExitCollided()
    {
    }

    public void PlayAudio()
    {

    }

    public void SelfDestroy()
    {
    }

    public void SetVisible()
    {
    }

    public void StayCollided()
    {

    }

    public void StopAudio()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
