using System;
using System.ComponentModel;
using System.Reflection;

namespace Enemies
{
    public enum EnemyType
    {
        [Description("Movable")]
        Movable = 1,
        [Description("Stalking")]
        Stalking = 2,
        [Description("Shooting")]
        Shooting = 3
    }

    public static class EnemyUtility
    {
        public static T GetEnumValueFromDescription<T>(string description)
        {
            MemberInfo[] fis = typeof(T).GetFields();

            foreach (var fi in fis) {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0 && attributes[0].Description == description)
                    return (T)Enum.Parse(typeof(T), fi.Name);
            }

            throw new Exception("Not found");
        }
    }
}
