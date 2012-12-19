﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEnginev2.Data;
using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;

namespace EntityEnginev2.Components
{
    public class Physics : Component
    {
        public float AngularVelocity;
        public float AngularVelocityDrag = 1f;
        public Vector2 Velocity = Vector2.Zero;
        public float Drag = 1f;

        public Physics(Entity e, string name)
            : base(e, name)
        {
        }

        public override void Update()
        {
            Velocity *= Drag;
            AngularVelocity *= AngularVelocityDrag;

            Entity.GetComponent<Body>().Position += Velocity;
            Entity.GetComponent<Body>().Angle += AngularVelocity;
        }

        public void Thrust(float power)
        {
            var angle = Entity.GetComponent<Body>().Angle;
            Thrust(power, angle);
        }

        public void Thrust(float power, float angle)
        {
            Velocity.X -= (float)Math.Sin(-angle) * power;
            Velocity.Y -= (float)Math.Cos(-angle) * power;
        }

        public void FaceVelocity()
        {
            Entity.GetComponent<Body>().Angle = (float)Math.Atan2(Velocity.X, Velocity.Y);
        }

        public void FaceVelocity(Vector2 velocity)
        {
            Entity.GetComponent<Body>().Angle = (float)Math.Atan2(velocity.X, velocity.Y);
        }

        public override void ParseXml(XmlParser xmlparser)
        {
            string rootnode = xmlparser.GetRootNode();
            rootnode = rootnode + "->" + Name + "->";

            try
            {
                Drag = xmlparser.GetFloat(rootnode + "Drag");
            }
            catch { }

            try
            {
                AngularVelocity = xmlparser.GetFloat(rootnode + "AngularVelocity");
            }
            catch { }

            try
            {
                Velocity = xmlparser.GetVector2(rootnode + "Velocity");
            }
            catch { }
        }
    }
}
