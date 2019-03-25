using UnityEngine;

public class DestruirCasilla : MonoBehaviour {

	private void Update () {
		if (Mathf.Abs(gameObject.transform.position.y) >= 150) {
			Destroy (gameObject);
		}
	}
}
