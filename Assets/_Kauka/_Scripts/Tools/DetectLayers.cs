using UnityEngine;
using System.Collections;

namespace Tools
{
	public class DetectLayer
	{
		/// <summary>
		/// Esta funcion sirve para detecta si un layer pertenece a una LayerMask
		/// </summary>
		public static bool LayerInLayerMask(int layer, LayerMask layerMask)
		{
			return (((1 << layer) & layerMask) != 0);
		}
	}


}
