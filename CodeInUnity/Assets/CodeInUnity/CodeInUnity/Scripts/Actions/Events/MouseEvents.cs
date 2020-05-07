using UnityEngine;
using UnityEngine.Events;

public class MouseEvents : MonoBehaviour
{
    public UnityEvent mouseDown;
    public UnityEvent mouseEnter;
    public UnityEvent mouseLeave;
    public UnityEvent mouseUp;

    void OnMouseUp()
    {
        mouseUp?.Invoke();
    }

    void OnMouseDown()
    {
        mouseDown?.Invoke();
    }

    void OnMouseOver()
    {
        mouseEnter?.Invoke();
    }

    void OnMouseExit()
    {
        mouseLeave?.Invoke();
    }
}
