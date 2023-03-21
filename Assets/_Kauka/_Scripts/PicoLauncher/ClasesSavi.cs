using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase para recopilar todos los enums

namespace Innovae.Savi
{
	#region SAVI Classes

	/// <summary>
	/// Clase encargada de la creacion del JSON para el envio al backend
	/// </summary>
	internal class InfoSession
	{
		public int UserId;
		public int SessionId;
		public int Duration;

		public List<Tarea> Tasks;

		public InfoSession(int sessionID, int userID)
		{
			UserId = userID;
			SessionId = sessionID;
		}
	}

	/// <summary>
	/// Clase encargada de la creacion de las tareas de forma apta para añadir al json
	/// </summary>
	[Serializable]
	internal class Tarea
	{
		public string task_title;
		public int taskID;
		public List<Variable> variableList;

		public Tarea(string T, int Id, List<Variable> vars)
		{
			task_title = T;
			taskID = Id;

			variableList = new List<Variable>();

			foreach (var variable in vars)
			{
				variableList.Add(variable);
			}
		}

		public void SetValue(int id, int value)
		{
			variableList[id].value = value;
		}
	}

	/// <summary>
	/// Clase encargada de la creacion de la lista de variables de cada tarea aptas para el JSON
	/// </summary>
	[Serializable]
	internal class Variable
	{
		public string name;
		public int id_value;
		public int value;

		public Variable(string n, int id, int tarea)
		{
			name = n;
			id_value = tarea /** 1000*/ + id;
		}
	}

	internal class infoVueltaCerrarSesion
	{
		public string ack;

		public string code;

		public string msg;
	}

	internal class RespSecurity
	{
		public string ack;
		public int code;
		public string msg;
		public bool checkResult;
	}

	#endregion SAVI Classes

	/// <summary>
	/// La clase tiempo es la encargada de registrar los tiempos de cada tarea y hacer su tratamiento
	/// </summary>
	[Serializable]
	internal class Tiempo
	{
		private DateTime Inicio;
		private DateTime Final;
		public int TiempoTotal;

		public Tiempo()
		{
			Inicio = DateTime.Now;
		}

		public int CalcularTiempo()
		{
			Final = DateTime.Now;

			TiempoTotal = (int)Final.Subtract(Inicio).TotalSeconds;

			return TiempoTotal;
		}

		public void ReiniciarContador()
		{
			Inicio = DateTime.Now;
		}
	}
}