using UnityEngine;
using MyDataStructures;

[ExecuteAlways]
public class Path : MonoBehaviour
{
    // This list will be built by OnValidate and Awake
    public LinkedList<GameObject> PathNodes = new LinkedList<GameObject>();

    [Header("Starting Waypoints (Drag GameObjects here)")]
    public GameObject Waypoint1;
    public GameObject Waypoint2;
    public GameObject Waypoint3;
    public GameObject Waypoint4;
    public GameObject Waypoint5;
    public GameObject Waypoint6;
    public GameObject Waypoint7;
    public GameObject Waypoint8;
    public GameObject Waypoint9;
    public GameObject Waypoint10;
    public GameObject Waypoint11;
    public GameObject Waypoint12;
    public GameObject Waypoint13;
    public GameObject Waypoint14;
    public GameObject Waypoint15;
    public GameObject Waypoint16;

    private void Awake()
    {
        // Build the path at runtime
        BuildPath();
    }

    private void OnValidate()
    {
        // Build the path in the editor when you change waypoints
        BuildPath();
    }

    private void OnDrawGizmos()
    {
        // 🛑 We REMOVED BuildPath() from here.
        // OnDrawGizmos should only READ data, not create new lists.

        if (PathNodes.Head == null)
        {
            // If the path is empty, draw a red sphere at the script's location
            // so you can see that it's working.
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.5f);
            return;
        }

        // Draw the yellow lines
        Node<GameObject> current = PathNodes.Head;
        while (current != null && current.Next != null)
        {
            if (current.Value != null && current.Next.Value != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(
                    current.Value.transform.position,
                    current.Next.Value.transform.position
                );
            }
            current = current.Next;
        }
    }

    private void BuildPath()
    {
        // Re-create the list
        PathNodes = new LinkedList<GameObject>();

        // Add all non-null waypoints in order
        if (Waypoint1 != null) PathNodes.AddLast(Waypoint1);
        if (Waypoint2 != null) PathNodes.AddLast(Waypoint2);
        if (Waypoint3 != null) PathNodes.AddLast(Waypoint3);
        if (Waypoint4 != null) PathNodes.AddLast(Waypoint4);
        if (Waypoint5 != null) PathNodes.AddLast(Waypoint5);
        if (Waypoint6 != null) PathNodes.AddLast(Waypoint6);
        if (Waypoint7 != null) PathNodes.AddLast(Waypoint7);
        if (Waypoint8 != null) PathNodes.AddLast(Waypoint8);
        if (Waypoint9 != null) PathNodes.AddLast(Waypoint9);
        if (Waypoint10 != null) PathNodes.AddLast(Waypoint10);
        if (Waypoint11 != null) PathNodes.AddLast(Waypoint11);
        if (Waypoint12 != null) PathNodes.AddLast(Waypoint12);
        if (Waypoint13 != null) PathNodes.AddLast(Waypoint13);
        if (Waypoint14 != null) PathNodes.AddLast(Waypoint14);
        if (Waypoint15 != null) PathNodes.AddLast(Waypoint15);
        if (Waypoint16 != null) PathNodes.AddLast(Waypoint16);
    }
}