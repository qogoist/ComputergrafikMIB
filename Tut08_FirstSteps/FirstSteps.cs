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
    public class FirstSteps : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private float _camAngle = 0;
        private float _cubeRotation = 0;
        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (intensities in R, G, B, A).
            RC.ClearColor = new float4(1, 1, 1, 1);

            // Create the scene containing the cube as the only object
            _scene = new SceneContainer();
            _scene.Children = new List<SceneNodeContainer>();
            createCubes(5);

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
        }

        private void createCubes(int number)
        {
            for (int i = 0; i < number; i++)
            {
                var cubeTransform = new TransformComponent
                {
                    Scale = new float3(1, 1, 1),
                    Translation = new float3(i * 10, 0, 0),
                    Rotation = new float3(0, 0, 0)
                };
                var cubeShader = new ShaderEffectComponent
                {
                    Effect = SimpleMeshes.MakeShaderEffect(new float3(0, 0, 0 + i * 0.2f), new float3(1, 1, 1), 2)
                };
                var cubeMesh = SimpleMeshes.CreateCuboid(new float3(5, 5, 5));

                // Assemble the cube node containing the three components
                var cubeNode = new SceneNodeContainer();
                cubeNode.Components = new List<SceneComponentContainer>();
                cubeNode.Components.Add(cubeTransform);
                cubeNode.Components.Add(cubeShader);
                cubeNode.Components.Add(cubeMesh);


                _scene.Children.Add(cubeNode);
            }
        }

        public Boolean isEven(int x)
        {
            var result = x % 2;
            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            _camAngle += 90.0f * M.Pi / 180.0f * DeltaTime;
            RC.View = float4x4.CreateTranslation(0, 0, 100) * float4x4.CreateRotationY(_camAngle);


            // Some Animation
            _cubeRotation += 360.0f * M.Pi / 180.0f * DeltaTime;
            for (int i = 0; i < _scene.Children.Count; i++)
            {
                var cube = (SceneNodeContainer)_scene.Children[i];
                var transform = (TransformComponent)cube.Components[0];
                var shader = (ShaderEffectComponent)cube.Components[1];

                if (isEven(i))
                {
                    transform.Translation = new float3(i * 10, 10 * M.Sin(1 * TimeSinceStart), 0);
                    //Shader Animation will make things lag like all hells
                    shader.Effect = SimpleMeshes.MakeShaderEffect(new float3(0 + 1*M.Sin(1 * TimeSinceStart), 0, 0 + i * 0.2f), new float3(1, 1, 1), 2);
                    transform.Rotation = new float3( 0, 0, _cubeRotation);
                } else {
                    transform.Translation = new float3(i*10 + 10 * M.Sin(1 * TimeSinceStart), 0, 0);
                    transform.Scale = new float3(2 * M.Sin(1 * TimeSinceStart), 2 * M.Sin(1 * TimeSinceStart), 2 * M.Sin(1 * TimeSinceStart));
                    transform.Rotation = new float3( _cubeRotation, 0, 0);
                }
            }


            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }


        // Is called when the window was resized
        public override void Resize()
        {
            // Set the new rendering area to the entire new windows size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}