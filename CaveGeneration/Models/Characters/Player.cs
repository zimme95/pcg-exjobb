﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CaveGeneration.Models.Characters
{
    public class Player : Character
    {
        public bool Alive { get; set; }

        private int frameCount = 0;
        bool groundJump;

        List<Action> oldInput = null;

        public Player(Texture2D texture, Vector2 position, SpriteBatch spiteBatch)
        {
            Position = position;
            Texture = texture;
            SpriteBatch = spiteBatch;
            MaxSpeed = 2;
            JumpingHeight = texture.Height * 1.5f;
            Gravity = 2;
            Alive = true;
        }

        public override void Update(GameTime gametime)
        {
            GetInputAndUpdateMovement();
            SimulateGravity();
            SimulateFriction();
            UpdatePosition(gametime);
            CollisionHandling(gametime);
        }

        private void GetInputAndUpdateMovement()
        {
            List<Action> actions = Input.GetInput();
            if(groundJump)
            frameCount++;
            if (frameCount > 10)
            {
                groundJump = false;
                frameCount = 0;
            }

            bool leftWall = IsByLeftWall();
            bool rightWall = IsByRightWall();

            if ((leftWall || rightWall) && !IsOnGround())
            {
                if (actions.Contains(Action.MoveUp) && (actions.Contains(Action.MoveLeft) && leftWall && !groundJump))
                {
                    if (!oldInput.Contains(Action.MoveUp)) { 
                        Movement = -Vector2.UnitY * (JumpingHeight * 1.1f);
                        Movement += Vector2.UnitX * (MaxSpeed * 7.5f);
                        groundJump = true;
                        return;
                    }
                }
                else if (actions.Contains(Action.MoveUp) && (actions.Contains(Action.MoveRight) && rightWall && !groundJump))
                {
                    if (!oldInput.Contains(Action.MoveUp)) {
                        Movement = -Vector2.UnitY * (JumpingHeight * 1.1f);
                        Movement -= Vector2.UnitX * (MaxSpeed * 7.5f);
                        groundJump = true;
                        return;
                    }
                }
                else if (!IsOnGround())
                {
                    if (actions.Contains(Action.MoveLeft))
                    { Movement -= Vector2.UnitX * (MaxSpeed * 1.1f); }
                    if (actions.Contains(Action.MoveRight))
                    { Movement += Vector2.UnitX * (MaxSpeed * 1.1f); }
                }
            }
            else if (!IsOnGround())
            {
                if (actions.Contains(Action.MoveLeft))
                    { Movement -= Vector2.UnitX * (MaxSpeed * 1.1f); }
                if (actions.Contains(Action.MoveRight))
                    { Movement += Vector2.UnitX * (MaxSpeed * 1.1f); }
            }

            if (IsOnGround() && (actions.Contains(Action.MoveUp)))
            {
                groundJump = true;
                Movement -= Vector2.UnitY * JumpingHeight;
            }

            if (IsOnGround() && actions.Contains(Action.MoveLeft))
                { Movement -= Vector2.UnitX * MaxSpeed; }
            if (IsOnGround() && actions.Contains(Action.MoveRight))
                { Movement += Vector2.UnitX * MaxSpeed; }

            oldInput = actions;

        }

        protected override void SimulateFriction()
        {
            if (IsOnGround())
            {
                KeyboardState kbState = Keyboard.GetState();
                if (!kbState.IsKeyDown(Keys.Space) && !kbState.IsKeyDown(Keys.Up))
                {
                    Movement -= Movement * Vector2.One * .08f;
                }
            }
            else { Movement -= Movement * Vector2.One * .01f; }
        }

    }
}