using System;
using System.Linq;
using ZFood.Core.API;
using ZFood.Web.DTO;

namespace ZFood.Web.Extensions
{
    public static class PageConversionExtensions
    {
        public static PageDTO<K> ToDTO<T, K>(this Page<T> page, Func<T, K> conversionFunction)
        {
            return new PageDTO<K>
            {
                Items = page.Items.Select(i => conversionFunction(i)),
                HasMore = page.HasMore,
                PagesQuantity = page.ItemsQuantity
            };
        }
    }
}
