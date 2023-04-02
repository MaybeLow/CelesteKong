//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using static UnityEngine.EventSystems.EventTrigger;

//public class MovingPlatformOld : MonoBehaviour, IPEntity
//{
//    private PlatformCommandController controller;

//    protected Rigidbody2D rb;
//    protected SpriteRenderer sr;
//    protected BoxCollider2D bc;

//    private Vector2 moveDirection = Vector2.right;

//    Rigidbody2D IPEntity.rb => rb;

//    [SerializeField] private Vector2 velocity;

//    private Vector2 startingPosition;

//    private Vector2 endingPosition;

//    private float timeSkipTimer = 0f;
//    private float timeSkipTimer2 = 0f;

//    private bool isTeleporting = false;

//    private bool commandListIsNegative = false;

//    [SerializeField] private float startDelay = 2.0f;
//    [SerializeField] private float endDelay = 5.0f;

//    private void Start()
//    {
//        startingPosition = transform.position;
//        endingPosition = Vector2.zero;
//        if (startDelay != 0f)
//        {
//            StartCoroutine(StartDelayCoroutine());
//        }
//    }

//    public void SkipTime()
//    {
//        if (isTeleporting)
//        {
//            //Debug.Log(timeSkipTimer);
//            if (!GameManager.UndoActive())
//            {
//                timeSkipTimer += Time.deltaTime;
//            }
//            else
//            {
//                timeSkipTimer -= Time.deltaTime;
//            }

//            if (timeSkipTimer > endDelay)
//            {
//                rb.position = startingPosition;
//                EnablePlatform();
//                isTeleporting = false;
//            } 
//            else if (timeSkipTimer < 0f)
//            {
//                rb.position = endingPosition;
//                EnablePlatform();
//                isTeleporting = false;
//            }
//        }
//    }

//    public void StartTeleport()
//    {
//        isTeleporting = true;
//        if (GameManager.UndoActive())
//        {
//            timeSkipTimer = endDelay;
//        } else
//        {
//            timeSkipTimer = 0;
//        }
//    }

//    public void Teleport()
//    {
//        if (endingPosition == Vector2.zero)
//        {
//            endingPosition = transform.position;
//        }
//        DisablePlatform();
//        StartTeleport();
//    }

//    public bool IsTeleporting()
//    {
//        return isTeleporting;
//    }

//    public void DisablePlatform()
//    {
//        rb.velocity = Vector2.zero;
//        sr.enabled = false;
//        bc.enabled = false;
//    }

//    public void EnablePlatform()
//    {
//        rb.velocity = velocity;
//        sr.enabled = true;
//        bc.enabled = true;
//    }

//    private IEnumerator StartDelayCoroutine()
//    {
//        DisablePlatform();

//        yield return new WaitForSeconds(startDelay);

//        EnablePlatform();
//    }

//    private void Awake()
//    {
//        sr = GetComponent<SpriteRenderer>();
//        rb = GetComponent<Rigidbody2D>();
//        controller = GetComponent<PlatformCommandController>();
//        bc = GetComponent<BoxCollider2D>();
//    }

//    // Update is called once per frame
//    private void FixedUpdate()
//    {
//        UpdateCommandMovement();
//        DisableIfNoCommandsLeft();
//        SkipTime();
//        StopIfNoCommands();
//    }

//    private void StopIfNoCommands()
//    {
//        if (controller.IsEmpty() && commandListIsNegative == false && GameManager.UndoActive())
//        {
//            DisablePlatform();
//            timeSkipTimer2 = 0f;
//            commandListIsNegative = true;
//        }
//    }

//    private void DisableIfNoCommandsLeft()
//    {
//        if (commandListIsNegative)
//        {
//            //Debug.Log(timeSkipTimer2);
//            if (!GameManager.UndoActive())
//            {
//                timeSkipTimer2 -= Time.deltaTime;
//            }
//            else
//            {
//                timeSkipTimer2 += Time.deltaTime;
//            }

//            if (timeSkipTimer2 < 0f)
//            {
//                EnablePlatform();
//                commandListIsNegative = false;
//            }
//        }
//    }

//    private void UpdateCommandMovement()
//    {
//        if (sr.enabled)
//        {
//            if (GameManager.UndoActive())
//            {
//                UpdateUndo();
//            }
//            else
//            {
//                UpdateMovement();
//            }
//        }
//    }

//    private void UpdateMovement()
//    {
//        controller.ExecuteCommand(new PlatformMoveCommand(this, Time.timeSinceLevelLoad, moveDirection));
//    }

//    private void UpdateUndo()
//    {
//        if (!controller.IsEmpty())
//        {
//            controller.UndoCommand();
//        } 
//    }

//    public Vector2 GetVelocity()
//    {
//        return rb.velocity;
//    }
//}
