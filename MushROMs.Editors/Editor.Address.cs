/* Deals with index to address conversions of the editor.
 * This logic must exist when dealing with editor data
 * that comes from game data at unspecified locations.
 * The default logic is to treat the address and the index
 * as the same value, but in derived classes, this is
 * almost never the case.
 * */

namespace MushROMs.Editors
{
    partial class Editor
    {
        #region Methods
        /// <summary>
        /// Gets the absolute byte address given the index of an element.
        /// </summary>
        /// <param name="index">
        /// The index of the element to get the address of.
        /// </param>
        /// <param name="args">
        /// When overridden in a derived class, provides any further arguments
        /// needed for making the conversion.
        /// </param>
        /// <returns>
        /// The byte address of the element.
        /// </returns>
        public virtual int GetAddressFromIndex(int index, object[] args)
        {
            return index;
        }

        /// <summary>
        /// Gets the index of an element given its abolute byte address.
        /// </summary>
        /// <param name="address">
        /// The byte address of the element to get the index of.
        /// </param>
        /// <param name="args">
        /// When overridden in a derived class, provides any further arguments
        /// needed for making the conversion.
        /// </param>
        /// <returns>
        /// The index of the element.
        /// </returns>
        public virtual int GetIndexFromAddress(int address, object[] args)
        {
            return address;
        }
        #endregion
    }
}