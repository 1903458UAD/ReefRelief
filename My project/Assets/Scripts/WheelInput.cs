using System.Diagnostics;
using UnityEngine;
using System.Collections;

public class WheelInput : MonoBehaviour
{
    #region Variables
    [Header("Pointer")]
    [SerializeField] private GameObject pointer;
    [SerializeField] private float spinSpeed;
    [SerializeField] private GameObject detector;
    [SerializeField] private LayerMask segments;
    [Header ("Segments")]
    [SerializeField] private GameObject Segment1;
    [SerializeField] private GameObject Segment2;
    [SerializeField] private GameObject Segment3;
    [SerializeField] private GameObject Segment4;
    [SerializeField] private GameObject Segment5;
    [SerializeField] private GameObject Segment6;
    [SerializeField] private GameObject Segment7;
    [SerializeField] private GameObject Segment8;
    [SerializeField] private GameObject Segment9;
    [SerializeField] private GameObject Segment10;
    [SerializeField] private GameObject Segment11;
    [SerializeField] private GameObject Segment12;

    public TrashGameManager GM;
    public int playerNo;
    private bool stopped;
    #endregion

    private void Update()
    {
        if (!stopped)
        {
            SpinWheel();
        }
    }

    private void SpinWheel()
    {
        Vector3 rotationAmount = new Vector3(0, 0, spinSpeed);
        pointer.transform.Rotate(rotationAmount * Time.deltaTime);
    }

    public void StopWheel()
    {
        if (GM.wheelsActive)
        {
            stopped = true;
            StartCoroutine(FinalSpin(1f, 180f));
        }
    }

    private void RegisterChoice()
    {
        RaycastHit2D hit = Physics2D.Raycast(detector.transform.position, Vector2.down, 1f, segments);
        string name = hit.collider.name;
        GM.SetName(name, playerNo);
    }

    private IEnumerator FinalSpin(float duration, float speed)
    {
        float elapsed = 0f;
        int segments = 12;
        float segmentAngle = 30f;

        while (elapsed < duration)
        {
            pointer.transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        float currentZ = pointer.transform.eulerAngles.z % 360f;
        float snapPoint = Mathf.Round((currentZ - 15f) / segmentAngle) * segmentAngle + 15f;
        float snapDuration = 0.2f;

        yield return StartCoroutine(SnapSegment(snapPoint, snapDuration));
    }

    private IEnumerator SnapSegment(float targetAngle, float duration)
    {
        float startRotation = pointer.transform.eulerAngles.z;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float z = Mathf.LerpAngle(startRotation, targetAngle, elapsed / duration);
            pointer.transform.eulerAngles = new Vector3(0f, 0f, z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        pointer.transform.eulerAngles = new Vector3(0f, 0f, targetAngle);
        RegisterChoice();
    }
}
