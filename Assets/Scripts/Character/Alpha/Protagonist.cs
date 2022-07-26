using System;
using RuntimeAnchors;
using UnityEngine;

namespace Characters.Alpha1
{
    /// <summary>
    /// <para>This component consumes input from the InputReader and stores its values. The input is then read, and manipulated, by the StateMachine's Actions.</para>
    /// </summary>
    public class Protagonist : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;

        [SerializeField]
        private TransformAnchor _gameplayCameraTransform;
        
        private Vector2 _inputVector;
        private float _previousSpeed;

        //These fields are read and manipulated by StateMachine Actions;
        [NonSerialized]
        public Vector3 movementInput;
        [NonSerialized]
        public Vector3 movementVector;

        public const float GRAVITY_MULTIPLIER = 5f;
        public const float MAX_FALL_SPEED = -50f;
        public const float MAX_RISE_SPEED = 100f;
        public const float GRAVITY_COMEBACK_MULTIPLIER = .03f;
        public const float GRAVITY_DIVIDER = .6f;
        public const float AIR_RESISTANCE = 5f;

        //Adds listeners for events being triggered in the InputReader script
        private void OnEnable()
        {
            _inputReader.MoveEvent += OnMove;
            //...
        }

        //Removes all listeners to the events coming from the InputReader script
        private void OnDisable()
        {
            _inputReader.MoveEvent -= OnMove;
            //...
        }

        private void Update()
        {
            RecalculateMovement();
        }

        private void RecalculateMovement()
        {
            // Accelerate/Decelerate
            float targetSpeed = Mathf.Clamp01(_inputVector.magnitude);
            targetSpeed = Mathf.Lerp(_previousSpeed, targetSpeed, Time.deltaTime * 4.0f);
            
            // Assignment of Values
            movementInput = new Vector3(_inputVector.x, 0f, _inputVector.y);

            _previousSpeed = targetSpeed;
        }
        
        //---- Event Listeners ---- 

        private void OnMove(Vector2 movement)
        {
            _inputVector = movement;
        }
    }
}
/*# This .gitignore file should be placed at the root of your Unity project directory
#
# Get latest from https://github.com/github/gitignore/blob/master/Unity.gitignore
#
[Ll]ibrary/
[Tt]emp/
[Oo]bj/
[Bb]uild/
/[Bb]uilds/
/[Ll]ogs/
/[Mm]emoryCaptures/
/UserSettings/

# Asset meta data should only be ignored when the corresponding asset is also ignored
/!/[Aa]ssets/**//*.meta

# Uncomment this line if you wish to ignore the asset store tools plugin
# /[Aa]ssets/AssetStoreTools*

# Autogenerated Jetbrains Rider plugin
    [Aa]ssets/Plugins/Editor/JetBrains*

# Visual Studio cache directory
    .vs/
    .vscode/

# Idea cache directory
    .idea/

# Gradle cache directory
    .gradle/

# Autogenerated VS/MD/Consulo solution and project files
    ExportedObj/
    .consulo/
    *.csproj
    *.unityproj
    *.sln
    *.suo
    *.tmp
    *.user
    *.userprefs
    *.pidb
    *.booproj
    *.svd
    *.pdb
    *.mdb
    *.opendb
    *.VC.db

# Unity3D generated meta files
    *.pidb.meta
    *.pdb.meta
    *.mdb.meta

# Unity3D generated file on crash reports
sysinfo.txt
debug.log

# Builds
    *.apk
    *.unitypackage

#Mac invisible folder files
    *.DS_Store

# Crashlytics generated file
crashlytics-build.properties

# Any image or video created by the Recorder package
    /Recordings/

# Package related files that can be ignored or are better kept user-specific
    ProjectSettings/Packages/com.unity.probuilder/Settings.json
ProjectSettings/Packages/com.unity.polybrush/Settings.json
    /ProjectSettings/Packages/com.unity.progrids/Settings.json
    /ProjectSettings/Packages/com.unity.learn.iet-framework/Settings.json
Assets/Polybrush Data*
    Assets/PolybrushData*

# Asset Bundles folder and .bin file
    /Assets/StreamingAssets
    *.bin

# A marker file which is used to decide whether to run the first-launch experience, for example show the welcome dialog
InitCodeMarker

# Tutorial Framework has a backup option for the project's content, let's ignore the possible backup folder
    /Tutorial Defaults/
    */
