using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace YinYang.CodeProject.Projects.SimplePathfinding.Scenarios.Geometric
{
    public abstract class BaseGeometryScenario<TGeometry> : BasePathScenario
    {
        #region | Fields |

        private readonly List<TGeometry> layout;

        protected readonly Random Random;

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomRectangleScenario" /> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        protected BaseGeometryScenario(Int32 width, Int32 height) : base(width, height)
        {
            Random = new Random();
            layout = new List<TGeometry>();
        }

        #endregion

        #region | Abstract methods |

        /// <summary>
        /// Build the actual scenario out of numerous geometry <see cref="TGeometry"/> shapes.
        /// </summary>
        /// <param name="geometry">The list of geometric definitions.</param>
        protected abstract void OnConstructLayout(out IEnumerable<TGeometry> geometry);

        /// <summary>
        /// Rebuild only a given geometric shape of <see cref="TGeometry"/>.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        protected virtual void OnReconstructGeometry(TGeometry geometry) { }

        /// <summary>
        /// Called when geometric element is about to be drawn to a graphics (presumably a bitmap).
        /// </summary>
        /// <param name="graphics">The target graphics.</param>
        /// <param name="geometry">The geometry to be drawn.</param>
        protected abstract void OnDrawGeometry(Graphics graphics, TGeometry geometry);

        /// <summary>
        /// Determines whether the geometry can even block can geometry block].
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="geometry">The instance of geomtric shape.</param>
        /// <returns></returns>
        protected abstract Boolean OnCanGeometryBlock(Int32 x, Int32 y, TGeometry geometry);

        /// <summary>
        /// Investigates whether the obstacles is actually present on the given point.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="geometry">The instance of geomtric shape.</param>
        /// <returns></returns>
        protected abstract Boolean OnIsGeometryBlocking(Int32 x, Int32 y, TGeometry geometry);

        #endregion

        #region << PathScenario >>

        /// <summary>
        /// See <see cref="BasePathScenario.OnBuild"/> for more details.
        /// </summary>
        protected override void OnBuild(Boolean generateNew)
        {
            if (generateNew)
            {
                layout.Clear();

                IEnumerable<TGeometry> geometry;
                OnConstructLayout(out geometry);
                layout.AddRange(geometry);
            }

            foreach (TGeometry geometry in layout)
            {
                OnReconstructGeometry(geometry);
            }
        }

        /// <summary>
        /// See <see cref="BasePathScenario.OnDraw"/> for more details.
        /// </summary>
        protected override void OnDraw(Graphics graphics)
        {
            foreach (TGeometry geometry in layout)
            {
                OnDrawGeometry(graphics, geometry);
            }
        }

        /// <summary>
        /// See <see cref="BasePathScenario.OnIsBlocked"/> for more details.
        /// </summary>
        protected override Boolean OnIsBlocked(Int32 x, Int32 y)
        {
            // determines whether any geometric entity that can block (OnCanGeomtryBlock), is actually blocking at this point (OnIsGeometryBlock)
            return layout.
                Where(geometry => OnCanGeometryBlock(x, y, geometry)).
                Any(geometry => OnIsGeometryBlocking(x, y, geometry));
        }

        #endregion
    }
}
