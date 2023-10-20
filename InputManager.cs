namespace Cosmic {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public static class InputManager {
        public static KeyboardState keyboardState;
        public static KeyboardState? keyboardStatePrevious;

        public static MouseState mouseState;
        public static MouseState? mouseStatePrevious;

        public static void Update() {
            keyboardStatePrevious = keyboardState;
            keyboardState = Keyboard.GetState();

            mouseStatePrevious = mouseState;
            mouseState = Mouse.GetState();
        }

        public static bool GetKeyPressed(Keys key) {
            return keyboardState.IsKeyDown(key) && !(keyboardStatePrevious?.IsKeyDown(key) ?? false);
        }

        public static bool GetKeyReleased(Keys key) {
            return keyboardState.IsKeyUp(key) && !(keyboardStatePrevious?.IsKeyUp(key) ?? false);
        }

        public static bool GetKeyHeld(Keys key) {
            return keyboardState.IsKeyDown(key);
        }

        public static bool GetMouseLeftPressed() {
            return mouseState.LeftButton == ButtonState.Pressed && mouseStatePrevious?.LeftButton != ButtonState.Pressed;
        }

        public static bool GetMouseMiddlePressed() {
            return mouseState.MiddleButton == ButtonState.Pressed && mouseStatePrevious?.MiddleButton != ButtonState.Pressed;
        }

        public static bool GetMouseRightPressed() {
            return mouseState.RightButton == ButtonState.Pressed && mouseStatePrevious?.RightButton != ButtonState.Pressed;
        }

        public static bool GetMouseLeftReleased() {
            return mouseState.LeftButton == ButtonState.Released && mouseStatePrevious?.LeftButton != ButtonState.Released;
        }

        public static bool GetMouseMiddleReleased() {
            return mouseState.MiddleButton == ButtonState.Released && mouseStatePrevious?.MiddleButton != ButtonState.Released;
        }

        public static bool GetMouseRightReleased() {
            return mouseState.RightButton == ButtonState.Released && mouseStatePrevious?.RightButton != ButtonState.Released;
        }

        public static bool GetMouseLeftHeld() {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool GetMouseMiddleHeld() {
            return mouseState.MiddleButton == ButtonState.Pressed;
        }

        public static bool GetMouseRightHeld() {
            return mouseState.RightButton == ButtonState.Pressed;
        }

        public static bool GetMouseScrollUp() {
            return mouseState.ScrollWheelValue > mouseStatePrevious?.ScrollWheelValue;
        }

        public static bool GetMouseScrollDown() {
            return mouseState.ScrollWheelValue < mouseStatePrevious?.ScrollWheelValue;
        }

        public static Vector2 GetMousePosition() {
            return mouseState.Position.ToVector2() + Camera.position;
        }

        public static Vector2 GetMouseWorldPosition() {
            return (mouseState.Position.ToVector2() / 2f) + Camera.position;
        }
    }
}