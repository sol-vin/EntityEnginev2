﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

//This part of the code was taken from the Eye of the Dragon tutorials located here:
//http://xnagpa.net/xna4rpg.php
//The reason I copied this code was because I didn't want to reinvent the wheel.
//If you are Jamie McMahon and want me to take down this code, I will, shoot me an email @ redcodefinal@gmail.com

namespace EntityEnginev2.Input
{
    public sealed class InputHandler : GameComponent
    {
        internal static GameTime Gametime;

        #region Keyboard Field Region

        private static KeyboardState _keyboardState;
        private static KeyboardState _lastKeyboardState;

        #endregion Keyboard Field Region

        #region Game Pad Field Region

        private static GamePadState[] _gamePadStates;
        private static GamePadState[] _lastGamePadStates;

        #endregion Game Pad Field Region

        #region Keyboard Property Region

        public static KeyboardState KeyboardState
        {
            get { return _keyboardState; }
        }

        public static KeyboardState LastKeyboardState
        {
            get { return _lastKeyboardState; }
        }

        #endregion Keyboard Property Region

        #region Game Pad Property Region

        public static GamePadState[] GamePadStates
        {
            get { return _gamePadStates; }
        }

        public static GamePadState[] LastGamePadStates
        {
            get { return _lastGamePadStates; }
        }

        #endregion Game Pad Property Region

        #region Constructor Region

        public InputHandler(Game game)
            : base(game)
        {
            _keyboardState = Keyboard.GetState();
            _gamePadStates = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];

            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
                _gamePadStates[(int)index] = GamePad.GetState(index);
        }

        #endregion Constructor Region

        #region XNA methods

        public override void Update(GameTime gameTime)
        {
            Gametime = gameTime;
            _lastKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();

            _lastGamePadStates = (GamePadState[])_gamePadStates.Clone();
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
                _gamePadStates[(int)index] = GamePad.GetState(index);

            base.Update(gameTime);
        }

        #endregion XNA methods

        #region General Method Region

        public static void Flush()
        {
            _lastKeyboardState = _keyboardState;
            _lastGamePadStates = _gamePadStates;
        }

        #endregion General Method Region

        #region Keyboard Region
        public static bool KeyUp(Keys key)
        {
            return _keyboardState.IsKeyUp(key);
        }
        public static bool KeyReleased(Keys key)
        {
            return _keyboardState.IsKeyUp(key) &&
                _lastKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return _keyboardState.IsKeyDown(key) &&
                _lastKeyboardState.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return _keyboardState.IsKeyDown(key);
        }

        #endregion Keyboard Region

        #region Game Pad Region
        public static bool ButtonUp(Buttons button, PlayerIndex index)
        {
            return _gamePadStates[(int) index].IsButtonUp(button);
        }

        public static bool ButtonReleased(Buttons button, PlayerIndex index)
        {
            return _gamePadStates[(int)index].IsButtonUp(button) &&
                _lastGamePadStates[(int)index].IsButtonDown(button);
        }

        public static bool ButtonPressed(Buttons button, PlayerIndex index)
        {
            return _gamePadStates[(int)index].IsButtonDown(button) &&
                _lastGamePadStates[(int)index].IsButtonUp(button);
        }

        public static bool ButtonDown(Buttons button, PlayerIndex index)
        {
            return _gamePadStates[(int)index].IsButtonDown(button);
        }

        #endregion Game Pad Region
    }
}