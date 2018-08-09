namespace Office.App.Contracts
{
    public interface ICommandInterpreter
    {
        string Read(string[] commandArgs);
    }
}