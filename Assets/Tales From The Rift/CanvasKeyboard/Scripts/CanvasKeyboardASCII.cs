using UnityEngine;
using System.Collections;

namespace TalesFromTheRift
{
	public class CanvasKeyboardASCII : MonoBehaviour 
	{
		public CanvasKeyboard canvasKeyboard;
		private bool ShiftDown = false;			//БОЛЬШАЯ БУКВА
		private bool AltDown = false;			//знаки
		private bool EngDown = false;			//английский
		public GameObject alphaBoardUnsfhifted;	//русские буквы
		public GameObject alphaBoardSfhifted;	//БОЛЬШИЕ русские буквы
		public GameObject numberBoardUnshifted;	//цифры
		public GameObject numberBoardShifted;	//Символы
		public GameObject engBoardUnshifted;	//английские буквы
		public GameObject engBoardShifted;		//БОЛЬШИЕ английские буквы

		void Awake() 
		{
			Refresh();
		}
		
		void Refresh()				//Метод обновляет клавиатуру
		{
			// Show the current board
			alphaBoardUnsfhifted.SetActive(!AltDown && !ShiftDown && !EngDown);
			alphaBoardSfhifted.SetActive(!AltDown && ShiftDown && !EngDown);
			numberBoardUnshifted.SetActive(AltDown && !ShiftDown && !EngDown);
			numberBoardShifted.SetActive(AltDown && ShiftDown && !EngDown);
			engBoardUnshifted.SetActive(!AltDown && !ShiftDown && EngDown);
			engBoardShifted.SetActive(!AltDown && ShiftDown && EngDown);
		}

		public void OnKeyDown(GameObject kb)		//События клавиш
		{
			if (kb.name == "DONE")
			{
				if (canvasKeyboard != null)
				{
					canvasKeyboard.CloseKeyboard();
				}
			}
			else if (kb.name == "ALT")
			{
				AltDown = !AltDown;
				ShiftDown = false;
				Refresh ();
			}
			else if (kb.name == "SHIFT")
			{
				ShiftDown = !ShiftDown;
				Refresh ();
			}
			else if(kb.name == "ENG")
			{
				EngDown = !EngDown;
				Refresh();
			}
			else if(kb.name == "RUS")
			{
				EngDown = !EngDown;
				Refresh();
			}
			else
			{
				if (canvasKeyboard != null)
				{
					string s;
					if (kb.name == "BACKSPACE")
					{
						s = "\x08";
					}
					else
					{
						s = kb.name;
					}
					canvasKeyboard.SendKeyString(s);
				}
			}
			
		}
	}
}