using System;
using System.Collections.Generic;
using System.Drawing;

namespace YinYang.CodeProject.Projects.SimplePathfinding.Scenarios.Geometric
{
    public class RandomMarkerScenario : RandomRectangleScenario
    {
        #region | Properties |

        /// <summary>
        /// See <see cref="BasePathScenario.UseCache"/> for more details.
        /// </summary>
        protected override Boolean UseCache
        {
            get { return true; }
        }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomMarkerScenario" /> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public RandomMarkerScenario(Int32 width, Int32 height) : base(width, height) { }

        #endregion

        #region << RandomGeometryScenario >>

        /// <summary>
        /// See <see cref="BaseGeometryScenario{TGeometry}.OnConstructLayout"/> for more details.
        /// </summary>
        protected override void OnConstructLayout(out IEnumerable<Rectangle> geometry)
        {
            List<Rectangle> result = new List<Rectangle>();
            Int32 shift = AreHollowAreasMinimized ? 0 : 1;

            for (Int32 index = 0; index < 5000; index++)
            {
                Int32 x = Random.Next(Width - 100) + 50;
                Int32 y = Random.Next(Height - 100) + 50;

                Rectangle rectangle = new Rectangle(x - 2, y - 2, 5 - shift, 5 - shift);
                result.Add(rectangle);
            }

            geometry = result;
        }

        /// <summary>
        /// See <see cref="BaseGeometryScenario{TGeometry}.OnReconstructGeometry"/> for more details.
        /// </summary>
        protected override void OnReconstructGeometry(Rectangle rectangle)
        {
            AddRectangleToCache(rectangle);
        }

        /// <summary>
        /// We have faster variant, because we've cached the map.
        /// </summary>
        protected override Boolean OnIsBlocked(Int32 x, Int32 y)
        {
            return GetCacheBit(x, y);
        }

        #endregion
    }
}
