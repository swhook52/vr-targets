using UnityEngine;

public class RayViewer : MonoBehaviour
{
    public float weaponRange = 50f;
    public GameObject gunEnd;

    void Start()
    {
    }


    void Update()
    {
        // Draw a line in the Scene View  from the point lineOrigin in the direction of fpsCam.transform.forward * weaponRange, using the color green
        Debug.DrawRay(gunEnd.transform.position, gunEnd.transform.forward * weaponRange, Color.green);
    }
}