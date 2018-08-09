namespace Office.App.Contracts
{
    public interface ICommand
    {
        string Execute(string[] data);
    }
}
