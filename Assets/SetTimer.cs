using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SetTimer : MonoBehaviour
{
    [SerializeField] private int time = 0;
    [SerializeField] private UnityEvent action = default;

    public void StartTimer()
    {
        StartCoroutine(Timer(time));
    }

    private IEnumerator Timer(int time)
    {
        yield return new WaitForSeconds(time);

        action.Invoke();
    }
}
