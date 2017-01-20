namespace Structures
{
    [System.Flags]
    public enum States
    {
        None = 0,
        Saved = 1,
        Changed = 2,
        Searching = 4
    }
}
