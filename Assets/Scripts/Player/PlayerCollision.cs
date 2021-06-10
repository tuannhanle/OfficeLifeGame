using MyGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

interface ITriggerable
{
    void EnterCollided();
    void ExitCollided();
    void StayCollided();
    void SelfDestroy();

    void SetVisible();
}
interface IHittable
{
    void Collided();
    void PreCollided();
    void ExitCollided();
    void ExitPreCollided();
}
interface ICollidable
{
    void EnterCollided(Vector3 attacker, float force);
}
public class PlayerCollision : MonoBehaviour
{
    [Header("Debug Mode")]
    [SerializeField] private bool isDebug = true;
    [SerializeField] private float _rayLenght;
    [SerializeField] private float _rayHeight;
    [SerializeField] private float _distanceBetweenPlayerAndWall;

    private Action OnDebug;
    private Action<Action> OnCollidedOnceTime;
    private Action<Action<RaycastHit>> OnRayCastForward, OnRaycastDown;

    private GameObject _lastForwardHitted, _lastDownHitted = null;
    private void Awake()
    {

        InitGameStage();

        if (isDebug)
            OnDebug += DebugHandle;
    }
    private void InitGameStage()
    {
        this.RegisterListener(EventID.OnCastCollider, (o) =>
        {
            switch ((GameStage)o)
            {
                case GameStage.StandBy:
                    SetAttachedment(false);
                    break;
                case GameStage.Playing:
                    SetAttachedment(false);
                    SetAttachedment(true);

                    break;
                case GameStage.End:
                    SetAttachedment(false);

                    break;
                default:
                    break;
            }
        });

    }
    private void SetAttachedment(bool isAttach)
    {
        if (isAttach)
        {
            OnCollidedOnceTime += CollidedOnceTimeHandle;
            OnRayCastForward += RaycastForwardHandle;
            OnRaycastDown += RaycastDownHandle;
        }
        else
        {
            OnCollidedOnceTime -= CollidedOnceTimeHandle;
            OnRayCastForward -= RaycastForwardHandle;
            OnRaycastDown -= RaycastDownHandle;
        }
    }
    private void CollidedOnceTimeHandle(Action callback)
    {
        OnCollidedOnceTime -= CollidedOnceTimeHandle;
        callback.Invoke();
    }

    void Update()
    {

        OnDebug?.Invoke();

    }
    private void FixedUpdate()
    {
        OnRayCastForward?.Invoke((hit) => OnRaycastForwardHit(hit));
        OnRaycastDown?.Invoke((hit) => OnRaycastDownHit(hit));

    }
    private void OnRaycastForwardHit(RaycastHit hit)
    {
        if (hit.distance >= _distanceBetweenPlayerAndWall) // chưa chạm
        {

            if (_lastForwardHitted != null)
            {
                if (hit.collider.gameObject.Equals(_lastForwardHitted))
                {
                    hit.collider.gameObject.GetComponent<IHittable>()?.PreCollided();

                }
                else
                {
                    _lastForwardHitted.GetComponent<IHittable>()?.ExitPreCollided();
                    _lastForwardHitted.GetComponent<IAudible>()?.StopAudio();
                }
            }

        }
        else  // đang chạm
        {
            if (_lastForwardHitted != null)
            {
                if (hit.collider.gameObject.Equals(_lastForwardHitted))
                {
                    hit.collider.gameObject.GetComponent<IHittable>()?.Collided();
                    hit.collider.gameObject.GetComponent<IAudible>()?.PlayAudio();
                }
                else
                {
                    _lastForwardHitted.GetComponent<IHittable>()?.ExitCollided();
                    _lastForwardHitted.GetComponent<IAudible>()?.StopAudio();

                }
            }
        }
        _lastForwardHitted = hit.collider.gameObject;

    }
    private void OnRaycastDownHit(RaycastHit hit)
    {
        hit.collider.gameObject.GetComponent<IHittable>()?.Collided();

        if (_lastDownHitted != null && !hit.collider.gameObject.Equals(_lastDownHitted))
        {
            _lastDownHitted.GetComponent<IHittable>()?.ExitCollided();
        }

        _lastDownHitted = hit.collider.gameObject;

    }
    private void RaycastForwardHandle(Action<RaycastHit> callbackRaycastHitHandle)
    {
        RaycastHit hit;
        var rayPos = new Vector3(transform.position.x, transform.position.y + _rayHeight, transform.position.z);

        if (Physics.Raycast(rayPos, Vector3.right, out hit, _rayLenght))
            callbackRaycastHitHandle.Invoke(hit);

    }
    private void RaycastDownHandle(Action<RaycastHit> callbackRaycastHitHandle)
    {
        RaycastHit hit;
        var rayPos = new Vector3(transform.position.x, transform.position.y+_rayHeight, transform.position.z);

        if (Physics.Raycast(rayPos, Vector3.down, out hit, _rayLenght))
            callbackRaycastHitHandle.Invoke(hit);

    }
    private void DebugHandle()
    {
        var forward = Vector3.right * _rayLenght;
        var rayPos = new Vector3(transform.localPosition.x, transform.localPosition.y + _rayHeight, transform.localPosition.z);
        Debug.DrawRay(rayPos, forward, Color.red);

        var down = Vector3.down * 10f;
        var rayPosDown = new Vector3(transform.localPosition.x, transform.localPosition.y + _rayHeight, transform.localPosition.z);
        Debug.DrawRay(rayPosDown, down, Color.blue);
    }

    private void OnTriggerEnter(Collider col)
    {
        col.gameObject.GetComponent<ITriggerable>()?.EnterCollided();
        col.gameObject.GetComponent<IAudible>()?.PlayAudio();

    }
    private void OnTriggerExit(Collider col)
    {
        col.gameObject.GetComponent<ITriggerable>()?.ExitCollided();
        col.gameObject.GetComponent<IAudible>()?.StopAudio();


    }
    private void OnTriggerStay(Collider col)
    {
        col.gameObject.GetComponent<ITriggerable>()?.StayCollided();
        col.gameObject.GetComponent<IAudible>()?.PlayAudio();

    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<ICollidable>()?.EnterCollided(this.transform.position,20f);
    }
}
