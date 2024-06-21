namespace FirmaKurierska.WebAPI.Utils
{
    public static class PropertyUpdater
    {
        public static void UpdateProperties<TTarget, TSource>(TTarget target, TSource source)
        {
            var targetProperties = typeof(TTarget).GetProperties();
            var sourceProperties = typeof(TSource).GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var targetProperty = targetProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);
                if (targetProperty != null && sourceProperty.GetValue(source) != null)
                {
                    targetProperty.SetValue(target, sourceProperty.GetValue(source));
                }
            }
        }
    }
}
