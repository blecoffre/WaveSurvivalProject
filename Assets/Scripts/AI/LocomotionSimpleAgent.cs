using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class LocomotionSimpleAgent : MonoBehaviour
{

    private static int ANIMATOR_PARAM_WALK_SPEED =
        Animator.StringToHash("walkSpeed");

    private Animator _animator;
    private NavMeshAgent _agent;

    private void Awake()
    {
        this._animator = this.GetComponent<Animator>();
        this._agent = this.GetComponent<NavMeshAgent>();
    }

    private void LateUpdate()
    {
        float speed = this._agent.velocity.magnitude;
        this._animator.SetFloat(ANIMATOR_PARAM_WALK_SPEED, speed);
    }

    //Animator anim;
    //NavMeshAgent agent;
    //Vector2 smoothDeltaPosition = Vector2.zero;
    //Vector2 velocity = Vector2.zero;

    //void Start()
    //{
    //    anim = GetComponent<Animator>();
    //    agent = GetComponent<NavMeshAgent>();
    //    // Don’t update position automatically
    //    agent.updatePosition = false;
    //}

    //void Update()
    //{
    //    Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

    //    // Map 'worldDeltaPosition' to local space
    //    float dx = Vector3.Dot(transform.right, worldDeltaPosition);
    //    float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
    //    Vector2 deltaPosition = new Vector2(dx, dy);

    //    // Low-pass filter the deltaMove
    //    float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
    //    smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

    //    // Update velocity if time advances
    //    if (Time.deltaTime > 1e-5f)
    //        velocity = smoothDeltaPosition / Time.deltaTime;

    //    bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

    //    if (dx >= -0.001 && dx <= 0.001f)
    //    {
    //        Mathf.Clamp(velocity.x, 0, 10);
    //    }

    //    DXMoy(dx);

    //    // Update animation parameters
    //    anim.SetBool("move", shouldMove);
    //    anim.SetFloat("x", velocity.x);
    //    anim.SetFloat("y", velocity.y);

    //    //LookAt lookAt = GetComponent<LookAt>();
    //    //if (lookAt)
    //    //    lookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;

    //    // Pull agent towards character
    //    //if (worldDeltaPosition.magnitude > agent.radius)
    //    //agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;

    //    // Pull character towards agent
    //    if (worldDeltaPosition.magnitude > agent.radius)
    //        transform.position = agent.nextPosition - 0.9f * worldDeltaPosition;
    //}

    //private List<float> dxMoy = new List<float>();
    //private void DXMoy(float newVal)
    //{
    //    if(dxMoy != null)
    //    {
    //        dxMoy.Add(newVal);
    //        if(dxMoy.Count > 50)
    //        {
    //            dxMoy.RemoveAt(0);
    //        }
    //    }

    //    float moy = 0;

    //    for (int i = 0; i < dxMoy.Count; i++)
    //    {
    //        moy += dxMoy[i];
    //    }

    //    moy = moy / dxMoy.Count;

    //    Debug.Log(moy);
    //}

    //void OnAnimatorMove()
    //{
    //    // Update position based on animation movement using navigation surface height
    //    Vector3 position = anim.rootPosition;
    //    position.y = agent.nextPosition.y;
    //    transform.position = position;
    //}
}