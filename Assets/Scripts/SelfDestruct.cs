using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float lifeTimer = 0.05f;

    void Start() => Destroy(this.gameObject, lifeTimer);
}
