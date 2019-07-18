using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleSearch : MonoBehaviour {

	public PictureFactory pictureFactory;
	private string API_KEY = "AIzaSyDLltARMzlMRt3_kmTHec8LFqzou-_6-MM";
	public Text buttonText;

	public void GetPicture() {
		StartCoroutine(PictureRoutine());
	}

	IEnumerator PictureRoutine() {
		buttonText.transform.parent.gameObject.SetActive(false);
		string query  = buttonText.text;

		query = WWW.EscapeURL(query += " memes");
		pictureFactory.deleteOldPictures();

		Vector3 CameraForward = Camera.main.transform.forward;

		int rowNum = 1;
		for(int i = 1; i < 60; i += 10) {
			string url = "https://www.googleapis.com/customsearch/v1?q="+query+ "&cx=005774657766531524867%3An1ww7b8k-vy&filter=1&num=10&searchType=image&start="+i+
			"&fields=items%2Flink&key=" + API_KEY;

			WWW www = new WWW(url);
			yield return www;

			pictureFactory.CreateImages(ParseResponse(www.text), rowNum, CameraForward);
			rowNum++;
		}
		yield return new WaitForSeconds(5f);
		buttonText.transform.parent.gameObject.SetActive(true);
	}


	List<string> ParseResponse (string text)
	{
		List<string> urlList = new List<string> ();
		string[] urls = text.Split ('\n');
		foreach (string line in urls) {
			if (line.Contains ("link")) {
				string url = line.Substring (12, line.Length - 13);
				if (url.Contains (".jpg") || url.Contains (".png")) {
					urlList.Add(url);
				}
			}
		}
		return urlList;
	}
}
