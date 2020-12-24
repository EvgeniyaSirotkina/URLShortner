using System.Text;
using Microsoft.EntityFrameworkCore;

namespace URLShortner.Data.Helpers
{
    public static class DbExceptionMessageBuilder
    {
        public static string DbUpdateConcurrencyExceptionMessageBuilder(this DbUpdateConcurrencyException exception)
        {
            var builder = new StringBuilder("A DbUpdateConcurrencyException was caught while saving changes. ");

            foreach (var item in exception.Entries)
            {
                builder.AppendFormat("Type: {0} was part of the problem. ", item.Entity.GetType().Name);
            }

            return builder.ToString();
        }
        public static string DbUpdateExceptionMessageBuilder(this DbUpdateException exception)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

            foreach (var item in exception.Entries)
            {
                builder.AppendFormat("Type: {0} was part of the problem. ", item.Entity.GetType().Name);
            }

            return builder.ToString();
        }
    }
}
