using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base class for all agent characters; players and NPCs
/// </summary>
public class AgentCharacter : MonoBehaviour
{
    public bool PointNClickMouse = false;
    protected NavMeshAgent agent => GetComponent<NavMeshAgent>();
    protected Animator animator => GetComponent<Animator>();

    public void SetAgentDest(Vector3 position)
    {
        agent.SetDestination(position);
    }
    protected virtual void Awake() {}

    protected virtual void Update() 
    {
        animator.SetFloat("AgentVelocityMagnitude", agent.velocity.magnitude);

        //mouse controls
        if (PointNClickMouse && Input.GetMouseButtonDown(0))        
            NavigateToDestination(Input.mousePosition);        
    }

    private void NavigateToDestination(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(hit.point, hit.point + Vector3.up, Color.green, .1f);

            agent.SetDestination(hit.point);
        }
    }
}
