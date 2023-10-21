namespace Cosmic {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public static class InputManager {
        public static KeyboardState KeyboardState { get; private set; }
        public static KeyboardState? KeyboardStatePrevious { get; private set; }

        public static MouseState MouseState { get; private set; }
        public static MouseState? MouseStatePrevious { get; private set; }

        public static void Update() {
            KeyboardStatePrevious = KeyboardState;
            KeyboardState = Keyboard.GetState();

            MouseStatePrevious = MouseState;
            MouseState = Mouse.GetState();
        }

        public static bool GetKeyPressed(Keys key) {
            return KeyboardState.IsKeyDown(key) && !(KeyboardStatePrevious?.IsKeyDown(key) ?? false);
        }

        public static bool GetKeyReleased(Keys key) {
            return KeyboardState.IsKeyUp(key) && !(KeyboardStatePrevious?.IsKeyUp(key) ?? false);
        }

        public static bool GetKeyHeld(Keys key) {
            return KeyboardState.IsKeyDown(key);
        }

        public static bool GetMouseLeftPressed() {
            return MouseState.LeftButton == ButtonState.Pressed && MouseStatePrevious?.LeftButton != ButtonState.Pressed;
        }

        public static bool GetMouseMiddlePressed() {
            return MouseState.MiddleButton == ButtonState.Pressed && MouseStatePrevious?.MiddleButton != ButtonState.Pressed;
        }

        public static bool GetMouseRightPressed() {
            return MouseState.RightButton == ButtonState.Pressed && MouseStatePrevious?.RightButton != ButtonState.Pressed;
        }

        public static bool GetMouseLeftReleased() {
            return MouseState.LeftButton == ButtonState.Released && MouseStatePrevious?.LeftButton != ButtonState.Released;
        }

        public static bool GetMouseMiddleReleased() {
            return MouseState.MiddleButton == ButtonState.Released && MouseStatePrevious?.MiddleButton != ButtonState.Released;
        }

        public static bool GetMouseRightReleased() {
            return MouseState.RightButton == ButtonState.Released && MouseStatePrevious?.RightButton != ButtonState.Released;
        }

        public static bool GetMouseLeftHeld() {
            return MouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool GetMouseMiddleHeld() {
            return MouseState.MiddleButton == ButtonState.Pressed;
        }

        public static bool GetMouseRightHeld() {
            return MouseState.RightButton == ButtonState.Pressed;
        }

        public static bool GetMouseScrollUp() {
            return MouseState.ScrollWheelValue > MouseStatePrevious?.ScrollWheelValue;
        }

        public static bool GetMouseScrollDown() {
            return MouseState.ScrollWheelValue < MouseStatePrevious?.ScrollWheelValue;
        }

        public static Vector2 GetMousePosition() {
            return MouseState.Position.ToVector2() + Camera.Position;
        }

        public static Vector2 GetMouseWorldPosition() {
            return (MouseState.Position.ToVector2() / 2f) + Camera.Position;
        }
    }
}