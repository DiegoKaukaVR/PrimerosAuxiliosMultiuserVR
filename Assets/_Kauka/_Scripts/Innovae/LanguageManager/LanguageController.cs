using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class LanguageController
{
	//Get the translations
	private static Dictionary<string, string> translations = new Dictionary<string, string>();

	/*
     *
     *  1 - Guardar Fichero delimitado por comas
     *
     *  2 - Abrir fichero con Notepad++
     *
     *  3 - Copiar Texto
     *
     *  4 - Codificacion >> Codificar en UTF-8
     *
     *  5 - Seguramente se han cambiado ciertos caracteres por simbolos raros.
     *
     *  6 - Pegar el texto Guardado
     *
     *  7 - Cerrar el fichero.
     *
     */

	#region ErrorManager

	[Space(10)]
	[Header("Show Debugs of this class")]
	private static bool showDebugs = false;

	private static string logName = "LanguageController";

	#endregion ErrorManager

	public static void GetKeys()
	{
		string text = GameController.Instance.textsFileName;
		Idioma selectedLanguage = GameController.Instance.selectedLanguage;
		//Get resource file
		string fileContent;
		try
		{

			///!!!---Cambiado para que la carpeta este dentro de assets innovae y que funcione en build
			TextAsset dictionary = Resources.Load<TextAsset>(text);
			fileContent = dictionary.text;
		}
		catch
		{
			ErrorManager.Error(logName, MethodBase.GetCurrentMethod().Name, "The resource is missing/unreadable");
			return;
		}

		//Read resource file
		using (StringReader reader = new StringReader(fileContent))
		{
			//Check if the file is empty
			string header;
			if ((header = reader.ReadLine()) == null)
			{
				ErrorManager.Error(logName, MethodBase.GetCurrentMethod().Name, "The resource is empty");
				return;
			}

			string separator = @";(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; //regex formula that handles value separation

			//Check if there's at least one translation
			var languages = Regex.Split(header, separator);
			if (languages.Length < 2)
			{
				ErrorManager.Error(logName, MethodBase.GetCurrentMethod().Name, "No translations found");
				return;
			}

			//Get the column index of the language to translate
			int targetColumnIndex = 0;

			for (int i = 1; i < languages.Length; i++) //i = 1 to skip the reference column
			{
				try
				{
					Idioma columnLanguage = (Idioma)Enum.Parse(typeof(Idioma), languages[i]);
					if (columnLanguage == selectedLanguage)
					{
						targetColumnIndex = i;
						break;
					}
				}
				catch
				{
					ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("Unknown language <{0}> found", languages[i]), showDebugs);
				}
			}

			if (targetColumnIndex == 0)
			{
				ErrorManager.Error(logName, MethodBase.GetCurrentMethod().Name, string.Format("Target language <{0}> is missing", selectedLanguage));
				return;
			}

			//Get the translations
			translations = new Dictionary<string, string>();

			string line;
			while ((line = reader.ReadLine()) != null)
			{
				//Check if there are enough translations
				var values = Regex.Split(line, separator);
				string key = values[0]; //0 = key column index

				if (values.Length <= targetColumnIndex)
				{
					ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("Key <{0}> skipped, no translation found", key), showDebugs);
					continue; //skip row
				}

				//Parse the translation
				string translation = values[targetColumnIndex];

				if (translation.StartsWith("\""))
					translation = translation.Remove(0, 1);

				if (translation.EndsWith("\""))
					translation = translation.Remove(translation.Length - 1);

				translation = translation.Replace("\"\"", "\"");

				//Add the translations to the dictionary
				if (translations.ContainsKey(key))
					ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("Duplicate key <{0}> ignored", key), showDebugs);
				else if (key.Equals(""))
					ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("Empty key <{0}> ignored", key), showDebugs);
				else
					translations.Add(key, translation);
			}

			//Get the gameObjects with the Translatable script and ovewrite their text component if the dictionary contains the key to translate
			string trText;
			foreach (Translatable tr in Resources.FindObjectsOfTypeAll(typeof(Translatable)))    //https://docs.unity3d.com/ScriptReference/Resources.FindObjectsOfTypeAll.html
			{
				trText = tr.targetText.text;
				foreach (var item in translations)
				{

					if (translations[item.Key] == trText)
					{
						string keyTrimed;
						if (translations.TryGetValue(item.Key, out keyTrimed))
						{

							keyTrimed = keyTrimed.Trim();
							tr.key = item.Key;
						}
						else
						{
							ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("Key <{0}> not found", item.Key), showDebugs);
						}
					}
				}
			}
		}
	}

	public static void LoadDictionary()
	{
		string text = GameController.Instance.textsFileName;
		Idioma selectedLanguage = GameController.Instance.selectedLanguage;
		//Get resource file
		string fileContent;
		try
		{

			///!!!---Cambiado para que la carpeta este dentro de assets innovae y que funcione en build
			TextAsset dictionary = Resources.Load<TextAsset>(text);
			fileContent = dictionary.text;
		}
		catch
		{
			ErrorManager.Error(logName, MethodBase.GetCurrentMethod().Name, "The resource is missing/unreadable");
			return;
		}

		//Read resource file
		using (StringReader reader = new StringReader(fileContent))
		{
			//Check if the file is empty
			string header;
			if ((header = reader.ReadLine()) == null)
			{
				ErrorManager.Error(logName, MethodBase.GetCurrentMethod().Name, "The resource is empty");
				return;
			}

			string separator = @";(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; //regex formula that handles value separation

			//Check if there's at least one translation
			var languages = Regex.Split(header, separator);
			if (languages.Length < 2)
			{
				ErrorManager.Error(logName, MethodBase.GetCurrentMethod().Name, "No translations found");
				return;
			}

			//Get the column index of the language to translate
			int targetColumnIndex = 0;

			for (int i = 1; i < languages.Length; i++) //i = 1 to skip the reference column
			{
				try
				{
					Idioma columnLanguage = (Idioma)Enum.Parse(typeof(Idioma), languages[i]);
					if (columnLanguage == selectedLanguage)
					{
						targetColumnIndex = i;
						break;
					}
				}
				catch
				{
					ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("Unknown language <{0}> found", languages[i]), showDebugs);
				}
			}

			if (targetColumnIndex == 0)
			{
				ErrorManager.Error(logName, MethodBase.GetCurrentMethod().Name, string.Format("Target language <{0}> is missing", selectedLanguage));
				return;
			}

			//Get the translations
			translations = new Dictionary<string, string>();

			string line;
			while ((line = reader.ReadLine()) != null)
			{
				//Check if there are enough translations
				var values = Regex.Split(line, separator);
				string key = values[0]; //0 = key column index

				if (values.Length <= targetColumnIndex)
				{
					ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("Key <{0}> skipped, no translation found", key), showDebugs);
					continue; //skip row
				}

				//Parse the translation
				string translation = values[targetColumnIndex];

				if (translation.StartsWith("\""))
					translation = translation.Remove(0, 1);

				if (translation.EndsWith("\""))
					translation = translation.Remove(translation.Length - 1);

				translation = translation.Replace("\"\"", "\"");

				//Add the translations to the dictionary
				if (translations.ContainsKey(key))
					ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("Duplicate key <{0}> ignored", key), showDebugs);
				else if (key.Equals(""))
					ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("Empty key <{0}> ignored", key), showDebugs);
				else
					translations.Add(key, translation);
			}

			//Get the gameObjects with the Translatable script and ovewrite their text component if the dictionary contains the key to translate
			string trKeyTrimed;
			foreach (Translatable tr in Resources.FindObjectsOfTypeAll(typeof(Translatable)))
			{
				trKeyTrimed = tr.key;
				trKeyTrimed = trKeyTrimed.Trim();
				if (tr.targetText == null)
				{
					if (tr.gameObject.GetComponent<Text>())
					{
						if (!translations.ContainsKey(trKeyTrimed))
							ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("Translation not found for key <{0}>", trKeyTrimed), showDebugs);
						else
							tr.gameObject.GetComponent<Text>().text = translations[trKeyTrimed];
					}
					else
					{
						ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("The TextComponent of <{0}> has not been assigned", tr.gameObject.name), showDebugs);
					}
				}
				else if (!translations.ContainsKey(trKeyTrimed))
					ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("Translation not found for key <{0}>", trKeyTrimed), showDebugs);
				else
					tr.targetText.text = translations[trKeyTrimed];
			}

			//Save configuration
			//PlayerPrefs.SetInt(enm_PlayerPrefs.CurrentLanguage.ToString(), (int)selectedLanguage);
		}
	}

	private static string GetKeyTranslation(string key)

	{
		string trKeyTrimed = key.Trim();

		if (!translations.ContainsKey(trKeyTrimed))
		{
			ErrorManager.Warning(logName, MethodBase.GetCurrentMethod().Name, string.Format("Translation not found for key <{0}>", trKeyTrimed), showDebugs);
			return null;
		}
		else
			return translations[trKeyTrimed];
	}


	public static void CrearCSV()
	{

		if (File.Exists(Application.dataPath + "/_Innovae/Resources/" + Application.productName + ".csv"))
		{
			File.Delete(Application.dataPath + "/_Innovae/Resources/" + Application.productName + ".csv");
		}
		string csv = "#;";
		foreach (Idioma idiomas in Enum.GetValues(typeof(Idioma)))
		{
			csv += idiomas.ToString() + ";";
		}
		csv += "\n";
		int index = 1;
		foreach (Translatable tr in Resources.FindObjectsOfTypeAll(typeof(Translatable)))
		{
			if (tr.targetText != null)
			{
				tr.targetText.text = tr.targetText.text.Replace(System.Environment.NewLine, "<br>");
				tr.targetText.text = tr.targetText.text.Replace("\n", "<br>");

				if (translations.TryGetValue(tr.targetText.text, out string tmp))
				{
					tr.key = tmp;
				}
				else
				{
					if (tr.key != "" && false)
					{
						csv += tr.key + ";\"" + tr.targetText.text + "\";\n";
					}
					else
					{
						csv += index.ToString() + ";\"" + tr.targetText.text + "\";\n";
						tr.key = index.ToString();
						translations.Add(tr.targetText.text, index.ToString());
					}
				}
				index++;
			}
		}
		File.WriteAllText(Application.dataPath + "/_Innovae/Resources/" + Application.productName + ".csv", csv);
		Debug.Log("CSV creado en :" + Application.dataPath + "/_Innovae/Resources/" + Application.productName + ".csv");
	}
}