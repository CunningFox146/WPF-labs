using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace lab6_7.Models
{
    enum ItemCategory
    {
        FamilyFrendly,
        Party,
        Cards,
    }

    enum SearchType
    {
        Name,
        Count,
        Price
    }

    class Item
    {
        public string Name { get; set; }
        public string SmallDescription { get; set; }
        public string Description { get; set; }
        private float rating;
        public float Rating { get => rating; set => rating = Math.Clamp(value, 0, 10); }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public BitmapImage Image { get; set; }
        public string ImagePath { get; set; }

        public bool IsValid() => !String.IsNullOrEmpty(Name) &&
            !String.IsNullOrEmpty(SmallDescription) && !String.IsNullOrEmpty(Description) &&
            (Rating >= 0 && Rating <= 10) && Price > 0 &&
            Quantity > 0;
    }
}
