using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    Player playerScr;
    PickUpObject pickScr;

    [SerializeField] LayerMask isInteractable;
    GameObject interactedO;

    public bool cuerdaActivado;
    public bool presionActivado;
    int cuerdaId;
    int presionId;

    [SerializeField] LlavePresion llaveScr;

    public static InteractionHandler iHandler;

    Transform body;

    private void OnEnable()
    {
        EventManager.OnCuerdaTriggerEnter += CuerdaOn;
        EventManager.OnCuerdaTriggerExit += CuerdaOff;

        EventManager.OnPresionTriggerEnter += PresionOn;
        EventManager.OnPresionTriggerExit += PresionOff;
    }

    private void OnDisable()
    {
        EventManager.OnCuerdaTriggerEnter -= CuerdaOn;
        EventManager.OnCuerdaTriggerExit -= CuerdaOff;

        EventManager.OnPresionTriggerEnter -= PresionOn;
        EventManager.OnPresionTriggerExit -= PresionOff;
    }

    private void Awake()
    {
        if (iHandler == null)
        {
            iHandler = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerScr = GetComponent<Player>();
        pickScr = GetComponent<PickUpObject>();

        body = transform.GetChild(0);
        //llaveScr = llaveGo.GetComponent<LlavePresion>();
    }

    void Update()
    {
        Interact();
        //if (pickScr.heldObject != null)
        //{
        //    pickScr.MovePickObject();
        //}
    }

    public void CuerdaOn(int uwu)
    {
        cuerdaId = uwu;
        cuerdaActivado = true;
        Debug.Log("cuerda!");
    }
    void CuerdaOff(int uwu)
    {
        cuerdaActivado = false;
    }

    public void PresionOn(int uwu)
    {
        Debug.LogError(uwu);    
        presionId = uwu;
        Debug.LogError(presionId);   

        presionActivado = true;
    }
    public void PresionOff(int uwu)
    {
        presionActivado = false;
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (cuerdaActivado)
            {
                Debug.Log("acceso");
                EventManager.DoorTriggerEnter(cuerdaId);
            }

            if (presionActivado)
            {
                Debug.Log("presion");
                llaveScr.Alternate(presionId);
            }

            Collider[] colls = Physics.OverlapSphere(body.position + transform.forward, 1f, isInteractable);
            if (colls.Length != 0)
            {
                for (int i = 0; i < colls.Length; i++)
                {
                    interactedO = colls[i].gameObject;

                    if (colls[i].CompareTag("PowerCell"))
                    {
                        if (!playerScr.powerCells)
                        {
                            playerScr.powerCells = true;
                        }
                    }

                    else if (colls[i].CompareTag("Generator"))
                    {
                        Generator generatorScr = interactedO.GetComponent<Generator>();
                        if (playerScr.powerCells)
                        {
                            generatorScr.Activate();
                            playerScr.powerCells = false;
                        }
                        else
                        {
                            //mensaje o algo
                        }
                    }

                    else if (colls[i].CompareTag("Actioner"))
                    {
                        Actioner actionerScr = interactedO.GetComponent<Actioner>();

                        if (!actionerScr.activated)
                        {
                            actionerScr.Activate();
                        }
                        else
                        {
                            actionerScr.Deactivate();
                        }
                    }

                    else if (colls[i].CompareTag("CajaCoger"))
                    {
                        Debug.LogError("coger");
                        if (pickScr.heldObject == null)
                        {
                            pickScr.PickUpObjects(colls[i].gameObject);
                            Debug.Log(pickScr.heldObject);
                        }
                        else
                        {
                            pickScr.DropObjects();
                        }
                    }

                    else if (colls[i].CompareTag("NPC"))
                    {
                        Npc npcScr = interactedO.GetComponent<Npc>();
                    }
                }
            }
        }
    }
}
