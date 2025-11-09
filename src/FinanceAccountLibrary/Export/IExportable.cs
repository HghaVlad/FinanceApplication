namespace FinanceAccountLibrary.Export
{
    public interface IExportable
    {
        void Accept(IExportVisitor visitor);
    }
}