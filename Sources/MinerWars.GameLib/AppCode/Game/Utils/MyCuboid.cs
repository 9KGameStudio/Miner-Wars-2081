﻿using System;
using System.Collections.Generic;
using MinerWarsMath;

 
namespace MinerWars.AppCode.Game.Utils
{
    //  6 - 7  
    // /   /|
    //4|- 5 |
    //|2 -|3
    //|/  |/
    //0 - 1                
    public class MyCuboidSide
    {
        public MyPlane Plane = new MyPlane();
        public MyLine[] Lines = new MyLine[4];

        public MyCuboidSide()
        {
            Lines[0] = new MyLine();
            Lines[1] = new MyLine();
            Lines[2] = new MyLine();
            Lines[3] = new MyLine();
        }

        public void CreatePlaneFromLines()
        {
            Plane.Normal = Vector3.Cross(Lines[1].Direction, Lines[0].Direction);
            Plane.Point = Lines[0].From;
        }
    }

    public class MyCuboid
    {
        public MyCuboidSide[] Sides = new MyCuboidSide[6];

        public MyCuboid()
        {
            Sides[0] = new MyCuboidSide();
            Sides[1] = new MyCuboidSide();
            Sides[2] = new MyCuboidSide();
            Sides[3] = new MyCuboidSide();
            Sides[4] = new MyCuboidSide();
            Sides[5] = new MyCuboidSide();
        }

        public IEnumerable<MyLine> UniqueLines
        {
            get
            {
                yield return Sides[0].Lines[0];
                yield return Sides[0].Lines[1];
                yield return Sides[0].Lines[2];
                yield return Sides[0].Lines[3];

                yield return Sides[1].Lines[0];
                yield return Sides[1].Lines[1];
                yield return Sides[1].Lines[2];
                yield return Sides[1].Lines[3];

                yield return Sides[2].Lines[0];
                yield return Sides[2].Lines[2];
                yield return Sides[4].Lines[1];
                yield return Sides[5].Lines[2];
            }
        }

        public IEnumerable<Vector3> Vertices
        {
            get
            {
                yield return Sides[2].Lines[1].From;
                yield return Sides[2].Lines[1].To;
                yield return Sides[0].Lines[1].From;
                yield return Sides[0].Lines[1].To;

                yield return Sides[1].Lines[2].From;
                yield return Sides[1].Lines[2].To;
                yield return Sides[3].Lines[2].From;
                yield return Sides[3].Lines[2].To;
            }
        }

