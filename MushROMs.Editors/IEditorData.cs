
namespace MushROMs.Editors
{
    /// <summary>
    /// Provides an interface to expose <see cref="Editor"/> data and
    /// its <see cref="Selection"/>.
    /// </summary>
    public interface IEditorData
    {
        #region Properties
        /// <summary>
        /// Gets the <see cref="Selection"/> of the <see cref="IEditorData"/>.
        /// </summary>
        ISelection Selection { get; }
        #endregion
    }
}
