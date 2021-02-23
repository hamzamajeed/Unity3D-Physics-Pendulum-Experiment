using UnityEngine;
using System.Collections;
using KetosGames.SceneTransition;

namespace KetosGames.SceneTransition.Example
{
    public class GoScript : MonoBehaviour
    {
        public string ToScene;

        public void GoToNextScene()
        {
            SceneLoader.LoadScene(ToScene);
        }
    }
}
