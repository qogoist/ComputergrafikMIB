using System;
using System.Collections.Generic;
using System.Linq;
using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static System.Math;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;

namespace Fusee.Tutorial.Core
{
    public class AssetsPicking : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private TransformComponent _baseTransform;

        private ScenePicker _scenePicker;
        private PickResult _currentPick;
        private float3 _oldColor;
        private TransformComponent _turretTransform;
        private TransformComponent _armTransform;
        private TransformComponent _bodyTransform;
        private TransformComponent _frontAxle;
        private TransformComponent _backAxle;
        private float _camAngle = 0;
        private float _speed = 0;

        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };

            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNodeContainer>
                {
                    new SceneNodeContainer
                    {
                        Components = new List<SceneComponentContainer>
                        {
                            // TRANSFROM COMPONENT
                            _baseTransform,

                            // SHADER EFFECT COMPONENT
                            new ShaderEffectComponent
                            {
                                Effect = SimpleMeshes.MakeShaderEffect(new float3(0.7f, 0.7f, 0.7f), new float3(1, 1, 1), 5)
                            },

                            // MESH COMPONENT
                            // SimpleAssetsPickinges.CreateCuboid(new float3(10, 10, 10))
                            SimpleMeshes.CreateCuboid(new float3(10, 10, 10))
                        }
                    },
                }
            };
        }

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = AssetStorage.Get<SceneContainer>("CubeCar.fus");

            _turretTransform = _scene.Children.FindNodes(node => node.Name == "Turret")?.FirstOrDefault()?.GetTransform();
            _armTransform = _scene.Children.FindNodes(node => node.Name == "Arm")?.FirstOrDefault()?.GetTransform();
            _bodyTransform = _scene.Children.FindNodes(node => node.Name == "Body")?.FirstOrDefault()?.GetTransform();
            _frontAxle = _scene.Children.FindNodes(node => node.Name == "FrontAxle")?.FirstOrDefault()?.GetTransform();
            _backAxle = _scene.Children.FindNodes(node => node.Name == "RearAxle")?.FirstOrDefault()?.GetTransform();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
            _scenePicker = new ScenePicker(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            //Setup the Camera
            if (Mouse.LeftButton)
            {
                _speed = Mouse.Velocity.x * 0.000025f;
                _camAngle -= _speed;
            }
            else
            {
                _camAngle -= _speed;
                _speed = _speed * 0.9f;
            }

            RC.View = float4x4.CreateTranslation(0, -10, 50) * float4x4.CreateRotationY(_camAngle);

            if (Mouse.LeftButton)
            {
                float2 pickPosClip = Mouse.Position * new float2(2.0f / Width, -2.0f / Height) + new float2(-1, 1);
                _scenePicker.View = RC.View;
                _scenePicker.Projection = RC.Projection;

                List<PickResult> pickResults = _scenePicker.Pick(pickPosClip).ToList();
                PickResult newPick = null;
                if (pickResults.Count > 0)
                {
                    pickResults.Sort((a, b) => Sign(a.ClipPos.z - b.ClipPos.z));
                    newPick = pickResults[0];
                }

                if (newPick?.Node != _currentPick?.Node)
                {
                    if (_currentPick != null)
                    {
                        ShaderEffectComponent shaderEffectComponent = _currentPick.Node.GetComponent<ShaderEffectComponent>();
                        shaderEffectComponent.Effect.SetEffectParam("DiffuseColor", _oldColor);
                    }
                    if (newPick != null)
                    {
                        ShaderEffectComponent shaderEffectComponent = newPick.Node.GetComponent<ShaderEffectComponent>();
                        _oldColor = (float3)shaderEffectComponent.Effect.GetEffectParam("DiffuseColor");
                        shaderEffectComponent.Effect.SetEffectParam("DiffuseColor", new float3(1, 0.4f, 0.4f));
                    }
                    _currentPick = newPick;
                    //Diagnostics.Log(_currentPick.Node.Name);
                }
            }

            if (_currentPick != null)
            {
                switch (_currentPick.Node.Name)
                {
                    case "Arm":
                        _armTransform.Rotation.x = _armTransform.Rotation.x + Keyboard.UpDownAxis * Time.DeltaTime;
                        break;
                    case "Turret":
                        _turretTransform.Rotation.y = _turretTransform.Rotation.y + Keyboard.LeftRightAxis * Time.DeltaTime;
                        break;
                    default:
                        break;
                }
            }

            float rotVel = 0.05f;
            float posVel = 1;
            float wheelRot = 0.1f;
            bool turn = false;


            if (Keyboard.GetButton(65) || Keyboard.GetButton(68))
            {
                //Diagnostics.Log(_frontAxle.Rotation.y);
                if (_frontAxle.Rotation.y > M.DegreesToRadians(-10) && _frontAxle.Rotation.y < M.DegreesToRadians(10))
                {
                    float tempRot = wheelRot * Keyboard.ADAxis;
                    _frontAxle.Rotation.y = tempRot;
                }
                turn = true;
            }
            else
            {
                _frontAxle.Rotation.y = 0;
            }


            if (Keyboard.GetButton(87) || Keyboard.GetButton(83))
            {
                float3 newPos = _bodyTransform.Translation;
                newPos.x += Keyboard.WSAxis * posVel * M.Sin(_bodyTransform.Rotation.y);
                newPos.z += Keyboard.WSAxis * posVel * M.Cos(_bodyTransform.Rotation.y);
                _bodyTransform.Translation = newPos;

                _frontAxle.Rotation.x += wheelRot * Keyboard.WSAxis;
                _backAxle.Rotation.x += wheelRot * Keyboard.WSAxis;

                if (turn)
                {   
                    float orientation = Keyboard.WSAxis * Keyboard.ADAxis;
                    float newYRot = _bodyTransform.Rotation.y + rotVel * orientation;
                    _bodyTransform.Rotation = new float3(0, newYRot, 0);
                }
            }

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered farame) on the front buffer.
            Present();
        }


        // Is called when the window was resized
        public override void Resize()
        {
            // Set the new rendering area to the entire new windows size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45� Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}
