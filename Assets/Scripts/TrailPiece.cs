using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class TrailPiece : MonoBehaviour
{
    private TrailRenderer trailRenderer;

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        gameObject.SetActive(false);
    }


    public IEnumerator CreateParaBole(Transform pos1, Transform pos2, float height, float trailTime , Color glowColor)
    {
        Material mat = trailRenderer.material;

        mat.SetColor("_GlowColor", glowColor);
        transform.position = pos1.position;
        trailRenderer.Clear();

        float T = 0;
        float timeSinceStart = 0;
        while (timeSinceStart < trailTime)
        {
            timeSinceStart += Time.deltaTime;
            T = timeSinceStart / trailTime;

            transform.position = SampleParabola(pos1.position, pos2.position, height, T, Vector3.up);
            yield return new WaitForEndOfFrame();
        }
        trailRenderer.Clear();
        gameObject.SetActive(false);
    }

    private Vector3 SampleParabola(Vector3 start, Vector3 end, float height, float t, Vector3 outDirection)
    {
        float parabolicT = t * 2 - 1;

        Vector3 travelDirection = end - start;
        Vector3 levelDirection = end - new Vector3(start.x, end.y, start.z);
        Vector3 right = Vector3.Cross(travelDirection, levelDirection);
        Vector3 up = outDirection;
        Vector3 result = start + t * travelDirection;
        result += ((-parabolicT * parabolicT + 1) * height) * up.normalized;
        return result;
    }
}
