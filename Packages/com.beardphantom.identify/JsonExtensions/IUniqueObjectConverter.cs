#if JSON_EXTENSIONS_ENABLED
using BeardPhantom.Identify;
using Newtonsoft.Json;
using System;

namespace JsonExtensions
{
    public class UniqueObjectConverter : JsonConverter<IUniqueObject>
    {
        #region Fields

        private readonly IUniqueObjectDataStore _uniqueObjectDataStore;

        #endregion

        #region Constructors

        public UniqueObjectConverter(IUniqueObjectDataStore uniqueObjectDataStore)
        {
            _uniqueObjectDataStore = uniqueObjectDataStore;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, IUniqueObject value, JsonSerializer serializer)
        {
            writer.WriteValue(value.IdentifierString);
        }

        /// <inheritdoc />
        public override IUniqueObject ReadJson(
            JsonReader reader,
            Type objectType,
            IUniqueObject existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            var identifier = (string)reader.Value;
            return _uniqueObjectDataStore.FindUniqueObject(identifier);
        }

        #endregion
    }
}
#endif