using System;
using System.Collections.Generic;

namespace WebAPI.Helpers
{
    /// <summary>
    /// Helper function to copy object's values from one to another. If the property name matches, it will copy the values over
    /// </summary>
    /// <typeparam name="TParent">Parent object from which we are going to copy the values</typeparam>
    /// <typeparam name="TChild">Child object with new instance. Destination object</typeparam>
    public class PropertyCopier<TParent, TChild> where TParent : class where TChild : class
    {
        public static List<TChild> Copy(List<TParent> parentList, TChild child)
        {
            // We keep a clonable copy of the initial child, since we need to reinitialize the child in each loop below
            var childCopy = (ICloneable)child;

            var childList = new List<TChild>();

            foreach (var parent in parentList)
            {
                CopyObjects(parent, child);

                childList.Add(child);

                child = (TChild)childCopy.Clone();
            }

            return childList;
        }

        /// <summary>
        /// Method to copy values from Source to Destination objects
        /// </summary>
        /// <param name="parent">Source objects with values</param>
        /// <param name="child">Destination objects with values</param>
        private static void CopyObjects(TParent parent, TChild child)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();
            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name &&
                        parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        childProperty.SetValue(child, parentProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }
    }
}