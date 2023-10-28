#if JSON_EXTENSIONS_ENABLED
using Newtonsoft.Json;
using System;
using UnityEngine.Assertions;

namespace BeardPhantom.Identify.Json
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
            writer.WriteValue(value.Identifier);
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
            var didFind = _uniqueObjectDataStore.TryFindUniqueObject(identifier, out var result);
            Assert.IsTrue(didFind, "didFind");
            return result;
        }

        #endregion
    }
}
#endif