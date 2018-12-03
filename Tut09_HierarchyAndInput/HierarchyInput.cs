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
    public class HierarchyInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private float _camAngle = 0;
        private float _speed = 0;
        private TransformComponent _baseTransform;
        private TransformComponent _bodyTransform;
        private TransformComponent _lowerArmTransform;
        private TransformComponent _upperArmTransform;
        private TransformComponent _pincerTransform;
        private TransformComponent _leftPincerTransform;
        private TransformComponent _rightPincerTransform;

        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };
            _bodyTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 6, 0)
            };
            _lowerArmTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(2, 4, 0)
            };
            _upperArmTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(-2, 8, 0)
            };
            _pincerTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, M.DegreesToRadians(90)),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 8.5f, -1.5f)
            };
            _leftPincerTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, M.DegreesToRadians(-90)),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, -1.5f, -1)
            };
            _rightPincerTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, M.DegreesToRadians(-90)),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 1.5f, -1)
            };
            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNodeContainer>
                {
                    //Grey Base
                    new SceneNodeContainer
                    {
                        Name = "Base",
                        Components = new List<SceneComponentContainer>
                        {
                            // TRANSFROM COMPONENT
                            _baseTransform,

                            // SHADER EFFECT COMPONENT
                            new ShaderEffectComponent
                            {
                                Effect = SimpleMeshes.MakeShaderEffect(new float3(0.7f, 0.7f, 0.7f), new float3(0.7f, 0.7f, 0.7f), 5)
                            },

                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        },
                        Children = new List<SceneNodeContainer>
                        {
                            //Red Body
                            new SceneNodeContainer
                            {
                                Name = "Body",
                                Components = new List<SceneComponentContainer>
                                {
                                    // TRANSFROM COMPONENT
                                    _bodyTransform,

                                    // SHADER EFFECT COMPONENT
                                    new ShaderEffectComponent
                                    {
                                        Effect = SimpleMeshes.MakeShaderEffect(new float3(1.0f, 0.3f, 0.3f), new float3(0.7f, 0.7f, 0.7f), 5)
                                    },

                                    // MESH COMPONENT
                                    SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                },
                                Children = new List<SceneNodeContainer>
                                {
                                    //Green Lower Arm
                                    new SceneNodeContainer
                                    {
                                        Name = "Lower Arm",
                                        Components = new List<SceneComponentContainer>
                                        {
                                            _lowerArmTransform,
                                        },
                                        Children = new List<SceneNodeContainer>
                                        {
                                            new SceneNodeContainer
                                            {
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    //TRANSFORM COMPONENT
                                                    new TransformComponent
                                                    {
                                                        Rotation = new float3(0, 0, 0),
                                                        Scale = new float3(1, 1, 1),
                                                        Translation = new float3(0, 4, 0)
                                                    },
                                                    // SHADER EFFECT COMPONENT
                                                    new ShaderEffectComponent
                                                    {
                                                        Effect = SimpleMeshes.MakeShaderEffect(new float3(0.3f, 1.0f, 0.3f), new float3(0.7f, 0.7f, 0.7f), 5)
                                                    },

                                                    // MESH COMPONENT
                                                    SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                                }
                                            },

                                            //Blue Upper Arm
                                            new SceneNodeContainer
                                            {
                                                Name = "Upper Arm",
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    _upperArmTransform
                                                },
                                                Children = new List<SceneNodeContainer>
                                                {
                                                    new SceneNodeContainer
                                                    {
                                                        Components = new List<SceneComponentContainer>
                                                        {
                                                            //TRANSFORM COMPONENT
                                                            new TransformComponent
                                                            {
                                                                Rotation = new float3(0, 0, 0),
                                                                Scale = new float3(1, 1, 1),
                                                                Translation = new float3(0, 4, 0)
                                                            },
                                                            // SHADER EFFECT COMPONENT
                                                            new ShaderEffectComponent
                                                            {
                                                                Effect = SimpleMeshes.MakeShaderEffect(new float3(0.3f, 0.3f, 1.0f), new float3(0.7f, 0.7f, 0.7f), 5)
                                                            },

                                                            // MESH COMPONENT
                                                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                                        }
                                                    },
                                                    //Pincer Base
                                                    new SceneNodeContainer
                                                    {
                                                        Name = "Pincer Base",
                                                        Components = new List<SceneComponentContainer>
                                                        {
                                                            _pincerTransform
                                                        },
                                                        Children = new List<SceneNodeContainer>
                                                        {
                                                            new SceneNodeContainer
                                                            {
                                                                Components = new List<SceneComponentContainer>
                                                                {
                                                                    //TRANSFORM COMPONENT
                                                                    new TransformComponent
                                                                    {
                                                                        Rotation = new float3(0, 0, 0),
                                                                        Scale = new float3(1, 1, 1),
                                                                        Translation = new float3(0, 0, 0)
                                                                    },
                                                                    // SHADER EFFECT COMPONENT
                                                                    new ShaderEffectComponent
                                                                    {
                                                                        Effect = SimpleMeshes.MakeShaderEffect(new float3(0.3f, 0.3f, 0.3f), new float3(0.7f, 0.7f, 0.7f), 5)
                                                                    },

                                                                    // MESH COMPONENT
                                                                    SimpleMeshes.CreateCuboid(new float3(1, 4, 1))
                                                                }
                                                            },
                                                            //Left Pincer
                                                            new SceneNodeContainer
                                                            {
                                                                Name = "Left Pincer",
                                                                Components = new List<SceneComponentContainer>
                                                                {
                                                                    _leftPincerTransform
                                                                },
                                                                Children = new List<SceneNodeContainer>
                                                                {
                                                                    new SceneNodeContainer
                                                                    {
                                                                        Components = new List<SceneComponentContainer>
                                                                        {
                                                                            //TRANSFORM COMPONENT
                                                                            new TransformComponent
                                                                            {
                                                                                Rotation = new float3(0, 0, 0),
                                                                                Scale = new float3(1, 1, 1),
                                                                                Translation = new float3(0, 1.5f, 0)
                                                                            },
                                                                            // SHADER EFFECT COMPONENT
                                                                            new ShaderEffectComponent
                                                                            {
                                                                                Effect = SimpleMeshes.MakeShaderEffect(new float3(0.3f, 0.3f, 0.3f), new float3(0.7f, 0.7f, 0.7f), 5)
                                                                            },

                                                                            // MESH COMPONENT
                                                                            SimpleMeshes.CreateCuboid(new float3(1, 4, 1))
                                                                        }
                                                                    }
                                                                }
                                                            },
                                                            //Right Pincer
                                                            new SceneNodeContainer
                                                            {
                                                                Name = "Right Pincer",
                                                                Components = new List<SceneComponentContainer>
                                                                {
                                                                    _rightPincerTransform
                                                                },
                                                                Children = new List<SceneNodeContainer>
                                                                {
                                                                    new SceneNodeContainer
                                                                    {
                                                                        Components = new List<SceneComponentContainer>
                                                                        {
                                                                            //TRANSFORM COMPONENT
                                                                            new TransformComponent
                                                                            {
                                                                                Rotation = new float3(0, 0, 0),
                                                                                Scale = new float3(1, 1, 1),
                                                                                Translation = new float3(0, 1.5f, 0)
                                                                            },
                                                                            // SHADER EFFECT COMPONENT
                                                                            new ShaderEffectComponent
                                                                            {
                                                                                Effect = SimpleMeshes.MakeShaderEffect(new float3(0.3f, 0.3f, 0.3f), new float3(0.7f, 0.7f, 0.7f), 5)
                                                                            },

                                                                            // MESH COMPONENT
                                                                            SimpleMeshes.CreateCuboid(new float3(1, 4, 1))
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
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
            Diagnostics.Log(_speed);

            RC.View = float4x4.CreateTranslation(0, -10, 50) * float4x4.CreateRotationY(_camAngle);

            //Move the robot
            _bodyTransform.Rotation = new float3(0, _bodyTransform.Rotation.y + Keyboard.LeftRightAxis * Time.DeltaTime, 0);
            _lowerArmTransform.Rotation = new float3(_lowerArmTransform.Rotation.x + Keyboard.UpDownAxis * Time.DeltaTime, 0, 0);
            _upperArmTransform.Rotation = new float3(_upperArmTransform.Rotation.x + Keyboard.WSAxis * Time.DeltaTime, 0, 0);

            //TODO: Start- and endangle + open and close pincers on button press
            _leftPincerTransform.Rotation = new float3(0, 0, _leftPincerTransform.Rotation.z + Keyboard.ADAxis * Time.DeltaTime);
            _rightPincerTransform.Rotation = new float3(0, 0, _rightPincerTransform.Rotation.z - Keyboard.ADAxis * Time.DeltaTime);

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

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}