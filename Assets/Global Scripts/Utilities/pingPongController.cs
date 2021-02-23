using UnityEngine;
using UnityEngine.UI;

public class pingPongController : MonoBehaviour
{
    Image logo;

    private void Start()
    {
        logo = GetComponent<Image>();
    }

    void Update()
    {
        logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, Mathf.PingPong(Time.time, 1));
    }
}
