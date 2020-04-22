using System;
using System.Collections.Generic;
using System.Drawing;

namespace YinYang.CodeProject.Projects.SimplePathfinding.Scenarios.Geometric
{
    public class BlackObeliskScenario : RandomRectangleScenario
    {
        #region | Fields |

        /// <summary>
        /// All bow to The Black Obelist!
        /// </summary>
        private readonly Rectangle blackObelisk;

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackObeliskScenario" /> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public BlackObeliskScenario(Int32 width, Int32 height) : base(width, height)
        {
            Int32 width4 = Width >> 2;
            blackObelisk = new Rectangle(width4 + (Width >> 3), Height >> 2, width4, Height >> 1);
        }

        #endregion

        #region << BaseGeometryScenario >>

        /// <summary>
        /// See <see cref="BaseGeometryScenario{TGeometry}.OnConstructLayout"/> for more details.
        /// </summary>
        protected override void OnConstructLayout(out IEnumerable<Rectangle> geometry)
        {
            geometry = new[] { blackObelisk };
        }

        #endregion
    }
}
