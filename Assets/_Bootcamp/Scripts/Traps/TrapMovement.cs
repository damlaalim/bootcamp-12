using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TrapMovement : MonoBehaviour
{
    public float xPositionA , xPositionB , xPositionC , durationAB , durationBC, startDelay; 

    void Start()
    {
        transform.position = new Vector3(xPositionA, transform.position.y, transform.position.z); 
        StartCoroutine(MoveTrap());
    }

    IEnumerator MoveTrap()
    {
        while (true)
        {
            transform.DOMoveX(xPositionB, durationAB).SetEase(Ease.Linear);
            yield return new WaitForSeconds(durationAB);

            transform.DOMoveX(xPositionC, durationBC).SetEase(Ease.Linear);
            yield return new WaitForSeconds(durationBC);

            yield return new WaitForSeconds(startDelay);

            transform.DOMoveX(xPositionB, durationBC).SetEase(Ease.Linear);
            yield return new WaitForSeconds(durationBC);


            transform.DOMoveX(xPositionA, durationAB).SetEase(Ease.Linear);
            yield return new WaitForSeconds(durationAB);
        }
    }
}
