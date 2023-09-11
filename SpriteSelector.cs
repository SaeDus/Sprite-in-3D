namespace Sprite_in_3D
{
    using UnityEngine;

    public class SpriteSelector : MonoBehaviour
    {
        private Transform _transform;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Camera _mainCam;

        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int IdleHorizontal = Animator.StringToHash("IdleHorizontal");
        private static readonly int IdleVertical = Animator.StringToHash("IdleVertical");
        private static readonly int Jumping = Animator.StringToHash("Jumping");
        private static readonly int Attack = Animator.StringToHash("Attack");

        private bool _isJumping;

        public bool IsJumping
        {
            get => _isJumping;

            set
            {
                if (value == _isJumping) return;

                _isJumping = value;

                SetJumpingBool();
            }
        }


        private void Awake()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _mainCam = Camera.main;
        }

        private void Update()
        {
            GetMovementInput();
            HandleIdleAnimations();
        }


        private void GetMovementInput()
        {
            _animator.SetFloat(Horizontal, Input.GetAxis("Horizontal"));
            _animator.SetFloat(Vertical, Input.GetAxis("Vertical"));
        }


        private void HandleIdleAnimations()
        {
            var characterForward = _transform.forward;
            var camForward = _mainCam.transform.forward;

            _animator.SetFloat(IdleHorizontal, GetHorizontalDirection(characterForward, camForward));

            _animator.SetFloat(IdleVertical, Vector3.Dot(characterForward, camForward));
        }


        private float GetHorizontalDirection(Vector3 characterForward, Vector3 camForward)
        {
            var targetDir = characterForward - camForward;

            var crossProd = Vector3.Cross(targetDir, camForward);

            return Vector3.Dot(Vector3.down, crossProd);
        }


        private void SetJumpingBool()
        {
            _animator.SetBool(Jumping, _isJumping);
        }


        public void DoAttackAnimation()
        {
            _animator.SetTrigger(Attack);
        }
    }
}