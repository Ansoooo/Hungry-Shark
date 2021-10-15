using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Camera cam;
    RaycastHit hit;

    Vector3 target;
    public float swimSpeed;

    public ObjectPool pool;

    float score;
    public Text scoreDisplay;

    float lives;
    public Text liveDisplay;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 3;
    }

    void movePlayer()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, swimSpeed * Time.deltaTime);
    }

    void Update()
    {
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && Input.GetMouseButton(1))
        {
            target = new Vector3(hit.point.x, 10, hit.point.z);
            movePlayer();
        }

        scoreDisplay.text = score.ToString();
        liveDisplay.text = lives.ToString();

        if (lives == 0)
            Application.Quit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            int checkType = collision.gameObject.GetComponent<Fish>().getFishType();

            if(checkType == 0)
            {
                score += 0.5f;
                    if(swimSpeed > 5)
                        swimSpeed -= 0.5f; // cap the debuff
            }
            else if (checkType == 1)
            {
                    score -= 0.5f;
                    lives -= 0.5f;
            }

            pool.stash(collision.gameObject);
        }
    }
}
