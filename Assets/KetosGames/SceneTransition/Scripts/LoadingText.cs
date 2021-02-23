using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace KetosGames.SceneTransition
{
    public class LoadingText : MonoBehaviour
    {
        private float lastUpdate = 0;
        private int numElipses = 1;

        void Update()
        {
            if (lastUpdate == 0 || Time.unscaledTime > (lastUpdate + 0.3f))
            {
                string t = "Loading";
                for (int i = 0; i < numElipses; i++)
                {
                    t += ".";
                }
                GetComponent<Text>().text = t;
                numElipses = numElipses == 3 ? 0 : numElipses + 1;

                lastUpdate = Time.unscaledTime;
            }
        }
    }
}