        public void CreateFromVertices(Vector3[] vertices)
        {
            Vector3 min = new Vector3(float.MaxValue);
            Vector3 max = new Vector3(float.MinValue);
            foreach (Vector3 v in vertices)
            {
                min = Vector3.Min(v, min);
                max = Vector3.Min(v, max);
            }

            MyLine line02 = new MyLine(vertices[0], vertices[2], false);
            MyLine line23 = new MyLine(vertices[2], vertices[3], false);
            MyLine line31 = new MyLine(vertices[3], vertices[1], false);
            MyLine line10 = new MyLine(vertices[1], vertices[0], false);

            MyLine line76 = new MyLine(vertices[7], vertices[6], false);
            MyLine line64 = new MyLine(vertices[6], vertices[4], false);
            MyLine line45 = new MyLine(vertices[4], vertices[5], false);
            MyLine line57 = new MyLine(vertices[5], vertices[7], false);

            MyLine line40 = new MyLine(vertices[4], vertices[0], false);
            MyLine line01 = new MyLine(vertices[0], vertices[1], false);
            MyLine line15 = new MyLine(vertices[1], vertices[5], false);
            MyLine line54 = new MyLine(vertices[5], vertices[4], false);

            MyLine line32 = new MyLine(vertices[3], vertices[2], false);
            MyLine line26 = new MyLine(vertices[2], vertices[6], false);
            MyLine line67 = new MyLine(vertices[6], vertices[7], false);
            MyLine line73 = new MyLine(vertices[7], vertices[3], false);

            MyLine line13 = new MyLine(vertices[1], vertices[3], false);
            MyLine line37 = new MyLine(vertices[3], vertices[7], false);
            MyLine line75 = new MyLine(vertices[7], vertices[5], false);
            MyLine line51 = new MyLine(vertices[5], vertices[1], false);

            MyLine line04 = new MyLine(vertices[0], vertices[4], false);
            MyLine line46 = new MyLine(vertices[4], vertices[6], false);
            MyLine line62 = new MyLine(vertices[6], vertices[2], false);
            MyLine line20 = new MyLine(vertices[2], vertices[0], false);

            Sides[0].Lines[0] = line02;
            Sides[0].Lines[1] = line23;
            Sides[0].Lines[2] = line31;
            Sides[0].Lines[3] = line10;
            Sides[0].CreatePlaneFromLines();

            Sides[1].Lines[0] = line76;
            Sides[1].Lines[1] = line64;
            Sides[1].Lines[2] = line45;
            Sides[1].Lines[3] = line57;
            Sides[1].CreatePlaneFromLines();

            Sides[2].Lines[0] = line40;
            Sides[2].Lines[1] = line01;
            Sides[2].Lines[2] = line15;
            Sides[2].Lines[3] = line54;
            Sides[2].CreatePlaneFromLines();

            Sides[3].Lines[0] = line32;
            Sides[3].Lines[1] = line26;
            Sides[3].Lines[2] = line67;
            Sides[3].Lines[3] = line73;
            Sides[3].CreatePlaneFromLines();

            Sides[4].Lines[0] = line13;
            Sides[4].Lines[1] = line37;
            Sides[4].Lines[2] = line75;
            Sides[4].Lines[3] = line51;
            Sides[4].CreatePlaneFromLines();

            Sides[5].Lines[0] = line04;
            Sides[5].Lines[1] = line46;
            Sides[5].Lines[2] = line62;
            Sides[5].Lines[3] = line20;
            Sides[5].CreatePlaneFromLines();
        }

        public void CreateFromSizes(float width1, float depth1, float width2, float depth2, float length)
        {
            float halfLength = length * 0.5f;
            float halfWidth1 = width1 * 0.5f;
            float halfWidth2 = width2 * 0.5f;
            float halfDepth1 = depth1 * 0.5f;
            float halfDepth2 = depth2 * 0.5f;

            Vector3[] vertices = new Vector3[8];
            vertices[0] = new Vector3(-halfWidth2, -halfLength, -halfDepth2);
            vertices[1] = new Vector3(halfWidth2, -halfLength, -halfDepth2);
            vertices[2] = new Vector3(-halfWidth2, -halfLength, halfDepth2);
            vertices[3] = new Vector3(halfWidth2, -halfLength, halfDepth2);
            vertices[4] = new Vector3(-halfWidth1, halfLength, -halfDepth1);
            vertices[5] = new Vector3(halfWidth1, halfLength, -halfDepth1);
            vertices[6] = new Vector3(-halfWidth1, halfLength, halfDepth1);
            vertices[7] = new Vector3(halfWidth1, halfLength, halfDepth1);

            CreateFromVertices(vertices);
        }

        public BoundingBox GetAABB()
        {
            BoundingBox aabb = BoundingBoxHelper.InitialBox;

            foreach (MyLine line in UniqueLines)
            {
                Vector3 from = line.From;
                Vector3 to = line.To;
                aabb = aabb.Include(ref from);
                aabb = aabb.Include(ref to);
            }

            return aabb;
        }

        public BoundingBox GetLocalAABB()
        {
            BoundingBox aabb = GetAABB();

            Vector3 center = aabb.GetCenter();
            aabb.Min -= center;
            aabb.Max -= center;

            return aabb;
        }

        public MyCuboid CreateTransformed(ref Matrix worldMatrix)
        {
            Vector3[] vertices = new Vector3[8];

            int i = 0;
            foreach (Vector3 vertex in Vertices)
            {
                vertices[i] = Vector3.Transform(vertex, worldMatrix);
                i++;
            }

            MyCuboid transformedCuboid = new MyCuboid();
            transformedCuboid.CreateFromVertices(vertices);
            return transformedCuboid;
        }
    }
}
