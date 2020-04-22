using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using YinYang.CodeProject.Projects.SimplePathfinding.Helpers;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.AStar;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.BestFirst;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.BreadthFirst;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.DepthFirst;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.Dijkstra;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.Evasion;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.JumpPoint;
using YinYang.CodeProject.Projects.SimplePathfinding.Properties;
using YinYang.CodeProject.Projects.SimplePathfinding.Scenarios;
using YinYang.CodeProject.Projects.SimplePathfinding.Scenarios.Geometric;
using YinYang.CodeProject.Projects.SimplePathfinding.Scenarios.Specialized;

namespace YinYang.CodeProject.Projects.SimplePathfinding
{
    public partial class MainForm : Form
    {
        #region | Constant |

        private const Int32 DefaultAreaWidth = 512; // 374
        private const Int32 DefaultAreaHeight = 512; // 390

        #endregion

        #region | Fields |

        private readonly Action processTasks;
        private readonly Action<Image> updateImage;
        private readonly Action<String> updateCaption;
        private readonly Func<CheckBox, Boolean> getOption;
        private readonly Func<Point, Point> getFormPosition;

        private Thread thread;
        private Bitmap image;
        private Graphics graphics;
        private Bitmap defaultImage;
        private Int32 objectDiameter;
        private Boolean needsUpdate;
        private Boolean turnOnEvents;
        private Graphics imageGraphics;
        private BlockMethodType methodType;

        private BasePathScenario activeScenario;
        private BasePathfinder activePathFinder;

        private List<BasePathScenario> scenarioList;
        private List<BasePathfinder> pathFinderList;

        private readonly List<Action> taskList;
        private readonly StopFunction stopFunction;

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            taskList = new List<Action>();

            needsUpdate = true;
            processTasks = ProcessTasks;
            methodType = BlockMethodType.Precise;
            objectDiameter = trackObjectDiameter.Value;
            getOption = checkbox => checkbox.Checked;
            updateCaption = caption => Text = caption;
            updateImage = image => graphics.DrawImageUnscaled(image, 0, 0);

            getFormPosition = PointToClient;

            MouseUp += (sender, args) => { if (args.Button == MouseButtons.Right) taskList.Add(() => RecreateDefaultImage()); };
            ClientSize = new Size(DefaultAreaWidth + 250, DefaultAreaHeight);

            // creates functions
            stopFunction = (x, y) => activeScenario.IsBlocked(x, y, objectDiameter);
        }

        #endregion

        #region | Process path-finding scenario |

        private void ProcessScenario()
        {
            // position starts at [0, 0]
            Point position = new Point(20, 20);
            Invoke(processTasks);

            using (image = new Bitmap(DefaultAreaWidth, DefaultAreaHeight, graphics))
            {
                imageGraphics = Graphics.FromImage(image);
                imageGraphics.SmoothingMode = SmoothingMode.HighQuality;

                do // repeats forever
                {
                    // starts measuring total time
                    DateTime startTime = DateTime.Now;

                    // prepares parameters
                    Point cursor = (Point) Invoke(getFormPosition, MousePosition);

                    // changes start position
                    if (MouseButtons.HasFlag(MouseButtons.Middle)) position = cursor;

                    // performs path finding with a given path finder
                    TimeSpan pathFindTime;
                    Int32 pointCount = DrawPath(image, imageGraphics, position, cursor, out pathFindTime);

                    // draws the markers
                    DrawMarker(imageGraphics, position, Brushes.WhiteSmoke, true);
                    DrawMarker(imageGraphics, cursor, Brushes.WhiteSmoke, true);

                    // updates the form information
                    Invoke(updateCaption, string.Format("Cursor: [{0:000}, {1:000}], Points: {2:0000}, Path finding time: {3:ss\\:ffffff}, Total time: {4:ss\\:ffffff}", cursor.X, cursor.Y, pointCount, pathFindTime, DateTime.Now - startTime));
                    Invoke(updateImage, image);

                    // restores the default image look
                    imageGraphics.DrawImageUnscaled(defaultImage, Point.Empty);

                    Invoke(processTasks);
                }
                while (true);
            }
        }

        #endregion

        #region | Methods |

        private void ChangePathFinder()
        {
            activePathFinder = pathFinderList[listMethod.SelectedIndex];
        }

        private void ChangeScenario()
        {
            activeScenario = scenarioList[listScenarios.SelectedIndex];

            taskList.Add(() =>
            {
                RecreateDefaultImage(false, needsUpdate);
                needsUpdate = false;
            });
        }

