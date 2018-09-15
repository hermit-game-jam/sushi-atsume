using UnityEditor;

public class WebGLBuilder
{
    static void Build()
    {
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "gh-pages", BuildTarget.WebGL, BuildOptions.None);
    }
}
