using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] List<string> sceneNames;

        void Start()
        {
            var loaded = Enumerable.Range(0, SceneManager.sceneCount)
                .Select(SceneManager.GetSceneAt)
                .Select(s => s.name).ToList();
            LoadScenesAdditive(sceneNames.Except(loaded));
        }

        static void LoadScenesAdditive(IEnumerable<string> sceneNames)
        {
            foreach (var name in sceneNames)
            {
                SceneManager.LoadScene(name, LoadSceneMode.Additive);
            }
        }
    }
}