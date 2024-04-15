using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System.Collections;

namespace EfCoreOpenJson.Models
{
    public static class ModelBuilderExtensions
    {
        private static PropertyBuilder<T> AddJsonConverter<T>(this PropertyBuilder<T> bldr,
            ValueConverter<T, string> converter)
        {
            var comp = new ValueComparer<T>(
                (l, r) => IsEqual(l, r),
                v => FindHashCode(v),
                v => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(v)));

            bldr.HasConversion(converter, comp);
            return bldr;
        }

        /// <summary>
        /// Marks a field as being stored as JSON in the database, using the default value for
        /// the field type if it's stored as null
        /// </summary>
        public static PropertyBuilder<T> IsJson<T>(
            this PropertyBuilder<T> bldr,
            JsonSerializerSettings settings = null
            )
        {
            var conv = new ValueConverter<T, string>(
                v => JsonConvert.SerializeObject(v, settings),
                v => v == null ? default : JsonConvert.DeserializeObject<T>(v, settings)
            );

            return AddJsonConverter(bldr, conv);
        }

        private static int FindHashCode<T>(T val)
        {
            if (val is IEnumerable list)
            {
                // https://stackoverflow.com/a/8094931/10260374
                unchecked
                {
                    int hash = 19;
                    foreach (var foo in list)
                    {
                        hash = hash * 31 + foo.GetHashCode();
                    }
                    return hash;
                }
            }

            return val.GetHashCode();
        }

        private static bool IsEqual<T>(T l, T r)
        {
            if (l == null || r == null)
            {
                return ReferenceEquals(l, r);
            }

            var defEq = Equals(l, r);

            if (defEq)
            {
                return true;
            }

            if (l is ICollection lCol && r is ICollection rCol)
            {
                if (lCol.Count != rCol.Count) return false;
                // No need to check rCol count because we know counts are the same here
                if (lCol.Count == 0) return true;

                // Otherwise if collections are non-empty with same number of items we need to serialize them to confirm
            }

            var lJson = JsonConvert.SerializeObject(l);
            var rJson = JsonConvert.SerializeObject(r);
            return lJson == rJson;
        }
    }
}
