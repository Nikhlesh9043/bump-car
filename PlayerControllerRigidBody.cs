using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerRigidBody : MonoBehaviour {

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private int height;
    private int width;

    public NavMeshAgent agent;
	private string MoveInputAxis = "Vertical";
	private string TurnInputAxis = "Horizontal";

    // rotation that occurs in angles per second holding down input
    public float rotationRate = 360;

    // units moved per second holding down move input
    public float moveRate = 100;

    private Rigidbody rb;

    private void Start()
    {
        width = Screen.width;
        height = Screen.height;
        rb = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	private void Update () 
    {
        if (!GameplayManager.instance.gameOver)
        {
            float moveAxis = Input.GetAxis(MoveInputAxis);
            float turnAxis = Input.GetAxis(TurnInputAxis);

            ApplyInput(moveAxis, turnAxis);

            // Touch Controls
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    fp = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    lp = touch.position;

                    Vector3 direction = new Vector3(lp.x - fp.x, 0, lp.y - fp.y);

                    transform.rotation = Quaternion.LookRotation(-direction);

                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    lp = touch.position;

                }
            }
        }
    }

    private void ApplyInput(float moveInput, float turnInput)
    {
        Move(2);
        //Move(moveInput);
        Turn(turnInput);
    }

    private void Move(float input) 
    {
        // Make sure to set drag high so the sliding effect is very minimal (5 drag is acceptable for now)

        // mention this trash function automatically converts to local space
        rb.AddForce(transform.forward * input * moveRate * Time.deltaTime * 30, ForceMode.Force);
    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }
}