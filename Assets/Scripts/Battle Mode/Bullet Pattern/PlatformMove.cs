using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] float speed = 4;

    private void Start()
    {
        Destroy(gameObject,10);
    }
    private void Update()
    {
        transform.Translate(Vector3.left * speed*Time.deltaTime);
    }
}
