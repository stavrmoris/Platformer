using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100; 
    public Transform laserFirePoint;
    public GameObject laser;
    public LineRenderer m_lineRenderer;
    public GameObject lineEnd;
    Transform m_transform;

    private void Awake() {
        m_transform = GetComponent<Transform>();
    }
    private void Update()
    {
        ShootLaser();
        RaycastHit2D hit = Physics2D.Raycast(m_transform.position, transform.right);
        if (hit.collider.tag == "Player")
            Destroy(hit.collider.gameObject);
    }

    void ShootLaser()
    {
        if (Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserFirePoint.position, transform.right * laser.transform.localScale.x);
            Draw2DRay(laserFirePoint.position, _hit.point);
            lineEnd.transform.position = _hit.point;
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay * laser.transform.localScale.x);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }
}
