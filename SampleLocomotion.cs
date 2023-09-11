namespace Sprite_in_3D
{
    using UnityEngine;

    public class SampleLocomotion : MonoBehaviour
    {
        [SerializeField, Range(2.0f, 10.0f)] private float moveSpeed = 6.0f;
        [SerializeField, Range(2.0f, 5.0f)] private float jumpSpeed = 3.5f;
        [SerializeField, Range(0.1f, 1.0f)] private float jumpDelay = 1.0f;
        [SerializeField, Range(5.0f, 20.0f)] private float gravity = 12.0f;


        private Transform _transform;
        private CharacterController _controller;
        private SpriteSelector _spriteSelector;
        private Camera _mainCam;


        private Vector3 _movement;
        private float _jumpTimer;
        private bool _hasLanded;


        private void Awake()
        {
            _transform = transform;
            _controller = GetComponent<CharacterController>();
            _spriteSelector = GetComponent<SpriteSelector>();
            _mainCam = Camera.main;
        }

        private void Update()
        {
            HandleMovement();
            HandleJumpTimer();

            if (!Input.GetButtonDown("Fire1"))
                return;

            _spriteSelector.DoAttackAnimation();
        }


        private void HandleMovement()
        {
            if (!_controller.isGrounded)
                return;

            _spriteSelector.IsJumping = false;

            if (!_hasLanded)
            {
                _jumpTimer = jumpDelay;
                _hasLanded = true;
            }

            _movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            _movement = _mainCam.transform.TransformDirection(_movement);
            _movement.y = 0;

            if (_movement != Vector3.zero)
                _transform.forward = _movement;

            if (Input.GetButton("Jump") && _jumpTimer <= 0)
            {
                _movement.y = jumpSpeed;

                _spriteSelector.IsJumping = true;

                _hasLanded = false;
            }
        }


        private void HandleJumpTimer()
        {
            if (_jumpTimer <= 0)
                return;

            _jumpTimer -= Time.deltaTime;
        }
    }
}