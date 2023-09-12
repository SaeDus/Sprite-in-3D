namespace Sprite_in_3D
{
    using UnityEngine;

    public class FaceCam : MonoBehaviour
    {
        private Transform _transform;

        private Camera _mainCam;


        private void Awake()
        {
            _transform = transform;
            _mainCam = Camera.main;
        }

        private void Update()
        {
            var faceDir = _mainCam.transform.forward;
            faceDir.y = 0;
            _transform.forward = faceDir;
        }
    }
}