/* Credits do Boris1998 */

using UnityEngine;
using System.Collections;
 
public static class AudioEffects {
 
	public static IEnumerator FadeIn (AudioSource audioSource, float FadeTime, float volume){

		audioSource.Play();
		float startVolume = 0.01f;
		audioSource.volume = startVolume;

		while(audioSource.volume < volume){
			audioSource.volume += startVolume * Time.deltaTime * 2 / FadeTime;
			yield return null;
		}
	}

	public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime){

		float startVolume = audioSource.volume;

		while(audioSource.volume > 0){
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
			yield return null;
		}

		audioSource.Stop();
		audioSource.volume = startVolume;
	}
}