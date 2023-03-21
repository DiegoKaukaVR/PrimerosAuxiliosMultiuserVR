using System.Collections.Generic;
using UnityEngine;

namespace Innovae.Savi
{
    public class MilestoneManager : MonoBehaviour
    {
        private static InfoSession Sesion;

        private static List<Tarea> tareas;

        private static Tiempo duracion;

        public static void CrearSesion(int sessionID, int userID)
        {
            Sesion = new InfoSession(sessionID, userID);
            duracion = new Tiempo();
            if (tareas == null)
            {
                tareas = new List<Tarea>();
            }
            Sesion.Tasks = tareas;
        }

        public static void AgregarTarea(string nombreTarea, int numeroVariables)
        {
            if (tareas == null)
            {
                tareas = new List<Tarea>();
            }
            tareas.Add(new Tarea(nombreTarea, tareas.Count, CrearListaVariables(numeroVariables, tareas.Count)));
        }
        // ---
        public static void AgregarTarea(string nombreTarea, string nombreVariable)
        {
            if (tareas == null)
            {
                tareas = new List<Tarea>();
            }
            tareas.Add(new Tarea(nombreTarea, tareas.Count, CrearListaVariables(tareas.Count, nombreVariable)));
        }
        // ---
        public static void AgregarTarea(string nombreTarea, int numeroVariables, List<string> nombreVariables)
        {
            if (tareas == null)
            {
                tareas = new List<Tarea>();
            }
            tareas.Add(new Tarea(nombreTarea, tareas.Count, CrearListaVariables(numeroVariables, tareas.Count, nombreVariables)));
        }

        // Lista de variables independiente para cada hito
        private static List<Variable> CrearListaVariables(int length, int hito, List<string> nombreVariables)
        {
            List<Variable> ListaVariables = new List<Variable>();

            for (int i = 0; i < length; i++)
            {
                if (nombreVariables.Count <= i)
                {
                    ListaVariables.Add(new Variable("Parameter: " + i, i, hito));
                    continue;
                }

                ListaVariables.Add(new Variable(nombreVariables[i], i, hito));
            }

            return ListaVariables;
        }

        // ---
        private static List<Variable> CrearListaVariables(int hito, string nombreVariable)
        {
            List<Variable> ListaVariables = new List<Variable>();
            ListaVariables.Add(new Variable(nombreVariable, 0, hito));
            return ListaVariables;
        }
        // ---
        private static List<Variable> CrearListaVariables(int length, int hito)
        {
            List<Variable> ListaVariables = new List<Variable>();

            for (int i = 0; i < length; i++)
            {
                ListaVariables.Add(new Variable("Parameter: " + i, i, hito));
            }

            return ListaVariables;
        }

        public static void CompletarTarea(int hito, int id, int value)
        {
                //tareas[hito].SetValue(id, value);
            try
            {
                tareas[hito].SetValue(id, value);
            }
            catch (System.Exception e)
            {
                Debug.Log("Variable value could not be assigned: " + e);
            }
        }

        public static string EscribirJSON()
        {
            Sesion.Duration = duracion.CalcularTiempo();

            string Texto = JsonUtility.ToJson(Sesion);

            return Texto;
        }
    }
}