using UnityEngine;

public class ExplosionSonido : MonoBehaviour {
	
	private AudioSource source;

	public AudioClip sonidoExplosion;

	private void Start(){
		source = GetComponent<AudioSource>();
		source.PlayOneShot (sonidoExplosion, Ajustes.ajustes.volumenes.VolumenEfectos);

	}
}
