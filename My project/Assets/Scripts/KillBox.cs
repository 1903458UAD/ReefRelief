using UnityEngine;

public class KillBox : MonoBehaviour
{
    [SerializeField] private CannonScript cannon_1;
    [SerializeField] private CannonScript cannon_2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball_1"))
        {
            cannon_1.GetComponent<CannonScript>().ToggleCanFire(true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Ball_2"))
        {
            cannon_2.GetComponent<CannonScript>().ToggleCanFire(true);
            Destroy(other.gameObject);
        }
    }
}
