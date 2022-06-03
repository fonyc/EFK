using EFK.Animations;
using EFK.Stats;
using UnityEngine;

namespace EFK.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Interactor interactor;
        InputActions inputActions;
        PlayerAnimations anim;
        private CharacterController controller;
        private Vector3 playerVelocity;

        private float gravity = -9.8f;

        [SerializeField] private float playerSpeed = 2.0f;

        public InputActions InputActions { get => inputActions; set => inputActions = value; }

        private void Awake()
        {
            InputActions = new InputActions();
            anim = GetComponent<PlayerAnimations>();
            controller = gameObject.GetComponent<CharacterController>();
            interactor = GetComponent<Interactor>();
        }

        void Update()
        {
            Vector2 inputMove = InputActions.Player.Move.ReadValue<Vector2>();

            //Given that inputActions has a threshold, normalize vector to always move at the same speed
            inputMove.Normalize();

            Vector3 move = new Vector3(inputMove.x, 0f, inputMove.y);
            anim.Walk(inputMove);
            controller.Move(move * Time.deltaTime * playerSpeed);

            //Puts character front in the vector direction 
            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            playerVelocity.y = gravity;
            controller.Move(playerVelocity * Time.deltaTime);

            Interact();
        }

        private void Interact()
        {
            if (InputActions.Player.Interact.triggered)
            {
                if (interactor.TARGET == null) return;
                
                interactor.TARGET.Interact(GetComponent<BaseStats>());
            }
        }

        #region ENABLE/DISABLE
        private void OnEnable()
        {
            InputActions.Player.Enable();
        }

        private void OnDisable()
        {
            InputActions.Player.Disable();
        }
        #endregion

    }

}
