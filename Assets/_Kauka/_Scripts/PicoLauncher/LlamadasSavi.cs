using System;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Networking;

namespace Innovae.Savi
{
	public class LlamadasSavi : MonoBehaviour
	{
		private static string[] token;

		private static string URL = "";

		public void CallCheckSecurity(Action<int, int> callback)
		{
			///Leemos los parametros que se mandan desde el launcer:
			///0 - No se usa, es la direccion del ejecutable
			///1 - Es el usuario
			///2 - Es el token
			///3 - Es el sessionID
			///4 - Es la URL del backoffice

			token = Environment.GetCommandLineArgs();

			string arguments = "";

			AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

			AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");
			bool hasExtra = intent.Call<bool>("hasExtra", "arguments");

			if (hasExtra)
			{
				try
				{
					AndroidJavaObject extras = intent.Call<AndroidJavaObject>("getExtras");
					arguments = extras.Call<string>("getString", "arguments");
					token = arguments.Split(' ');

					token[1] = token[0];
				}
				catch (System.Exception)
				{
					Application.Quit();
				}

			}
			else
			{
				Debug.LogError("No se han recibido argumentos correctos");
				Application.Quit();
			}


			try
			{
				URL = token[4];
				StartCoroutine(CheckSecurity(int.Parse(token[1]), token[2], ShowNetworkInterfaces(), int.Parse(token[3]), callback));
			}
			catch (Exception e)
			{
				Debug.LogError("No se han recibido argumentos correctos \n" + e);

				Application.Quit();
			}
		}

		private IEnumerator CheckSecurity(int userID, string token, string mac, int sessionID, Action<int, int> callback)
		{
			string url = URL + "/api/checkExperienceCanRun";

			WWWForm data = new WWWForm();

			data.AddField("user_id", userID);
			data.AddField("mac", mac);
			data.AddField("token", token);

			UnityWebRequest request = UnityWebRequest.Post(url, data);
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.LogError(request.error);
				Application.Quit();
			}
			else
			{
				Debug.LogError(request.downloadHandler.text);
				RespSecurity response = JsonUtility.FromJson<RespSecurity>(request.downloadHandler.text);
				if (response.checkResult == false)
				{
					Debug.LogError("Fallo de seguridad");
					Application.Quit();
				}
				else
				{
					Debug.Log("Seguridad comprobada");
					callback(sessionID, userID);
				}
			}
		}

		public void CerrarSesion(string json)
		{
			try
			{
				StartCoroutine(CerrarSessionCor(json));
			}
			catch (Exception e)
			{
				Debug.LogError(e);
				Application.Quit();
			}
		}

		private IEnumerator CerrarSessionCor(string json)
		{
			WWWForm data = new WWWForm();
			data.AddField("json", json);

			UnityWebRequest request = UnityWebRequest.Post(URL + "/api/closeSession", data);

			request.SetRequestHeader("Authorization", "Bearer" + token[2]);

			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.LogError(request.error + "     " + request.downloadHandler.text);
			}
			else
			{
				Debug.Log(request.downloadHandler.text);
				JsonUtility.FromJson<infoVueltaCerrarSesion>(request.downloadHandler.text);
			}
			Application.Quit();
		}

		private string ShowNetworkInterfaces()
		{
			string physicalAddress = "";
			NetworkInterface[] nice = NetworkInterface.GetAllNetworkInterfaces();

			foreach (NetworkInterface adaper in nice)
			{
				if (adaper.Description == "en0")
				{
					physicalAddress = adaper.GetPhysicalAddress().ToString();
					break;
				}
				else
				{
					physicalAddress = adaper.GetPhysicalAddress().ToString();

					if (physicalAddress != "")
					{
						break;
					};
				}
			}

			return physicalAddress;
		}
	}
}