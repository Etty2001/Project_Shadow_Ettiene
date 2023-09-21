using UnityEditor;
using UnityEditor.PackageManager;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    // This function gets called everytime Unity updates the editor interface.
    public override void OnInspectorGUI()
    {
        // Getting the of the current object on which this script is attached.
        // Target is the currently selected GameObject that we are inspecting.
        // "target" is a poroperty of the Editor Class.
        // "target" returns a value of type object, but we need to cast it to type of "Interactable" to get the class we want to work with.
        // Casting basically means to convert from onw type to another.
        Interactable interactable = (Interactable)target;

        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            // "Prompt Message" is only a label
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);

            EditorGUILayout.HelpBox("EventOnlyInteractable can ONLY use UnityEvents.", MessageType.Info);

            if (interactable.GetComponent<InteractionEvents>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvents>();
            }
        }
        else
        {
            base.OnInspectorGUI();

            if (interactable.useEvents)
            {
                if (interactable.GetComponent<InteractionEvents>() == null)
                    // We are adding an item/component into the inspector using code.
                    interactable.gameObject.AddComponent<InteractionEvents>();
            }
            else
            {
                if (interactable.GetComponent<InteractionEvents>() != null)
                    DestroyImmediate(interactable.GetComponent<InteractionEvents>());
            }
        }
    }
}
