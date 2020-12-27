using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Pathing : MonoBehaviour
{
    public Transform[] points;
    [SerializeField] GameObject path = default;
    [SerializeField] NavMeshAgent agent = default;
    [SerializeField] int destPoint = 0;
    [SerializeField] float viewRadius = 10f;
    [SerializeField] float enemyViewAngle = 30f;
    [SerializeField] float maxDistance = 3f;

    Enemy enemy;
    bool isChasing;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();

        points = path.GetComponentsInChildren<Transform>();

        List<Transform> tList = points.ToList();
        tList.RemoveAt(0);
        points = tList.ToArray();

        agent.autoBraking = false;

        GoToNextPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
            GoToNextPoint();

        Collider[] hits = Physics.OverlapSphere(transform.position, viewRadius);

        if (hits != null)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                Player player = hits[i].GetComponent<Player>();

                if (player != null)
                {
                    float angle = Vector3.Angle(transform.forward, player.transform.position - transform.position);

                    if (angle < enemyViewAngle)
                    {
                        float distance = Vector3.Distance(player.transform.position, transform.position);

                        agent.speed = 5f;
                        agent.destination = (player.transform.position);

                        Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - transform.position);
                        transform.rotation = lookRotation;

                        if (distance <= maxDistance)
                        {
                            enemy.DealDamageTo(player);

                            agent.isStopped = true;
                        }
                        else
                            agent.isStopped = false;
                    }
                }
                else
                {
                    agent.isStopped = false;
                    agent.speed = 3f;
                    agent.destination = points[destPoint].position;
                }
            }
        }
    }

    void GoToNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
        Debug.Log($"Destination: {points[destPoint].gameObject.name}");
    }
}
