using DG.Tweening;
using System.Collections;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    public float yPositionA, yPositionB, yPositionC, durationAB, durationBC, startDelay; 

    void Start()
    {
        transform.position = new Vector3(transform.position.x, yPositionA, transform.position.z); 
        StartCoroutine(MoveTrap());
    }

    IEnumerator MoveTrap()
    {
        while (true)
        {
            transform.DOLocalMoveY(yPositionB, durationAB).SetEase(Ease.Linear);
            yield return new WaitForSeconds(durationAB);

            transform.DOLocalMoveY(yPositionC, durationBC).SetEase(Ease.Linear);
            yield return new WaitForSeconds(durationBC);

            yield return new WaitForSeconds(startDelay);

            transform.DOLocalMoveY(yPositionB, durationBC).SetEase(Ease.Linear);
            yield return new WaitForSeconds(durationBC);

            transform.DOLocalMoveY(yPositionA, durationAB).SetEase(Ease.Linear);
            yield return new WaitForSeconds(durationAB);
        }
    }
}
