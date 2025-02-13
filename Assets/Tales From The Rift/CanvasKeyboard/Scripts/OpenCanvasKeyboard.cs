using UnityEngine;
using System.Collections;

namespace TalesFromTheRift
{
	public class OpenCanvasKeyboard : MonoBehaviour 
	{
		public Canvas CanvasKeyboardObject; //холст с клавиатурой
		public GameObject inputObject;		//вводимые символы
		public void OpenKeyboard() 
		{		
			CanvasKeyboard.Open(CanvasKeyboardObject, inputObject != null ? inputObject : gameObject);
		}

		public void CloseKeyboard() 
		{		
			CanvasKeyboard.Close ();
		}
	}
}