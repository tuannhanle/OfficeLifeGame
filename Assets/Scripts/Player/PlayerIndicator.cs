using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame
{
    public class PlayerIndicator : MonoBehaviour
    {
        private Transform _energyCanvas;
        private Transform _mainCamera;



        // Start is called before the first frame update
        void Awake()
        {
            _energyCanvas = GetComponentInChildren<Canvas>().transform;
            _mainCamera = Camera.main.transform;
            if (_mainCamera)
            {
                Vector3 directionToTarget = _mainCamera.position - _energyCanvas.transform.position;
                _energyCanvas.transform.rotation = Quaternion.LookRotation(-directionToTarget, _mainCamera.up);
            }
        }

        void Update()
        {
            //if (_mainCamera)
            //{
            //    Vector3 directionToTarget = _mainCamera.position - _energyCanvas.transform.position;
            //    _energyCanvas.transform.rotation = Quaternion.LookRotation(-directionToTarget, _mainCamera.up);
            //}
        }

    }
}