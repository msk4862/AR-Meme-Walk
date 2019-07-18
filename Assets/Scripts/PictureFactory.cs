﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFactory : MonoBehaviour {

	public GameObject picPrefab;
	public GoogleSearch google;

	public void deleteOldPictures ()
	{
		if (transform.childCount > 0) {
			foreach (Transform child in this.transform) {
				Destroy(child.gameObject);
			}
		}
	}

	public void CreateImages (List<string> urlList, int resultNum, Vector3 camForward)
	{
		int picNum = 1;
		Vector3 centre = Camera.main.transform.position;
		foreach (string url in urlList) {
			Vector3 pos = GetPosition(picNum, resultNum, camForward);
			GameObject pic = Instantiate(picPrefab, pos, Quaternion.identity, this.transform);

			pic.GetComponent<pictureBehaviour>().loadImage(url);
			picNum++;
		}
	}

	public Vector3 GetPosition (int picNum, int rowNum, Vector3 camForward)
	{
		Vector3 pos = Vector3.zero;

		if (picNum <= 5) {
			pos = camForward + new Vector3 (picNum * -3, 0, rowNum * 3.5f);
		} else {
			pos = camForward + new Vector3((picNum%5)* 3, 0, rowNum*3.5f);
		}
		return pos;
	}
}
