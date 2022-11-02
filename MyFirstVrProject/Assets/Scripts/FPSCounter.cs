using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    private Text fps_text;
    private float count;

    private IEnumerator Start()
    {
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            fps_text.text = count.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
