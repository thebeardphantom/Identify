namespace BeardPhantom.Identify
{
    public static class UniqueObjectDataStoreUtility
    {
        #region Methods

        public static bool TryFindUniqueObject<T>(
            this IUniqueObjectDataStore uniqueObjectDataStore,
            string identifier,
            out T result) where T : IUniqueObject
        {
            if (uniqueObjectDataStore.TryFindUniqueObject(identifier, out var resultUntyped) && resultUntyped is T typedResult)
            {
                result = typedResult;
                return true;
            }

            result = default;
            return false;
        }

        public static T FindUniqueObject<T>(this IUniqueObjectDataStore uniqueObjectDataStore, string identifier)
            where T : IUniqueObject
        {
            return uniqueObjectDataStore.TryFindUniqueObject<T>(identifier, out var result) ? result : default;
        }

        public static IUniqueObject FindUniqueObject(this IUniqueObjectDataStore uniqueObjectDataStore, string identifier)
        {
            return uniqueObjectDataStore.TryFindUniqueObject(identifier, out var result) ? result : default;
        }

        #endregion
    }
}