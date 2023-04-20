namespace RPG.Control
{
    public interface IRaycastable
    {
        CursorType GetCursorType();
        bool HandelRaycast(PlayerController callingController);
    }
}