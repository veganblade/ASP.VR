using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();            //взаимодействие с выделенным объектом 
    string GetDescription();    //наведение на объект
}
