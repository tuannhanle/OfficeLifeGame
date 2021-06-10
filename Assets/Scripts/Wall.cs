using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Observer;

public enum WallType { Task, Meeting, Feedback, ChangeBrief }
public class Wall : MonoBehaviour, IHittable, IAudible
{
    [SerializeField] private WallType _wallType;
    [SerializeField] private string _animationCollided;
    private float _waitTime;

    [SerializeField] private TMP_Text _textMeshPro;
    private int _tempCount;
    private Collider _collider;
    private AudioSource _audioSource;
    private GameObject _checkmark;
    private Action<Action> OnCollidedOnceTime;

    [HideInInspector]
    public WallController wallControllerDaddy;


    // Start is called before the first frame update
    void Awake()
    {

        OnCollidedOnceTime += CollidedOnceTimeHandle;

        #region switch case

        //switch (_wallType)
        //{
        //    case WallType.Task:
        //        this._waitTime = 3;
        //        break;
        //    case WallType.Meeting:
        //        this._waitTime = 4;

        //        break;
        //    case WallType.Feedback:
        //        this._waitTime = 5;

        //        break;
        //    case WallType.ChangeBrief:
        //        this._waitTime = 6;

        //        break;
        //    default:
        //        break;
        //}
        #endregion

        _collider = GetComponent<Collider>();
        _audioSource = GetComponentInChildren<AudioSource>();
        _checkmark = GetComponentInChildren<CheckMarkAnimator>().gameObject;
        _checkmark.SetActive(false);

        

    }

    public void LemmeFadeOut()
    {
        _collider.enabled = false;
    }
    public void SetWaitTime(float value)
    {
        _waitTime = value + UnityEngine.Random.Range(-0.5f,1f);
        PlayingStage();
    }

    public void Collided()
    {
        _checkmark.SetActive(true);
        this.PostEvent(EventID.OnCostEnergy);
        OnCollidedOnceTime?.Invoke(() =>
        {
            _checkmark.SetActive(true);

            GetComponent<Animator>().Play(_animationCollided);
            this.PostEvent(EventID.OnCastAnim, PlayerAnimState.Idle);
            this.PostEvent(EventID.OnCastMovementState, PlayerMomvementState.Idle);
        });



        Action overtimeCallback = () =>
           {
               StopAudio();
               this.PostEvent(EventID.OnCastAnim, PlayerAnimState.Run);
               this.PostEvent(EventID.OnCastMovementState, PlayerMomvementState.Run);

               wallControllerDaddy.ChildCallMe();

               this.gameObject.SetActive(false);
           };

        WaitTime(overtimeCallback);

    }
    public void PreCollided()
    {

        _checkmark.SetActive(true);
    }
    public void ExitCollided()
    {
        OnCollidedOnceTime += CollidedOnceTimeHandle;

        //this.PostEvent(EventID.OnCastAnim, PlayerAnim.Idle);
        //this.PostEvent(EventID.OnCastMovementState, PlayerMomvementState.Idle);

        

        _checkmark.SetActive(false);

    }
    public void ExitPreCollided()
    {
        //OnCollidedOnceTime += CollidedOnceTimeHandle;



        _checkmark.SetActive(false);
    }
    private void CollidedOnceTimeHandle(Action callback)
    {
        OnCollidedOnceTime -= CollidedOnceTimeHandle;
        callback.Invoke();
    }
    private bool IsTimeOver()
    {
        if (_waitTime >= 0.01f)
        {
            _waitTime -= Time.deltaTime*2f;
            PlayingStage();
            return false;
        }
        else
        {
            return true;
        }

    }
    private void WaitTime(Action overtimeCallback)
    {
        if (IsTimeOver())
        {
            overtimeCallback.Invoke();
        }
    }
    private void PlayingStage()
    {
        _tempCount = (int)(_waitTime * 10f);
        _textMeshPro.text = _tempCount.ToString();
    }

    public void PlayAudio()
    {
        if (!_audioSource.isPlaying && this.gameObject.active)
        {
            _audioSource.Play();

        }
    }

    public void StopAudio()
    {
        _audioSource.Stop();
    }
}

