using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    private Vector3 pos1, pos2;
    public float speed;
    private Vector3 startPos;
    Vector3 nextPos;

    private void Awake()
    {
        startPos = transform.position;
        pos1 = startPos;
        pos2 = startPos;
    }
    // Start is called before the first frame update
    void Start()
    {
        pos1.y = startPos.y + 0.1f;
        pos2.y = startPos.y - 0.1f;

        nextPos = pos1;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(90 * Time.deltaTime, 45 * Time.deltaTime, 90 * Time.deltaTime, Space.World);

        if (transform.position == pos1)
        {
            nextPos = pos2;
        }
        if (transform.position == pos2)
        {
            nextPos = pos1;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball1"))
        {
            Destroy(gameObject);
            SFXCtrl.instance.ShowCoinSparkle(other.transform.position);
        }
        else if (other.gameObject.CompareTag("Ball2"))
        {
            Destroy(gameObject);
            SFXCtrl.instance.ShowCoinSparkle(other.transform.position);
        }
        else if (other.gameObject.CompareTag("Hole"))
        {
            Destroy(gameObject);
        }
    }

}
