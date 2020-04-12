using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class BuildScript
{
    static string[] SCENES = FindEnabledEditorScenes();

    static string APP_NAME = "BrokenConnections";
    static string TARGET_DIR = "./Builds/x86_64";

    [MenuItem("CI/Build Windows Standalone")]
    static void PerformWindowsBuild()
    {
        string target_dir = APP_NAME+ ".exe";
        GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, build_target);
        UnityEditor.Build.Reporting.BuildReport report = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded) {
            Debug.Log("CI - Build succeded");
        }
    }
}