using System.Collections.Generic;
using System.Linq;
using testing_final.Logic.Utilities;


namespace testing_final.Logic.Models
{
    public class ShoppingCart
    {
        public List<Item> Items { get; private set; } = new();

        public event Action? CartUpdated;

        public void NotifyCartUpdated()
        {
            CartUpdated?.Invoke();
        }


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

        public decimal CalculateCartSubtotal()
        {
            return Items.Sum(item => item.Price * item.Quantity);
        }

        public decimal CalculateTax(decimal taxRate = 0.07m)
        {
            return TaxCalculator.CalculateTax(CalculateCartSubtotal(), taxRate);
        }

        public decimal CalculateCartTotal(decimal taxRate = 0.07m)
        {
            return CalculateCartSubtotal() + CalculateTax(taxRate);
        }

        public void ClearCart()
        {
            Items.Clear();
        }
    }
}

