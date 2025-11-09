using FinanceAccountLibrary.models;
using System.Text;

namespace FinanceAccountLibrary.Export
{
    public abstract class ExporterBase : IExportVisitor
    {
        protected StringBuilder Content = new StringBuilder();
        protected bool HeaderWritten;

        public virtual string GetContent()
        {
            return Content.ToString();
        }

        public virtual void Clear()
        {
            Content.Clear();
            HeaderWritten = false;
        }


        protected abstract void WriteHeader(string header);
        public abstract void Visit(BankAccount bankAccount);
        public abstract void Visit(Category category);
        public abstract void Visit(Operation operation);
    }
}