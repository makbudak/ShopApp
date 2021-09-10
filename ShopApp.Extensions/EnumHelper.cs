using ShopApp.Model.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShopApp.Extensions
{
    public static class EnumHelper
    {
        public static List<LookupModel> GetEnumValuesAndDescriptions<T>()
        {
            Type enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException();

            List<LookupModel> enumValList = new List<LookupModel>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                var fi = e.GetType().GetField(e.ToString());
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                enumValList.Add(new LookupModel()
                {
                    Id = (int)e,
                    Name = (attributes.Length > 0) ? attributes[0].Description : e.ToString()
                });
            }

            return enumValList;
        }
    }
}
