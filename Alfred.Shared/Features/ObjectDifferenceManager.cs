namespace Alfred.Shared.Features
{
    public class ObjectDifferenceManager
    {

        public T UpdateObject<T>(T original, T updated) where T : class
        {
            var diffs = ObjectDiffPatch.GenerateDiff(original, updated);
            return ObjectDiffPatch.PatchObject(original, diffs.NewValues);
        }
    }
}
