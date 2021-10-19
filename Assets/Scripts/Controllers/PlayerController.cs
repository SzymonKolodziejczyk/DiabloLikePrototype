using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public Interactable focus;

    public LayerMask movementMask;
    public float dashDistance = 5f;

    Camera cam;
    PlayerMotor motor;
    CharacterCombat combat;

    private void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update () {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        // Movement
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, movementMask))
            {
                // Move player to where was clicked
                motor.MoveToPoint(hit.point);

                RemoveFocus();
                // Stop focus
            }
        }


        // Dash
        if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("dash");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                Vector3 dashPoint = (hit.point - transform.position).normalized * dashDistance + transform.position;

                motor.Warp(dashPoint);

                RemoveFocus();
                // Stop focus
            }
        }

        // Interact
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Physics.OverlapSphere()
            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                
                if(interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

    }

    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
            motor.FollowTarget(focus);
            //motor.MoveToPoint(focus.transform.position);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
}
