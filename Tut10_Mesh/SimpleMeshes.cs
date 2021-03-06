﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;

namespace Fusee.Tutorial.Core
{
    public static class SimpleMeshes
    {
        public static Mesh CreateCuboid(float3 size)
        {
            return new Mesh
            {
                Vertices = new[]
                {
                    // left, bottom, front vertex
                    new float3(-0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 0  - belongs to left
                    new float3(-0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 1  - belongs to bottom
                    new float3(-0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 2  - belongs to front

                    // left, bottom, back vertex
                    new float3(-0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 3  - belongs to left
                    new float3(-0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 4  - belongs to bottom
                    new float3(-0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 5  - belongs to back

                    // left, up, front vertex
                    new float3(-0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 6  - belongs to left
                    new float3(-0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 7  - belongs to up
                    new float3(-0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 8  - belongs to front

                    // left, up, back vertex
                    new float3(-0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 9  - belongs to left
                    new float3(-0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 10 - belongs to up
                    new float3(-0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 11 - belongs to back

                    // right, bottom, front vertex
                    new float3( 0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 12 - belongs to right
                    new float3( 0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 13 - belongs to bottom
                    new float3( 0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 14 - belongs to front

                    // right, bottom, back vertex
                    new float3( 0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 15 - belongs to right
                    new float3( 0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 16 - belongs to bottom
                    new float3( 0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 17 - belongs to back

                    // right, up, front vertex
                    new float3( 0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 18 - belongs to right
                    new float3( 0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 19 - belongs to up
                    new float3( 0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 20 - belongs to front

                    // right, up, back vertex
                    new float3( 0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 21 - belongs to right
                    new float3( 0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 22 - belongs to up
                    new float3( 0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 23 - belongs to back

                },
                Normals = new[]
                {
                    // left, bottom, front vertex
                    new float3(-1,  0,  0), // 0  - belongs to left
                    new float3( 0, -1,  0), // 1  - belongs to bottom
                    new float3( 0,  0, -1), // 2  - belongs to front

                    // left, bottom, back vertex
                    new float3(-1,  0,  0),  // 3  - belongs to left
                    new float3( 0, -1,  0),  // 4  - belongs to bottom
                    new float3( 0,  0,  1),  // 5  - belongs to back

                    // left, up, front vertex
                    new float3(-1,  0,  0),  // 6  - belongs to left
                    new float3( 0,  1,  0),  // 7  - belongs to up
                    new float3( 0,  0, -1),  // 8  - belongs to front

                    // left, up, back vertex
                    new float3(-1,  0,  0),  // 9  - belongs to left
                    new float3( 0,  1,  0),  // 10 - belongs to up
                    new float3( 0,  0,  1),  // 11 - belongs to back

                    // right, bottom, front vertex
                    new float3( 1,  0,  0), // 12 - belongs to right
                    new float3( 0, -1,  0), // 13 - belongs to bottom
                    new float3( 0,  0, -1), // 14 - belongs to front

                    // right, bottom, back vertex
                    new float3( 1,  0,  0),  // 15 - belongs to right
                    new float3( 0, -1,  0),  // 16 - belongs to bottom
                    new float3( 0,  0,  1),  // 17 - belongs to back

                    // right, up, front vertex
                    new float3( 1,  0,  0),  // 18 - belongs to right
                    new float3( 0,  1,  0),  // 19 - belongs to up
                    new float3( 0,  0, -1),  // 20 - belongs to front

                    // right, up, back vertex
                    new float3( 1,  0,  0),  // 21 - belongs to right
                    new float3( 0,  1,  0),  // 22 - belongs to up
                    new float3( 0,  0,  1),  // 23 - belongs to back
                },
                Triangles = new ushort[]
                {
                    0,  6,  3,     3,  6,  9,  // left
                    2, 14, 20,     2, 20,  8,  // front
                    12, 15, 18,    15, 21, 18, // right
                    5, 11, 17,    17, 11, 23,  // back
                    7, 22, 10,     7, 19, 22,  // top
                    1,  4, 16,     1, 16, 13,  // bottom 
                },
                UVs = new float2[]
                {
                    // left, bottom, front vertex
                    new float2( 1,  0), // 0  - belongs to left
                    new float2( 1,  0), // 1  - belongs to bottom
                    new float2( 0,  0), // 2  - belongs to front

                    // left, bottom, back vertex
                    new float2( 0,  0),  // 3  - belongs to left
                    new float2( 1,  1),  // 4  - belongs to bottom
                    new float2( 1,  0),  // 5  - belongs to back

                    // left, up, front vertex
                    new float2( 1,  1),  // 6  - belongs to left
                    new float2( 0,  0),  // 7  - belongs to up
                    new float2( 0,  1),  // 8  - belongs to front

                    // left, up, back vertex
                    new float2( 0,  1),  // 9  - belongs to left
                    new float2( 0,  1),  // 10 - belongs to up
                    new float2( 1,  1),  // 11 - belongs to back

                    // right, bottom, front vertex
                    new float2( 0,  0), // 12 - belongs to right
                    new float2( 0,  0), // 13 - belongs to bottom
                    new float2( 1,  0), // 14 - belongs to front

                    // right, bottom, back vertex
                    new float2( 1,  0),  // 15 - belongs to right
                    new float2( 0,  1),  // 16 - belongs to bottom
                    new float2( 0,  0),  // 17 - belongs to back

                    // right, up, front vertex
                    new float2( 0,  1),  // 18 - belongs to right
                    new float2( 1,  0),  // 19 - belongs to up
                    new float2( 1,  1),  // 20 - belongs to front

                    // right, up, back vertex
                    new float2( 1,  1),  // 21 - belongs to right
                    new float2( 1,  1),  // 22 - belongs to up
                    new float2( 0,  1),  // 23 - belongs to back                    
                },
                BoundingBox = new AABBf(-0.5f * size, 0.5f * size)
            };
        }

        public static ShaderEffect MakeShaderEffect(float3 diffuseColor, float3 specularColor, float shininess)
        {
            MaterialComponent temp = new MaterialComponent
            {
                Diffuse = new MatChannelContainer
                {
                    Color = diffuseColor
                },
                Specular = new SpecularChannelContainer
                {
                    Color = specularColor,
                    Shininess = shininess
                }
            };

            return ShaderCodeBuilder.MakeShaderEffectFromMatComp(temp);
        }


        public static Mesh CreateCylinder(float radius, float height, int segments)
        {
            return CreateConeFrustum(radius, radius, height, segments);
        }

        public static Mesh CreateCone(float radius, float height, int segments)
        {
            return CreateConeFrustum(radius, 0.0f, height, segments);
        }

        public static Mesh CreateConeFrustum(float radiuslower, float radiusupper, float height, int segments)
        {
            Mesh mesh = new Mesh
            {
                Vertices = new float3[2 + segments * 4],
                Normals = new float3[2 + segments * 4],
                Triangles = new ushort[segments * 12],
                //UVs = new float2[2 + segments * 4],
            };

            float angle = 360f / (float)segments;

            //Center Points
            mesh.Vertices[4 * segments] = new float3(0, -(height / 2), 0);
            mesh.Normals[4 * segments] = new float3(0, -1, 0);
            mesh.Vertices[4 * segments + 1] = new float3(0, (height / 2), 0);
            mesh.Normals[4 * segments + 1] = new float3(0, 1, 0);

            for (int i = 0; i < segments; i++)
            {
                //lower circle
                mesh.Vertices[i] = new float3(radiuslower * M.Cos(M.DegreesToRadians(i * angle)), -(height / 2), radiuslower * M.Sin(M.DegreesToRadians(i * angle)));
                mesh.Normals[i] = new float3(0, -1, 0);

                mesh.Vertices[segments + i] = new float3(radiuslower * M.Cos(M.DegreesToRadians(i * angle)), -(height / 2), radiuslower * M.Sin(M.DegreesToRadians(i * angle)));
                mesh.Normals[segments + i] = new float3(M.Cos(M.DegreesToRadians(i * angle)), 0, M.Sin(M.DegreesToRadians(i * angle)));

                //Upper circle
                mesh.Vertices[2 * segments + i] = new float3(radiusupper * M.Cos(M.DegreesToRadians(i * angle)), (height / 2), radiusupper * M.Sin(M.DegreesToRadians(i * angle)));
                mesh.Normals[2 * segments + i] = new float3(0, 1, 0);

                mesh.Vertices[3 * segments + i] = new float3(radiusupper * M.Cos(M.DegreesToRadians(i * angle)), (height / 2), radiusupper * M.Sin(M.DegreesToRadians(i * angle)));
                mesh.Normals[3 * segments + i] = new float3(M.Cos(M.DegreesToRadians(i * angle)), 0, M.Sin(M.DegreesToRadians(i * angle)));

                //Triangles Lower circle
                mesh.Triangles[3 * i] = (ushort)(4 * segments);
                mesh.Triangles[3 * i + 1] = (ushort)(i);
                mesh.Triangles[3 * i + 2] = (ushort)(i + 1);

                //Triangles Upper circle
                mesh.Triangles[(segments * 3) + 3 * i] = (ushort)(4 * segments + 1);
                mesh.Triangles[(segments * 3) + 3 * i + 1] = (ushort)(2 * segments + i);
                mesh.Triangles[(segments * 3) + 3 * i + 2] = (ushort)(2 * segments + i + 1);

                //Triangles cylinder
                mesh.Triangles[(segments * 6) + 3 * i] = (ushort)(segments + i);
                mesh.Triangles[(segments * 6) + 3 * i + 1] = (ushort)(segments + i + 1);
                mesh.Triangles[(segments * 6) + 3 * i + 2] = (ushort)(3 * segments + i + 1);

                mesh.Triangles[(segments * 9) + 3 * i] = (ushort)(segments + i);
                mesh.Triangles[(segments * 9) + 3 * i + 1] = (ushort)(3 * segments + i + 1);
                mesh.Triangles[(segments * 9) + 3 * i + 2] = (ushort)(3 * segments + i);
            }

            //Last piece
            mesh.Triangles[3 * (segments - 1)] = (ushort)(4 * segments);
            mesh.Triangles[3 * (segments - 1) + 1] = (ushort)(segments - 1);
            mesh.Triangles[3 * (segments - 1) + 2] = (ushort)0;

            mesh.Triangles[segments * 3 + 3 * (segments - 1)] = (ushort)(4 * segments + 1);
            mesh.Triangles[segments * 3 + 3 * (segments - 1) + 1] = (ushort)(3 * segments - 1);
            mesh.Triangles[segments * 3 + 3 * (segments - 1) + 2] = (ushort)(2 * segments);

            mesh.Triangles[segments * 6 + 3 * (segments - 1)] = (ushort)(2 * segments - 1);
            mesh.Triangles[segments * 6 + 3 * (segments - 1) + 1] = (ushort)(segments);
            mesh.Triangles[segments * 6 + 3 * (segments - 1) + 2] = (ushort)(3 * segments);

            mesh.Triangles[segments * 9 + 3 * (segments - 1)] = (ushort)(2 * segments - 1);
            mesh.Triangles[segments * 9 + 3 * (segments - 1) + 1] = (ushort)(3 * segments);
            mesh.Triangles[segments * 9 + 3 * (segments - 1) + 2] = (ushort)(4 * segments - 1);

            return mesh;
        }

        public static Mesh CreatePyramid(float baselen, float height)
        {
            throw new NotImplementedException();
        }
        public static Mesh CreateTetrahedron(float edgelen)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreateTorus(float mainradius, float segradius, int segments, int slices)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreateCircle(float radius, int segments)
        {
            Mesh mesh = new Mesh { };

            mesh.Vertices = new float3[segments + 1];
            mesh.Normals = new float3[segments + 1];
            mesh.Triangles = new ushort[segments * 3];

            float angle = 360f / (float)segments;

            //Center Point
            mesh.Vertices[segments] = new float3(0, 0, 0);
            mesh.Normals[segments] = new float3(0, 1, 0);

            for (int i = 0; i < segments; i++)
            {
                mesh.Vertices[i] = new float3(radius * M.Cos(M.DegreesToRadians(i * angle)), 0, radius * M.Sin(M.DegreesToRadians(i * angle)));
                mesh.Normals[i] = new float3(0, 1, 0);

                mesh.Triangles[3 * i] = (ushort)segments;
                mesh.Triangles[3 * i + 1] = (ushort)(i);
                mesh.Triangles[3 * i + 2] = (ushort)(i + 1);
            }

            //last Triangle piece
            mesh.Triangles[3 * (segments - 1)] = (ushort)segments;
            mesh.Triangles[3 * (segments - 1) + 1] = (ushort)(segments - 1);
            mesh.Triangles[3 * (segments - 1) + 2] = (ushort)0;

            return mesh;
        }

    }
}
