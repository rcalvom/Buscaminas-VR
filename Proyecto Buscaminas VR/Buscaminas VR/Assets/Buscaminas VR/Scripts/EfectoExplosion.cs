using UnityEngine;

public class EfectoExplosion : MonoBehaviour {

	public GameObject bomb;
	public float power = 10.0f;
	public float radius = 5.0f;
	public float upforce = 1.0f;

	private void FixedUpdate () {
		if (bomb == enabled) {
			Invoke ("Detonate", 0);
		}
	}

	private void Detonate(){
		Vector3 explosionPosition = bomb.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
		foreach(Collider hit in colliders) { 
            Rigidbody rb = hit.GetComponent<Rigidbody>();
			if (rb != null) { 
                rb.AddExplosionForce(power, explosionPosition, radius, upforce, ForceMode.Impulse);
			}
		}
	}
}