        private void RecreateDefaultImage(Boolean generateNew = true, Boolean updateDistanceMap = true)
        {
            Text = Resources.MainForm_RecreateScenario_WaitCaption;

            // disposes of old default image
            DisposeDefaultImage();

            // creates new default image for this scenario
            Boolean minimizeHollowAreas = (Boolean) Invoke(getOption, checkMinimizeHollowAreas);
            Boolean showDistanceMap = (Boolean) Invoke(getOption, checkShowDistanceMap);

            defaultImage = activeScenario.CreateDefaultImage(methodType, generateNew, minimizeHollowAreas, updateDistanceMap, showDistanceMap);
        }

        public static void DrawMarker(Graphics graphics, Point point, Brush color, Boolean frame = false, Boolean filled = true)
        {
            Int32 shift = filled ? 0 : 1;
            Rectangle rectangle = new Rectangle(point.X - 2, point.Y - 2, 5 - shift, 5 - shift);

            if (filled)
            {
                graphics.FillRectangle(color, rectangle);

                if (frame)
                {
                    graphics.DrawRectangle(Pens.DarkSlateGray, rectangle);
                }
            }
            else
            {
                graphics.DrawRectangle(new Pen(color), rectangle);
            }
        }

        private void ProcessTasks()
        {
            if (taskList.Count > 0)
            {
                try
                {
                    foreach (var action in taskList)
                    {
                        action();
                    }
                }
                finally
                {
                    taskList.Clear();
                }
            }
        }

        private void DisposeDefaultImage()
        {
            if (defaultImage != null)
            {
                defaultImage.Dispose();
                defaultImage = null;
            }
        }

        #endregion

        #region | Common |

        protected Int32 DrawPath(Bitmap image, Graphics graphics, Point start, Point end, out TimeSpan time)
        {
            // performs boundary check first
            BasePathScenario.CheckBounds(ref start, ref end, DefaultAreaWidth, DefaultAreaHeight);

            // determines the options
            Boolean flagPerformOptimizations = (Boolean) Invoke(getOption, checkPerformOptimization);
            Boolean flagDrawPivotPoints = (Boolean) Invoke(getOption, checkDrawPivotPoints);
            Boolean flagDrawLargerPivots = (Boolean) Invoke(getOption, checkDrawLargerPivots);

            // initializes the parameters
            Int32 result = 0;
            IReadOnlyCollection<Point> points;
            IReadOnlyCollection<Point> pivotPoints;

            // finds the path while measuring time elapsed
            DateTime startDate = DateTime.Now;
            Single precisionAlignment = objectDiameter%2 == 0 ? 0.5f : 0.0f;
            Pen pen = new Pen(Color.DarkGreen) { Width = objectDiameter - precisionAlignment, StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round };

            Boolean found = activePathFinder.TryFindPath(start, end, stopFunction, out points, out pivotPoints, flagPerformOptimizations);
            time = DateTime.Now - startDate;

            if (found)
            {
                // draws connected lines through all the points
                if (points.Count > 1)
                {
                    graphics.DrawLines(pen, points.ToArray());
                }

                // draws markers at pivot points (if turned on)
                if (flagDrawPivotPoints && pivotPoints != null)
                {
                    foreach (Point highlightPoint in pivotPoints)
                    {
                        if (flagDrawLargerPivots)
                        {
                            DrawMarker(graphics, highlightPoint, Brushes.Red, true);
                        }
                        else
                        {
                            image.SetPixel(highlightPoint.X, highlightPoint.Y, Color.Red);
                        }
                    }
                }

                result = points.Count;
            }

            return result;
        }

        #endregion

        #region << Form >>

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            graphics = CreateGraphics();

