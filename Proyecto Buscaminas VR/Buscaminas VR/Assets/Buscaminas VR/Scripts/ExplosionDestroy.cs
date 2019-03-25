using UnityEngine;

public class ExplosionDestroy : MonoBehaviour {

	private float inicio;

	void Start(){
		inicio = Time.time;
	}

	void Update () {
		if(Mathf.Abs(inicio-Time.time)>=2.2f){
			Destroy(gameObject);
		}
	}
}
