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
            transform.DOLocalMoveX(xPositionB, durationAB).SetEase(Ease.Linear);
            yield return new WaitForSeconds(durationAB);

            transform.DOLocalMoveX(xPositionC, durationBC).SetEase(Ease.Linear);
            yield return new WaitForSeconds(durationBC);

            yield return new WaitForSeconds(startDelay);

            transform.DOLocalMoveX(xPositionB, durationBC).SetEase(Ease.Linear);
            yield return new WaitForSeconds(durationBC);


            transform.DOLocalMoveX(xPositionA, durationAB).SetEase(Ease.Linear);
            yield return new WaitForSeconds(durationAB);
        }
    }
}
