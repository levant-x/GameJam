using UnityEngine;
using UnityEngine.SceneManagement;

namespace MurphyInc
{
    public class BoosterScene : MonoBehaviour
    {
        void Start()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}