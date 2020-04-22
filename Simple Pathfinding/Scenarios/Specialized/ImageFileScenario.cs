using System;
using System.Drawing;
using YinYang.CodeProject.Projects.SimplePathfinding.Properties;

namespace YinYang.CodeProject.Projects.SimplePathfinding.Scenarios.Specialized
{
    public class ImageFileScenario : BasePathScenario
    {
        #region | Fields |

        private readonly Bitmap cachedBitmap;

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFileScenario" /> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public ImageFileScenario(Int32 width, Int32 height) : base(width, height)
        {
            cachedBitmap = new Bitmap(Resources.Maze);
        }

        #endregion

        #region << BasePathScenario >>

        /// <summary>
        /// See <see cref="BasePathScenario.OnIsBlocked"/> for more details.
        /// </summary>
        protected override Boolean OnIsBlocked(Int32 x, Int32 y)
        {
            return cachedBitmap.GetPixel(x, y).ToArgb() == BlockColorValue;
        }

        /// <summary>
        /// See <see cref="BasePathScenario.OnDraw"/> for more details.
        /// </summary>
        protected override Bitmap OnCreateDefaultImage(Boolean generateNew, Boolean updateDistanceMap, Boolean showDistanceMap)
        {
            Bitmap result = Resources.Maze;

            // creates distance map, or at least updates it
            UpdateDistanceMap(result, generateNew, updateDistanceMap, showDistanceMap);

            return result;
        }

        #endregion
    }
}
