using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Checkers
{
    public class CameraRotate : MonoBehaviour
    {
        [SerializeField] private GameObject _eventSystem;
        [SerializeField] protected Transform _whiteSide;
        [SerializeField] protected Transform _blackSide;
        private float _speed = 2.7f;
        private Camera mainCam;


        private void Awake()
        {
            mainCam = GetComponentInChildren<Camera>();
        }


        public IEnumerator Rotate()
        {
            Transform currentTransform;
            if (Handler.YourColorChekers == ColorType.White)
            {
                currentTransform = _whiteSide;
            }
            else
            {
                currentTransform = _blackSide;
            }
            _eventSystem.SetActive(false);
            while (mainCam.transform.rotation != currentTransform.localRotation)
            {
                mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, currentTransform.rotation, _speed * Time.deltaTime);
                mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, currentTransform.position, _speed * Time.deltaTime);
                yield return null;
            }
            _eventSystem.SetActive(true);
        }

       

    }
}