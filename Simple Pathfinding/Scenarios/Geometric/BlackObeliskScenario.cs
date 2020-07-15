using System;
using System.Collections.Generic;
using System.Drawing;

namespace SimplePathfinding.Scenarios.Geometric
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
        public BlackObeliskScenario(int width, int height) : base(width, height)
        {
            int width4 = Width >> 2;
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
