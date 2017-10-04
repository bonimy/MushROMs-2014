/* Allows abstract controls to be visible in the Visual
 * Studio designer. All code credit from
 * http://stackoverflow.com/questions/1620847/how-can-i-get-visual-studio-2008-windows-forms-designer-to-render-a-form-that-im/17661276#17661276
 * */

using System;
using System.ComponentModel;
using System.Reflection;

namespace MushROMs.Controls
{
    /// <summary>
    /// Provides supplemental metadata to the <see cref="TypeDescriptor"/> for components
    /// that are abstract that derive directly from components using the
    /// <see cref="TypeDescriptionProvider"/>.
    /// </summary>
    /// <typeparam name="TAbstract">
    /// The abstract <see cref="Type"/>.
    /// </typeparam>
    /// <typeparam name="TBase">
    /// The base <see cref="Type"/>.
    /// </typeparam>
    public class AbstractControlDescriptionProvider<TAbstract, TBase> : TypeDescriptionProvider
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AbstractControlDescriptionProvider&lt;TAbstract, TBase&gt;"/> class.
        /// </summary>
        public AbstractControlDescriptionProvider()
            : base(TypeDescriptor.GetProvider(typeof(TAbstract)))
        {
        }

        /// <summary>
        /// Performs normal reflection against the given object with the given type.
        /// </summary>
        /// <param name="objectType">
        /// The type of object for which to retrieve the <see cref="IReflect"/>.
        /// </param>
        /// <param name="instance">
        /// An instance of the type. Can be null.
        /// </param>
        /// <returns>
        /// A <see cref="Type"/>.
        /// </returns>
        public override Type GetReflectionType(Type objectType, object instance)
        {
            // Get the base type instead of the abstract type
            if (objectType == typeof(TAbstract))
                return typeof(TBase);

            return base.GetReflectionType(objectType, instance);
        }

        /// <summary>
        /// Creates an object that can substitute for another data type.
        /// </summary>
        /// <param name="provider">
        /// An optional service provider.
        /// </param>
        /// <param name="objectType">
        /// The type of object to create. This parameter is never null.
        /// </param>
        /// <param name="argTypes">
        /// An optional array of types that represent the parameter types to be passed
        /// to the object's constructor. This array can be null or of zero length.
        /// </param>
        /// <param name="args">
        /// An optional array of parameter values to pass to the object's constructor.
        /// </param>
        /// <returns>
        /// The substitute <see cref="Object"/>.
        /// </returns>
        public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
        {
            // Get the base type instead of the abstract type
            if (objectType == typeof(TAbstract))
                objectType = typeof(TBase);

            return base.CreateInstance(provider, objectType, argTypes, args);
        }
    }
}
