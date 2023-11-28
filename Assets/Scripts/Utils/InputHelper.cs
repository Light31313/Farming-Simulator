public class InputHelper
{
    private static InputMaster _input;

    public static InputMaster Input
    {
        get
        {
            if (_input != null) return _input;
            _input = new InputMaster();
            _input.Enable();
            return _input;
        }
    }
}