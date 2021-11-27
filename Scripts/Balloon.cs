using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    GameManager GM;
    public GameObject particle;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }
    void FixedUpdate()
    {
        if (GM.inMainMenu||GM.CanPlay)
        transform.Translate(Vector3.up * GM.balloonSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Balloon"))
            {
                if (GM.CanPlay)
                {
                    GM.AS.PlayOneShot(GM.popSound);
                    GameObject NewParticle = Instantiate(particle,
                                                         gameObject.transform.position,
                                                         Quaternion.identity) as GameObject;
                    GM.score++;
                    GM.Score.text = GM.score.ToString();
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Trap"))
        {
            if (!GM.inMainMenu)
            {
                GM.ShowResult();
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }
}
