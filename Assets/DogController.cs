using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogController : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Goal;
    public GameObject GameManager;
    private GameObject target;
    NavMeshAgent navmeshagent;
    public GameObject child;


    // Start is called before the first frame update
    void Start()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        target = Ball;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetComponent<GameManager>().state == 0)
        {
            child = null;
        }
        if (GameManager.GetComponent<GameManager>().state == 1)
        {
            child = null;
            Retrieve();
            if ((target == Goal) && ((Vector3.Distance(this.transform.position,Goal.transform.position) < 1.0f)))
            {
                child = transform.GetChild(1).gameObject;
                child.transform.parent = null;
                child.transform.position = new Vector3(0, 0.1f, -26.0f);
                target = Ball;
            }
        }
        if (GameManager.GetComponent<GameManager>().state == 2)
        {
            child = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == Ball && !(other.gameObject.tag == "GoalTag"))
        {
            if (other.gameObject.tag == "BallTag")
            {
                Ball.GetComponent<Rigidbody>().isKinematic = true;
            }
            if (other.gameObject.tag == "BallTag" || other.gameObject.tag == "BarTag" || other.gameObject.tag == "CanTag")
            {
                target = Goal;
                other.gameObject.transform.parent = this.gameObject.transform;
                other.gameObject.transform.localPosition = new Vector3(0, 0, 0.7f);
            }
        }
    }

    private void Retrieve()
    {
        navmeshagent.SetDestination(target.transform.position);
    }
}
