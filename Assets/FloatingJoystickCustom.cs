using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Observer;
using MyGame;
using System;

public class FloatingJoystickCustom : FloatingJoystick
{
    Action OnPlayOnceTime;

    protected override void Start()
    {
        OnPlayOnceTime += PlayOnceTimeHandle;

        base.Start();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        OnPlayOnceTime?.Invoke();
        base.OnPointerDown(eventData);
    }

    private void PlayOnceTimeHandle()
    {
        OnPlayOnceTime -= PlayOnceTimeHandle;
        GameManager.instance.ChangeStage(GameStage.Playing);

        //this.PostEvent(EventID.OnCastAnim, PlayerAnimState.Run);
        //this.PostEvent(EventID.OnCastMovementState, PlayerMomvementState.Run);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }
}
