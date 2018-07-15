using System;
using System.Linq;
using System.Text;
using FastFood.Data;

namespace FastFood.DataProcessor
{
    public static class Bonus
    {
        private const string successMessage = "{0} Price updated from ${1:f2} to ${2:f2}";
        private const string failedMessage = "Item {0} not found!";
        
        public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
	    {
	        var sb = new StringBuilder();

	        var item = context.Items.FirstOrDefault(p => p.Name == itemName);
	        if (item == null)
	        {
                return String.Format(failedMessage, itemName);
	        }

	        var oldPrice = item.Price;

	        item.Price = newPrice;
	        context.SaveChanges();

	        sb.AppendLine(String.Format(successMessage, itemName, oldPrice, newPrice));

	        return sb.ToString().Trim();
	    }
    }
}
