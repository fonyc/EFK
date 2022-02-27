using EFK.Animations;
using EFK.Stats;
using UnityEngine;

namespace EFK.Control
{
    public class PlayerController : MonoBehaviour
    {
        InputActions inputActions;
        PlayerAnimations anim;
        private CharacterController controller;
        private Vector3 playerVelocity;

        private float gravity = -9.8f;

        [SerializeField] private float playerSpeed = 2.0f;


        private void Awake()
        {
            inputActions = new InputActions();
            anim = GetComponent<PlayerAnimations>();
        }

        private void Start()
        {
            controller = gameObject.GetComponent<CharacterController>();
        }

        void Update()
        {
            Vector2 inputMove = inputActions.Player.Move.ReadValue<Vector2>();
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
            if (inputActions.Player.Interact.triggered)
            {
                Debug.Log("Interact!");
            }
        }

        #region ENABLE/DISABLE
        private void OnEnable()
        {
            inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            inputActions.Player.Disable();
        }
        #endregion

    }

}
