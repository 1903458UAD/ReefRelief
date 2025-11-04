using UnityEngine;
using System.Collections;

public class FishScript : MonoBehaviour
{
    public Transform target;
    public float duration;
    public bool isHit;
    [SerializeField] private TrashGameManager GM;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball_1") && !isHit)
        {
            isHit = true;
            GM.AddScore(-1500);
        }
        else if (other.gameObject.CompareTag("Ball_2") && !isHit)
        {
            isHit = true;
            GM.AddScore(-1500);
        }
    }

    public void MoveFish()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, target.position, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target.position;
    }
}
