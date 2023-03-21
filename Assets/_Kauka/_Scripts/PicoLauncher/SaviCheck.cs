using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Innovae.Savi;

public class SaviCheck : MonoBehaviour
{
	LlamadasSavi savi;

	// ---
	//public bool[] puntuacion = new bool[3];

	//public void buttonPulsado(int i)
 //   {
	//	puntuacion[i] = true;
 //   }
    // ---
    private void Awake()
    {
		CrearNuevaSesion();
    }
    public void CrearNuevaSesion()
	{
		savi = gameObject.AddComponent<LlamadasSavi>();
        savi.CallCheckSecurity(MilestoneManager.CrearSesion);
        #region comn
        /*
		/*if (!Application.isEditor)
			savi.CallCheckSecurity(MilestoneManager.CrearSesion);
		else (* /)
		MilestoneManager.CrearSesion(0, 0);
		*/

        // ---
  //      MilestoneManager.AgregarTarea("Boton pulsado 1", "BotonPulsado1");
		//MilestoneManager.AgregarTarea("Boton pulsado 2", "BotonPulsado2");
		//MilestoneManager.AgregarTarea("Boton pulsado 3", "BotonPulsado3");
		// ---

		//List<string> nombresVariables = new List<string>();

		//nombresVariables.Add("Arrival test answered correctly");

		//MilestoneManager.AgregarTarea("Arrival test answered correctly", 1, nombresVariables);
		//nombresVariables.Clear();

		//nombresVariables.Add("EPI correctly selected");
		//MilestoneManager.AgregarTarea("EPI correctly selected", 1, nombresVariables);
		//nombresVariables.Clear();

		//nombresVariables.Add("Ground in place");
		//nombresVariables.Add("Connected to electric current");
		//nombresVariables.Add("Semitrailer turned on");
		//nombresVariables.Add("Hose connected");
		//nombresVariables.Add("Purge hose");
		//nombresVariables.Add("Lower temperature to -90");
		//MilestoneManager.AgregarTarea("Semitrailer start-up correct", 6, nombresVariables);
		//nombresVariables.Clear();

		//nombresVariables.Add("Turn pump on at -90C");
		//nombresVariables.Add("Stop pump at  more or less at 95% full");
		//MilestoneManager.AgregarTarea("Discharge LIN/LOX/LAR", 2, nombresVariables);
		//nombresVariables.Clear();

		//nombresVariables.Add("Purge hose");
		//nombresVariables.Add("Disconnect hose");
		//nombresVariables.Add("Turn off the semi-trailer");
		//nombresVariables.Add("Disconnect electrical power");
		//nombresVariables.Add("Disconnect ground");
		//nombresVariables.Add("Close Semitrailer Doors");
		//MilestoneManager.AgregarTarea("Material pick up", 6, nombresVariables);
		//nombresVariables.Clear();

		//nombresVariables.Add("Pressure +/- 20% of the standard customer");
		//nombresVariables.Add("Level 95%-100% ");
		//MilestoneManager.AgregarTarea("Final customer pressure", 2, nombresVariables);
		//nombresVariables.Clear();

		//nombresVariables.Add("Closing test answered correctly");
		//MilestoneManager.AgregarTarea("Closing test answered correctly", 1, nombresVariables);
		//nombresVariables.Clear();

		//nombresVariables.Add("Not ocurred");
		//MilestoneManager.AgregarTarea("Incident, excessive pressure in customer's tank", 1, nombresVariables);
		//MilestoneManager.AgregarTarea("Incident, excessive pressure in the semitrailer", 1, nombresVariables);
		//MilestoneManager.AgregarTarea("Cavitation", 1, nombresVariables);
		//MilestoneManager.AgregarTarea("Overload", 1, nombresVariables);

		//nombresVariables.Clear();
		//nombresVariables.Add("Fatal error");
		//MilestoneManager.AgregarTarea("How many", 1, nombresVariables);

		//nombresVariables.Clear();
		//nombresVariables.Add("V9 Opened");
		//MilestoneManager.AgregarTarea("V9 Opened", 1, nombresVariables);

		//nombresVariables.Clear();
		//nombresVariables.Add("V9 Closed");
		//MilestoneManager.AgregarTarea("V9 Closed", 1, nombresVariables);
        #endregion comn
	}
    #region com
    //private void Update()
    //{
    //	if (Input.GetKeyDown(KeyCode.R))
    //		Debug.LogError(MilestoneManager.EscribirJSON());
    //}

    //public void CompletarTarea(int hito, int variable, int value)
    //{
    //    MilestoneManager.CompletarTarea(hito, variable, value);
    //}
    #endregion com
    public void CerrarSesion()
	{
		Debug.LogError(MilestoneManager.EscribirJSON());
		savi.CerrarSesion(MilestoneManager.EscribirJSON());
	}
	//public void RecopilarInformacionJSON()
	//{

 //       for (int i = 0; i < 3; i++)
 //       {
 //           if (puntuacion[i])
 //           {
 //               MilestoneManager.CompletarTarea(i, 0, 1);
 //           }
 //           else
 //           {
 //               MilestoneManager.CompletarTarea(i, 0, 0);
 //           }
 //       }

 //       //MilestoneManager.EscribirJSON();
 //   }
	public void finalizarAplicacion()
    {
		StartCoroutine(delayFinalizar());
	}
	IEnumerator delayFinalizar()
    {
		//RecopilarInformacionJSON();
		yield return new WaitForSeconds(0.5f);
		CerrarSesion();
	}
}
