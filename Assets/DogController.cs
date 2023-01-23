using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogController : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Goal;
    public GameObject HouseKey;
    public GameObject GameManager;
    private GameObject target;
    NavMeshAgent navmeshagent;
    public GameObject child;
    public GameObject Camera;
    private Vector3 lookAtTarget;
    private int count = 0;
    private Vector3 myposition;
    private Vector3 finalposition;
    private float time= 0f;
    private int finalcount = 0;
    private int particlecount = 0;


    // Start is called before the first frame update
    void Start()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        target = Ball;
        myposition = this.transform.position;
        finalposition = this.transform.position;
        finalposition.y = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetComponent<GameManager>().state == (State)0)
        {
            child = null;
        }
        else if (GameManager.GetComponent<GameManager>().state == (State)1)
        {
            child = null;
            Retrieve();
            if ((target == Goal) && ((Vector3.Distance(this.transform.position, Goal.transform.position) < 1.0f)))
            {
                child = transform.GetChild(1).gameObject;
                child.transform.parent = null;
                child.transform.position = new Vector3(0, 0.1f, -26.5f);
                target = Ball;
            }
        }
        else if ((GameManager.GetComponent<GameManager>().state >= (State)2) && (GameManager.GetComponent<GameManager>().state <= (State)10))
        {
            child = null;
        }
        else if (GameManager.GetComponent<GameManager>().state == (State)11)     //正面にむける
        {
            lookAtTarget = Camera.transform.position;
            lookAtTarget.y = this.transform.position.y;
            this.transform.LookAt(lookAtTarget);
        }
        else if (GameManager.GetComponent<GameManager>().state == State.BringTheKey)  //鍵を見つけてくる
        {
            if (count == 0)
            {
                target = HouseKey;
                count = 1;
            }
            if ((target == HouseKey) || (target == Goal))
            {
                Retrieve();
            }
            if ((target == Goal) && ((Vector3.Distance(this.transform.position, Goal.transform.position) < 1.0f)))
            {
                child = transform.GetChild(1).gameObject;
                child.transform.parent = null;
                child.transform.position = new Vector3(0, 0.1f, -26.5f);
                target = null;
                navmeshagent.speed = 0f;
                navmeshagent.angularSpeed = 0f;
            }
        }
        else if (GameManager.GetComponent<GameManager>().state == State.Final)
        {
            time += Time.deltaTime;
            this.transform.position = Vector3.Lerp(myposition, finalposition, time / 4.0f);
            if (particlecount == 0)
            {
                GetComponent<ParticleSystem>().Play();
                particlecount = 1;
            }
            if ((time > 5.0f) && (finalcount == 0))
            {
                Destroy(this.gameObject);
                finalcount = 1;
            }
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
        else if ((target == HouseKey) && (other.gameObject.tag == "HouseKeyTag"))
        {
            target = Goal;
            other.gameObject.transform.parent = this.gameObject.transform;
            other.gameObject.transform.localPosition = new Vector3(0, 0, 0.7f);
        }
    }

    private void Retrieve()
    {
        navmeshagent.SetDestination(target.transform.position);
    }
}
