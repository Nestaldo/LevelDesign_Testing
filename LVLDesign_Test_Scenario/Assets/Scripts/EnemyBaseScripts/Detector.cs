using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] LayerMask detectionLayer;

    public GameObject DetectClosestOne(Vector3 detectionPos, float radius)
    {
        GameObject detected = null;
        Collider[] detecteds = Physics.OverlapSphere(detectionPos, radius, detectionLayer);
        if(detecteds.Length > 0)
        {
            float minDis = Mathf.Infinity;
            foreach (Collider c in detecteds)
            {
                float dis = Vector3.Distance(detectionPos, c.transform.position);
                if(dis <= minDis)
                {
                    minDis = dis;
                    detected = c.gameObject;
                }
            }
        }
        return detected;
    }

    public List<GameObject> DetectAll(Vector3 detectionPos, float radius)
    {
        List<GameObject> all = new List<GameObject>();
        Collider[] detecteds = Physics.OverlapSphere(detectionPos, radius, detectionLayer);
        if(detecteds.Length > 0)
        {
            foreach (Collider c in detecteds)
            {
                all.Add(c.gameObject);
            }
        }
        return all;
    }
}
