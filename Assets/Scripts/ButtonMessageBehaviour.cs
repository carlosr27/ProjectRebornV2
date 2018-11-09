using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class ButtonMessageBehaviour : MonoBehaviour {

    public GameObject target;
    public string message;

	// Use this for initialization
	void Start () {

       GetComponent<TapGesture>().StateChanged += ButtonMessageBehaviour_StateChanged;
       

    }

    void ButtonMessageBehaviour_StateChanged(object sender, GestureStateChangeEventArgs e)
    {
        if (e.State == Gesture.GestureState.Ended)
        {
            target.SendMessage(message, SendMessageOptions.RequireReceiver);
        }
    }

   


}
