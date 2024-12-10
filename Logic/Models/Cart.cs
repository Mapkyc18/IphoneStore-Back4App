using System.Collections.Generic;
using System.Linq;

namespace testing_final.Logic.Models
{
    public class Cart
    {
        public List<Item> Items { get; set; } = new();

        private const decimal SalesTaxRate = 0.07m;

        public void AddItem(Item product, int quantity = 1)
        {
            var existingItem = Items.FirstOrDefault(i => i.Name == product.Name);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new Item
                {
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity
                });
            }
        }

        public void RemoveItem(string productName)
        {
            var item = Items.FirstOrDefault(i => i.Name == productName);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public void ClearCart()
        {
            Items.Clear();
        }

        public decimal CalculateCartSubtotal()
        {
            return Items.Sum(item => item.Price * item.Quantity);
        }

        public decimal CalculateTax()
        {
            return CalculateCartSubtotal() * SalesTaxRate;
        }

        public decimal CalculateCartTotal()
        {
            return CalculateCartSubtotal() + CalculateTax();
        }

        public string GetCartSummary()
        {
            var summary = string.Join("\n", Items.Select(item => $"{item.Quantity} x {item.Name} @ {item.Price:C} each = {item.Price * item.Quantity:C}"));
            var subtotal = CalculateCartSubtotal();
            var tax = CalculateTax();
            var total = CalculateCartTotal();

            return $"{summary}\n\nSubtotal: {subtotal:C}\nSales Tax (7%): {tax:C}\nTotal: {total:C}";
        }
    }
}
