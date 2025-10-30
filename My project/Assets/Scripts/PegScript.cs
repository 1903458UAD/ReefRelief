using UnityEngine;
using System.Collections;

public class PegScript : MonoBehaviour
{
    #region Variables

    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private Transform spriteObj;
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private Collider2D col;
    [SerializeField] private int hits;
    [SerializeField] private TrashGameManager GM;

    [Header("Sprites")]
    [SerializeField] private Sprite stage1;
    [SerializeField] private Sprite stage2;
    [SerializeField] private Sprite stage3;

    private float cooldown = 0.5f;
    private float maxStuckTime = 1f;
    private float stuckTime_1;
    private float stuckTime_2;
    private bool cooldownBool = false;

    #endregion

    #region Collision Logic

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball_1") && !cooldownBool)
        {
            StartCoroutine("TriggerCooldown");
            if (hits == 0)
            {
                GM.AddScore(500);
                hits++;
            }
            else if (hits == 1)
            {
                GM.AddScore(750);
                hits++;
            }
            else if (hits == 2)
            {
                GM.AddScore(1000);
                hits++;
            }
        }
        if (other.gameObject.CompareTag("Ball_2") && !cooldownBool)
        {
            StartCoroutine("TriggerCooldown");
            if (hits == 0)
            {
                GM.AddScore(500);
                hits++;
            }
            else if (hits == 1)
            {
                GM.AddScore(750);
                hits++;
            }
            else if (hits == 2)
            {
                GM.AddScore(1000);
                hits++;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball_1"))
        {
            stuckTime_1 += Time.deltaTime;

            if (stuckTime_1 >= maxStuckTime)
            {
                Destroy(gameObject);
                stuckTime_1 = 0f;
            }
        }
        else if (other.gameObject.CompareTag("Ball_2"))
        {
            stuckTime_2 += Time.deltaTime;

            if (stuckTime_2 >= maxStuckTime)
            {
                Destroy(gameObject);
                stuckTime_2 = 0f;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball_1"))
        {
            stuckTime_1 = 0f;
        }
        else if (other.gameObject.CompareTag("Ball_2"))
        {
            stuckTime_2 = 0f;
        }
    }

    #endregion

    private void Update()
    {
        switch (hits)
        {
            case 0:
                SR.color = Color.green;
                spriteObj.localScale = new Vector3(3f, 3f, 3f);
                spriteObj.GetComponent<SpriteRenderer>().sprite = stage1;
                break;
            case 1:
                SR.color = Color.blue;
                spriteObj.localScale = new Vector3(2.75f, 2.75f, 2.75f);
                spriteObj.GetComponent<SpriteRenderer>().sprite = stage2;
                break;
            case 2:
                SR.color = Color.red;
                spriteObj.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                spriteObj.GetComponent<SpriteRenderer>().sprite = stage3;
                break;
            case 3:
                RB.bodyType = RigidbodyType2D.Dynamic;
                break;
        }
    }

    IEnumerator TriggerCooldown()
    {
        cooldownBool = true;
        yield return new WaitForSeconds(cooldown);

        cooldownBool = false;
    }
}
