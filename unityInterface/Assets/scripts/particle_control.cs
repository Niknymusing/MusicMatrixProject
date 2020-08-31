using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_control : MonoBehaviour {

	public int channel = 0;
	public int audio_component = 3;
	public Material asset_mat;
	public float radiusMag = 1000f;
	public float emissionMag = 100f;
	public float startSizeMag = 10f;
	public float speedMultiplier = 50f;

	private float H, S, V, start_H, final_H;
	private Color startColor;
	private Color tmpColor;


	public ParticleSystem ps;
	private ParticleSystem.ShapeModule pShape;
	private ParticleSystem.MainModule pMain;
	private ParticleSystem.VelocityOverLifetimeModule vMain;
	private ParticleSystem.MainModule rMain;


	// Use this for initialization
	void Start () {
		//ps = GetComponent<ParticleSystem>();
		pShape = ps.shape;
		pMain = ps.main;
		vMain = ps.velocityOverLifetime;

	}
	
	// Update is called once per frame
	void Update () {
		//pShape.radius = SimpleSpectrum.spectrum[0] * radiusMag;
		pShape.angle = Mathf.Abs(OSC_channels.OSCch_data[channel,audio_component]) * 10f;
		

		//move object up and down
		transform.position = new Vector3(0f, SimpleSpectrum.spectrum[1] * startSizeMag, 0f);

		//color set and conversion to HSV
		startColor = asset_mat.GetColor("_Color");
		Color.RGBToHSV(startColor, out H, out S, out V);

		//color adjustments in hue
		start_H = Mathf.Clamp01(SimpleSpectrum.spectrum[0] * 10000f);

		//particle emission and size adjustments
		float tmp_emission = SimpleSpectrum.spectrum[0] * 10000f * emissionMag;
		ps.emissionRate = tmp_emission;
		float tmpSize = SimpleSpectrum.spectrum[0] * 1000f * startSizeMag;
		//pMain.startSize = new ParticleSystem.MinMaxCurve(tmpSize + 2f, tmpSize + 5f);

		//float tmp_speed = OSC_channels.OSCch_data[channel,2] * speedMultiplier;
		//pMain.startSpeed = new ParticleSystem.MinMaxCurve(tmp_speed+12f, tmp_speed+15f);
		
	}
	void LateUpdate(){
		//lerp across hue variations for smoother interpolations
		final_H = Mathf.LerpUnclamped(H, start_H, 2f * Time.deltaTime);

		//set final color
		asset_mat.SetColor("_Color", Color.HSVToRGB(final_H,S,V, true));
	}
}
