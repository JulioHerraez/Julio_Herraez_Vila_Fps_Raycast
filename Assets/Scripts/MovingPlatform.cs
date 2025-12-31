using UnityEngine;



public class MovingPlatform : MonoBehaviour

{

    public Vector3 moveOffset = new Vector3(5f, 0f, 0f);

    public float speed = 2f;



    private Vector3 startPoint;

    private Vector3 endPoint;



    void Start()

    {

        startPoint = transform.position;

        endPoint = startPoint + moveOffset;

    }



    void Update()

    {

        float t = (Mathf.Sin(Time.time * speed) + 1f) * 0.5f;

        transform.position = Vector3.Lerp(startPoint, endPoint, t);

    }



    private void OnCollisionEnter(Collision collision)

    {

        if (!collision.gameObject.CompareTag("Player"))

            return;



        

        if (collision.contacts[0].normal.y > 0.5f)

        {

            collision.transform.SetParent(transform);

        }

    }



    private void OnCollisionExit(Collision collision)

    {

        if (!collision.gameObject.CompareTag("Player"))

            return;



        if (collision.transform.parent == transform)

        {

            collision.transform.SetParent(null);

        }

    }
}