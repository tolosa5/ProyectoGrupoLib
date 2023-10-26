using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMov : MonoBehaviour
{
    #region Variables

    Rigidbody rb;
    PlayerInteractions plInteractions;

    #region Movement
    [Header("Movement")]

    Vector3 dirMov;
    public float h;
    public float v;
    [SerializeField] int speed;
    float plDrag;

    #endregion

    #region Jump
    [Header("Jump")]

    [SerializeField] Transform feet;
    [SerializeField] float feetRadius;
    [SerializeField] LayerMask onGround;
    [SerializeField] int jumpForce;
    float floatingTime;
    float jumpTimer;
    bool jumping;

    Vector3 currentGravity;

    #endregion

    #region Rotation
    [Header("Rotation")]

    Transform body;
    Vector3 bodyEuler;

    #endregion

    #region WallJump
    [Header("WallJump")]

    [SerializeField] Transform wallCheck;
    bool isOnWallR;
    bool isOnWallL;
    bool inputs = true;
    int facingDirection;
    [SerializeField] Vector3 wallJumpDirection;

    float dragTimer;
    float jumpHoldTimer;

    #endregion

    #region Pick
    [Header("Pick")]

    PickUpObject pickScr;

    #endregion

    #endregion

    public static RigidBodyMov rbMov;

    public enum State { Normal, Jumping, Gliding, WallSlide, HoldingObjJump };
    public State state;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        plDrag = rb.drag;
        plInteractions = GetComponent<PlayerInteractions>();
        pickScr = GetComponent<PickUpObject>();
        currentGravity = Physics.gravity;
        body = transform.GetChild(0);
        bodyEuler = body.eulerAngles;

        if (rbMov == null)
        {
            rbMov = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x, wallCheck.position.y, wallCheck.position.z + 0.3f));
        Gizmos.DrawWireSphere(feet.position, feetRadius);
    }

    private void Update() 
    {
        
        Debug.Log(state);

        switch (state)
        {
            default:
            case State.Normal:
                Movement();
                Rotation();
                ToNormal();

                break;

            case State.Jumping:
                jumpTimer += Time.deltaTime;
                Movement();
                Rotation();
                WallDetect();
                GroundDetect();
                StartGliding();

                break;

            case State.Gliding:
                WallDetect();
                GroundDetect();
                GliderController.gliderInstance.WindDrag();
                //GliderController.gliderInstance.Rotation();
                StopGliding();
                Movement();
                Rotation();

                break;

            case State.WallSlide:

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    h = Input.GetAxisRaw("Horizontal");
                    Debug.LogWarning(h);
                    Jump();
                }

                dragTimer += Time.deltaTime * 10;
                DragCalculator();
                GroundDetect();

                break;

            case State.HoldingObjJump:

                jumpTimer += Time.deltaTime;
                Movement();
                Rotation();
                GroundDetect();

                break;

        }

        if (Input.GetKeyDown(KeyCode.Space) && state != State.WallSlide)
        {
            Jump();
        }
    }

    void ToNormal()
    {
        rb.drag = plDrag;
        dragTimer = 0;
        jumpTimer = 0;
        Physics.gravity = currentGravity;
    }

    void Movement()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        dirMov = new Vector3(0, 0, h);
    }

    private void FixedUpdate() 
    {
        #region Mov

        if (Mathf.Abs(h) > 0)
        {
            rb.AddForce(dirMov * speed);
            //Debug.Log(rb.velocity);

            if (Mathf.Abs(rb.velocity.z) <= 5)
            {
                if (h > 0 && !plInteractions.onWindArea)
                {
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 5);
                }
                else if (h < 0 && !plInteractions.onWindArea)
                {
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -5);
                }
            }

            if (Mathf.Abs(rb.velocity.z) >= 10)
            {
                if (h > 0 && !plInteractions.onWindArea)
                {
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 10);

                }
                else if (h < 0 && !plInteractions.onWindArea)
                {
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -10);
                }
            }
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }
        #endregion
    }

    void Jump()
    {
        if (state == State.WallSlide)
        {
            rb.drag = plDrag;
            dragTimer = 0;

            if (isOnWallR)
            {
                Debug.Log("saltoPared");
                Vector3 direction = new Vector3(0, wallJumpDirection.y, wallJumpDirection.x * 1);
                rb.AddForce(direction, ForceMode.Impulse);
                body.eulerAngles = new Vector3(bodyEuler.x, bodyEuler.y, 0);
            }
            else
            {
                Vector3 direction = new Vector3(0, wallJumpDirection.y, wallJumpDirection.x * -1);
                rb.AddForce(direction, ForceMode.Impulse);
                body.eulerAngles = new Vector3(bodyEuler.x, bodyEuler.y, 180);
            }

            state = State.Normal;
            Invoke("CambioEstadoDelay", 0.2f);
        }

        Collider[] colls = Physics.OverlapSphere(feet.position, feetRadius, onGround);
        if (colls.Length != 0)
        {
            Debug.Log("saltoNormal");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (pickScr.heldObject == null)
            {
                state = State.Jumping;

            }
            else
            {
                state = State.HoldingObjJump;
            }
        }
    }
    
    void CambioEstadoDelay()
    {
        state = State.Jumping;
    }

    void StartGliding()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = State.Gliding;
            //hacer la animacion
        }
    }

    void StopGliding()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = State.Normal;
            //hacer la animación
        }
    }

    void GroundDetect()
    {
        if (jumpTimer > 0.2)
        {
            Collider[] colls = Physics.OverlapSphere(feet.position, feetRadius, onGround);
            if (colls.Length != 0)
            {
                state = State.Normal;
            }
        }
    }

    void Rotation()
    {
        if (h > 0)
        {
            body.eulerAngles = new Vector3(bodyEuler.x, bodyEuler.y, 0);
            facingDirection = 1;
        }
        else if (h < 0)
        {
            body.eulerAngles = new Vector3(bodyEuler.x, bodyEuler.y, 180);
            facingDirection = -1;
        }
    }

    #region WallJump

    void WallDetect()
    {
        isOnWallR = Physics.Raycast(wallCheck.position, Vector3.forward, 0.5f, onGround);
        isOnWallL = Physics.Raycast(wallCheck.position, Vector3.back, 0.5f, onGround);
        
        if (isOnWallR || isOnWallL)
        {
            WallSliding();
        }
    }

    void WallSliding()
    {
        Debug.Log("walljumping");
        state = State.WallSlide;
        rb.drag = 20;
    }

    void DragCalculator()
    {
        if (rb.drag > plDrag)
        {
            rb.drag = 20 - dragTimer;
        }
        else
        {
            rb.drag = plDrag;
        }
    }

    void WallExit()
    {
        state = State.Normal;
    }

    #endregion
}
