using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private GameObject player;


    private float force = 3f;
    private float fieldofImpact = 2f;
    [SerializeField] private LayerMask LayerToHit;

    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate(){
        if(-player.transform.position.x+transform.position.x<=fieldofImpact){
            DoExplode();
            if(!addedDelay)
                StartCoroutine(AddDelay());
        }
    }

    public void DoExplode(){
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofImpact, LayerToHit);

        Vector2 direction = player.transform.position - transform.position;
        player.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
    }
    public void DestroyBomb(){
        gameObject.SetActive(false);
    }

    private bool addedDelay;
    public IEnumerator AddDelay(){
        Debug.Log("fdfsfdsfs");
        addedDelay = true;
        yield return new WaitForSeconds ( .5f );
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        DestroyBomb();
        addedDelay = false;
    }

    //WORK IN PROGRESS (...)

}
