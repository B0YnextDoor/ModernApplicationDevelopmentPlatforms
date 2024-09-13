using WEB_253502_KRASYOV.Domain.Entities;

namespace WEB_253502_KRASYOV.Domain.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; } = new();
        public virtual void AddToCart(Device constructor)
        {
            if (Items.ContainsKey(constructor.Id))
                ++Items[constructor.Id].Amount;
            else
                Items.Add(constructor.Id, new CartItem { Item = constructor, Amount = 1 });
        }
        public virtual void RemoveItems(int id)
        {
            if (Items.ContainsKey(id) && --Items[id].Amount <= 0)
            {
                Items.Remove(id);
            }
        }
        public virtual void ClearAll()
        {
            Items.Clear();
        }
        public int Count { get => Items.Sum(item => item.Value.Amount); }
        public decimal CountPrice { get => Items.Sum(item => item.Value.Item.Price * item.Value.Amount); }
    }
}