            ThreadStart operation = ProcessScenario;
            thread = new Thread(operation);
            thread.Start();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.D1) listMethod.SelectedIndex = 0;
            if (e.KeyCode == Keys.D2) listMethod.SelectedIndex = 1;
            if (e.KeyCode == Keys.D3) listMethod.SelectedIndex = 2;
            if (e.KeyCode == Keys.D4) listMethod.SelectedIndex = 3;
            if (e.KeyCode == Keys.D5) listMethod.SelectedIndex = 4;
            if (e.KeyCode == Keys.D6) listMethod.SelectedIndex = 5;
            if (e.KeyCode == Keys.D7) listMethod.SelectedIndex = 6;

            if (e.KeyCode == Keys.O) checkPerformOptimization.Checked = !checkPerformOptimization.Checked;
            if (e.KeyCode == Keys.P) checkDrawPivotPoints.Checked = !checkDrawPivotPoints.Checked;
            if (e.KeyCode == Keys.H) checkMinimizeHollowAreas.Checked = !checkMinimizeHollowAreas.Checked;
            if (e.KeyCode == Keys.L) checkDrawLargerPivots.Checked = !checkDrawLargerPivots.Checked;
            if (e.KeyCode == Keys.Q) checkUseHighQuality.Checked = !checkUseHighQuality.Checked;
            if (e.KeyCode == Keys.W) checkShowDistanceMap.Checked = !checkShowDistanceMap.Checked;

            if (e.KeyCode == Keys.F) radioMethodFast.Checked = true;
            if (e.KeyCode == Keys.R) radioMethodPrecise.Checked = true;

            if (e.KeyCode == Keys.Space) taskList.Add(() => RecreateDefaultImage());

            if (e.KeyCode == Keys.Add) trackObjectDiameter.Value = Math.Max(trackObjectDiameter.Minimum, Math.Min(trackObjectDiameter.Maximum, trackObjectDiameter.Value + 5));
            if (e.KeyCode == Keys.Subtract) trackObjectDiameter.Value = Math.Max(trackObjectDiameter.Minimum, Math.Min(trackObjectDiameter.Maximum, trackObjectDiameter.Value - 5));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
            }

            DisposeDefaultImage(); 
           
            base.OnClosing(e);
        }

        #endregion

        #region << Events >>

        private void MainFormLoad(object sender, EventArgs e)
        {
            pathFinderList = new List<BasePathfinder>
            {
                new AStarPathfinder(DefaultAreaWidth, DefaultAreaHeight),
                new BestFirstPathfinder(DefaultAreaWidth, DefaultAreaHeight),
                new BreadthFirstPathfinder(DefaultAreaWidth, DefaultAreaHeight),
                new DepthFirstPathfinder(DefaultAreaWidth, DefaultAreaHeight),
                new DijkstraPathfinder(DefaultAreaWidth, DefaultAreaHeight),
                new EvasionPathfinder(),
                new JumpPointPathfinder(DefaultAreaWidth, DefaultAreaHeight)
            };

            scenarioList = new List<BasePathScenario>
            {
                new RandomRectangleScenario(DefaultAreaWidth, DefaultAreaHeight),
                new RandomMarkerScenario(DefaultAreaWidth, DefaultAreaHeight),
                new RandomEllipseScenario(DefaultAreaWidth, DefaultAreaHeight),
                new RandomLineScenario(DefaultAreaWidth, DefaultAreaHeight),
                new BlackObeliskScenario(DefaultAreaWidth, DefaultAreaHeight),
                new ImageFileScenario(DefaultAreaWidth, DefaultAreaHeight),
            };

            turnOnEvents = false;

            listMethod.SelectedIndex = 5; // 0
            listScenarios.SelectedIndex = 0; // 0

            turnOnEvents = true;

            ChangePathFinder();
            ChangeScenario();
        }

        private void ListMethodSelectedIndexChanged(object sender, EventArgs e)
        {
            if (turnOnEvents) ChangePathFinder();
        }

        private void ListScenariosSelectedIndexChanged(object sender, EventArgs e)
        {
            if (turnOnEvents) ChangeScenario();
        }

        private void TrackDiameterValueChanged(object sender, EventArgs e)
        {
            taskList.Add(() => { objectDiameter = trackObjectDiameter.Value; });
        }

        private void CheckMinimizeHollowAreasCheckedChanged(object sender, EventArgs e)
        {
            taskList.Add(() =>
            {
                RecreateDefaultImage(false);
                needsUpdate = true;
            });
        }

        private void CheckUseHighQualityCheckedChanged(object sender, EventArgs e)
        {
            taskList.Add(() =>
            {
                imageGraphics.SmoothingMode = checkUseHighQuality.Checked ? SmoothingMode.HighQuality : SmoothingMode.HighSpeed;
            });
        }

        private void CheckShowDistanceMapCheckedChanged(object sender, EventArgs e)
        {
            taskList.Add(() => RecreateDefaultImage(false, false));
        }

        private void RadioMethodPreciseCheckedChanged(object sender, EventArgs e)
        {
            taskList.Add(() =>
            {
                methodType = radioMethodPrecise.Checked ? BlockMethodType.Precise : BlockMethodType.Fast;
                RecreateDefaultImage(false);
                needsUpdate = true;
            });
        }

        private void ListBlockKeyPress(object sender, KeyPressEventArgs e)
        {
            // disables switching on fast keys
            switch (Char.ToUpper(e.KeyChar))
            {
                case 'O': case 'P': case 'H': case 'L':
                case 'Q': case 'W': case 'F': case 'R':
                    e.Handled = true;
                    break;
            }
        }

        #endregion
    }
}
