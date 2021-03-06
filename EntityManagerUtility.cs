using System.Collections.Generic;
using Unity.Entities;

namespace E7.EcsGadgets
{
    public class EntityManagerUtility
    {
        EntityManager em;
        List<int> scdIndices;

        public EntityManagerUtility(World world)
        {
            this.em = world.EntityManager;
            scdIndices = new List<int>();
        }
        /// <summary>
        /// Performs a linear search over all existing SCD of a single type to find out the index.
        /// The search equality test is by List.IndexOf
        /// Remember that the index may become unusable once the SCD order version changed.
        /// </summary>
        /// <param name="scdValues">In order for this method to not generate garbage, please bring your own
        /// managed List of the type you want that you allocate on OnCreate in your system. Just bring it in,
        /// this method has a .Clear inside.</param>
        /// <returns>-1 when not found. That means this SCD value has not been used anywhere yet or not used anymore.</returns>
        public int IndexOfSharedComponentData<T>(T sharedComponentValue, List<T> scdValues)
            where T : struct, ISharedComponentData
        {
            scdValues.Clear();
            scdIndices.Clear();
            em.GetAllUniqueSharedComponentData(scdValues, scdIndices);
            var indexOf = scdValues.IndexOf(sharedComponentValue);
            return indexOf == -1 ? -1 : scdIndices[indexOf];
        }
    }
}