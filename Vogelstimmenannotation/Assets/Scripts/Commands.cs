namespace Commands
{
    public enum Menu
    {
        Confirm,
        Cancel,
        Up,
        Down
    }

    //TODO: Allow players to only use one command, or use flags and build a priority list for commands?
    //going for one command only for now
    public enum Player
    {
        OpenMenu,
        OpenBirDex,
        StartMinigame,
        Interact
    }
}