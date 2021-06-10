using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerLevelState { Intern, Fresher, Junior, Senior}

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private GameObject _case, _lightBulb, _pencil;
    [SerializeField] private GameObject _blankMat, _internMat, _fresherMat, _juniorMat, _seniorMat;



    public void SetItOn(PlayerLevelState playerLevelState)
    {
        _blankMat.SetActive(false); _internMat.SetActive(false); _fresherMat.SetActive(false); _juniorMat.SetActive(false); _seniorMat.SetActive(false);
        switch (playerLevelState)
        {
            case PlayerLevelState.Intern:
                _internMat.SetActive(true);

                _case.SetActive(false);
                _lightBulb.SetActive(false);
                _pencil.SetActive(false);
                break;
            case PlayerLevelState.Fresher:
                _fresherMat.SetActive(true);

                _case.SetActive(false);
                _lightBulb.SetActive(false);
                _pencil.SetActive(true);
                break;
            case PlayerLevelState.Junior:
                _juniorMat.SetActive(true);

                _case.SetActive(false);
                _lightBulb.SetActive(true);
                _pencil.SetActive(true);
                break;
            case PlayerLevelState.Senior:
                _seniorMat.SetActive(true);

                _case.SetActive(true);
                _lightBulb.SetActive(true);
                _pencil.SetActive(true);
                break;
            default:
                break;
        }

    }

}
