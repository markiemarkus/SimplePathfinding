namespace SimplePathfinding
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelSettings = new System.Windows.Forms.Panel();
            this.panelHints = new System.Windows.Forms.Panel();
            this.labelHints = new System.Windows.Forms.Label();
            this.panelMethodKeysHint = new System.Windows.Forms.Panel();
            this.labelMethodKeysHint = new System.Windows.Forms.Label();
            this.panelMiddleMouseHint = new System.Windows.Forms.Panel();
            this.labelMiddleMouseHint = new System.Windows.Forms.Label();
            this.panelRightMouseHint = new System.Windows.Forms.Panel();
            this.labelRightMouseHint = new System.Windows.Forms.Label();
            this.panelObjectDiameter = new System.Windows.Forms.Panel();
            this.trackObjectDiameter = new System.Windows.Forms.TrackBar();
            this.labelObjectDiameter = new System.Windows.Forms.Label();
            this.panelUseHighQuality = new System.Windows.Forms.Panel();
            this.labelUseHighQuality = new System.Windows.Forms.Label();
            this.checkUseHighQuality = new System.Windows.Forms.CheckBox();
            this.panelDrawLargerPivots = new System.Windows.Forms.Panel();
            this.labelDrawLargerPivots = new System.Windows.Forms.Label();
            this.checkDrawLargerPivots = new System.Windows.Forms.CheckBox();
            this.panelDrawPivotPoints = new System.Windows.Forms.Panel();
            this.checkDrawPivotPoints = new System.Windows.Forms.CheckBox();
            this.labelDrawPivotPoints = new System.Windows.Forms.Label();
            this.panelShowDistanceMap = new System.Windows.Forms.Panel();
            this.labelShowDistanceMap = new System.Windows.Forms.Label();
            this.checkShowDistanceMap = new System.Windows.Forms.CheckBox();
            this.panelMinimizeHollowAreas = new System.Windows.Forms.Panel();
            this.labelMinimizeHollowAreas = new System.Windows.Forms.Label();
            this.checkMinimizeHollowAreas = new System.Windows.Forms.CheckBox();
            this.panelPerformOptimization = new System.Windows.Forms.Panel();
            this.labelPerformOptimization = new System.Windows.Forms.Label();
            this.checkPerformOptimization = new System.Windows.Forms.CheckBox();
            this.panelBlockCheckMethod = new System.Windows.Forms.Panel();
            this.labelBlockCheckMethod = new System.Windows.Forms.Label();
            this.radioMethodFast = new System.Windows.Forms.RadioButton();
            this.radioMethodPrecise = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.listScenarios = new System.Windows.Forms.ComboBox();
            this.labelScenario = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listMethod = new System.Windows.Forms.ComboBox();
            this.labelMethod = new System.Windows.Forms.Label();
            this.panelSettings.SuspendLayout();
            this.panelHints.SuspendLayout();
            this.panelMethodKeysHint.SuspendLayout();
            this.panelMiddleMouseHint.SuspendLayout();
            this.panelRightMouseHint.SuspendLayout();
            this.panelObjectDiameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackObjectDiameter)).BeginInit();
            this.panelUseHighQuality.SuspendLayout();
            this.panelDrawLargerPivots.SuspendLayout();
            this.panelDrawPivotPoints.SuspendLayout();
            this.panelShowDistanceMap.SuspendLayout();
            this.panelMinimizeHollowAreas.SuspendLayout();
            this.panelPerformOptimization.SuspendLayout();
            this.panelBlockCheckMethod.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSettings
            // 
            this.panelSettings.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelSettings.Controls.Add(this.panelHints);
            this.panelSettings.Controls.Add(this.panelMethodKeysHint);
            this.panelSettings.Controls.Add(this.panelMiddleMouseHint);
            this.panelSettings.Controls.Add(this.panelRightMouseHint);
            this.panelSettings.Controls.Add(this.panelObjectDiameter);
            this.panelSettings.Controls.Add(this.panelUseHighQuality);
            this.panelSettings.Controls.Add(this.panelDrawLargerPivots);
            this.panelSettings.Controls.Add(this.panelDrawPivotPoints);
            this.panelSettings.Controls.Add(this.panelShowDistanceMap);
            this.panelSettings.Controls.Add(this.panelMinimizeHollowAreas);
            this.panelSettings.Controls.Add(this.panelPerformOptimization);
            this.panelSettings.Controls.Add(this.panelBlockCheckMethod);
            this.panelSettings.Controls.Add(this.panel3);
            this.panelSettings.Controls.Add(this.panel1);
            this.panelSettings.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSettings.Location = new System.Drawing.Point(512, 0);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(250, 512);
            this.panelSettings.TabIndex = 13;
            // 
            // panelHints
            // 
            this.panelHints.Controls.Add(this.labelHints);
            this.panelHints.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelHints.Location = new System.Drawing.Point(0, 392);
            this.panelHints.Name = "panelHints";
            this.panelHints.Padding = new System.Windows.Forms.Padding(5);
            this.panelHints.Size = new System.Drawing.Size(250, 30);
            this.panelHints.TabIndex = 23;
            // 
            // labelHints
            // 
            this.labelHints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelHints.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHints.Location = new System.Drawing.Point(5, 5);
            this.labelHints.Name = "labelHints";
            this.labelHints.Size = new System.Drawing.Size(240, 20);
            this.labelHints.TabIndex = 20;
            this.labelHints.Text = "Hints:";
            this.labelHints.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelMethodKeysHint
            // 
            this.panelMethodKeysHint.Controls.Add(this.labelMethodKeysHint);
            this.panelMethodKeysHint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMethodKeysHint.Location = new System.Drawing.Point(0, 422);
            this.panelMethodKeysHint.Name = "panelMethodKeysHint";
            this.panelMethodKeysHint.Padding = new System.Windows.Forms.Padding(5);
            this.panelMethodKeysHint.Size = new System.Drawing.Size(250, 30);
            this.panelMethodKeysHint.TabIndex = 22;
            // 
            // labelMethodKeysHint
            // 
            this.labelMethodKeysHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMethodKeysHint.Location = new System.Drawing.Point(5, 5);
            this.labelMethodKeysHint.Name = "labelMethodKeysHint";
            this.labelMethodKeysHint.Size = new System.Drawing.Size(240, 20);
            this.labelMethodKeysHint.TabIndex = 20;
            this.labelMethodKeysHint.Text = "Press 1-7 keys to change method (in order of list)";
            this.labelMethodKeysHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelMiddleMouseHint
            // 
            this.panelMiddleMouseHint.Controls.Add(this.labelMiddleMouseHint);
            this.panelMiddleMouseHint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMiddleMouseHint.Location = new System.Drawing.Point(0, 452);
            this.panelMiddleMouseHint.Name = "panelMiddleMouseHint";
            this.panelMiddleMouseHint.Padding = new System.Windows.Forms.Padding(5);
            this.panelMiddleMouseHint.Size = new System.Drawing.Size(250, 30);
            this.panelMiddleMouseHint.TabIndex = 19;
            // 
            // labelMiddleMouseHint
            // 
            this.labelMiddleMouseHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMiddleMouseHint.Location = new System.Drawing.Point(5, 5);
            this.labelMiddleMouseHint.Name = "labelMiddleMouseHint";
            this.labelMiddleMouseHint.Size = new System.Drawing.Size(240, 20);
            this.labelMiddleMouseHint.TabIndex = 20;
            this.labelMiddleMouseHint.Text = "Press middle mouse button to change start point";
            this.labelMiddleMouseHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelRightMouseHint
            // 
            this.panelRightMouseHint.Controls.Add(this.labelRightMouseHint);
            this.panelRightMouseHint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRightMouseHint.Location = new System.Drawing.Point(0, 482);
            this.panelRightMouseHint.Name = "panelRightMouseHint";
            this.panelRightMouseHint.Padding = new System.Windows.Forms.Padding(5);
            this.panelRightMouseHint.Size = new System.Drawing.Size(250, 30);
            this.panelRightMouseHint.TabIndex = 18;
            // 
            // labelRightMouseHint
            // 
            this.labelRightMouseHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRightMouseHint.Location = new System.Drawing.Point(5, 5);
            this.labelRightMouseHint.Name = "labelRightMouseHint";
            this.labelRightMouseHint.Size = new System.Drawing.Size(240, 20);
            this.labelRightMouseHint.TabIndex = 20;
            this.labelRightMouseHint.Text = "Press right mouse button to create new scenario";
            this.labelRightMouseHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelObjectDiameter
            // 
            this.panelObjectDiameter.Controls.Add(this.trackObjectDiameter);
            this.panelObjectDiameter.Controls.Add(this.labelObjectDiameter);
            this.panelObjectDiameter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelObjectDiameter.Location = new System.Drawing.Point(0, 270);
            this.panelObjectDiameter.Name = "panelObjectDiameter";
            this.panelObjectDiameter.Padding = new System.Windows.Forms.Padding(5);
            this.panelObjectDiameter.Size = new System.Drawing.Size(250, 30);
            this.panelObjectDiameter.TabIndex = 25;
            // 
            // trackObjectDiameter
            // 
            this.trackObjectDiameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackObjectDiameter.LargeChange = 2;
            this.trackObjectDiameter.Location = new System.Drawing.Point(115, 5);
            this.trackObjectDiameter.Maximum = 100;
            this.trackObjectDiameter.Minimum = 1;
            this.trackObjectDiameter.Name = "trackObjectDiameter";
            this.trackObjectDiameter.Size = new System.Drawing.Size(130, 20);
            this.trackObjectDiameter.TabIndex = 23;
            this.trackObjectDiameter.TickFrequency = 5;
            this.trackObjectDiameter.Value = 2;
            this.trackObjectDiameter.ValueChanged += new System.EventHandler(this.TrackDiameterValueChanged);
            // 
            // labelObjectDiameter
            // 
            this.labelObjectDiameter.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelObjectDiameter.Location = new System.Drawing.Point(5, 5);
            this.labelObjectDiameter.Name = "labelObjectDiameter";
            this.labelObjectDiameter.Size = new System.Drawing.Size(110, 20);
            this.labelObjectDiameter.TabIndex = 20;
            this.labelObjectDiameter.Text = "Object diameter (+/-):";
            this.labelObjectDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelUseHighQuality
            // 
            this.panelUseHighQuality.Controls.Add(this.labelUseHighQuality);
            this.panelUseHighQuality.Controls.Add(this.checkUseHighQuality);
            this.panelUseHighQuality.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUseHighQuality.Location = new System.Drawing.Point(0, 240);
            this.panelUseHighQuality.Name = "panelUseHighQuality";
            this.panelUseHighQuality.Padding = new System.Windows.Forms.Padding(5);
            this.panelUseHighQuality.Size = new System.Drawing.Size(250, 30);
            this.panelUseHighQuality.TabIndex = 26;
            // 
            // labelUseHighQuality
            // 
            this.labelUseHighQuality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelUseHighQuality.Location = new System.Drawing.Point(5, 5);
            this.labelUseHighQuality.Name = "labelUseHighQuality";
            this.labelUseHighQuality.Size = new System.Drawing.Size(215, 20);
            this.labelUseHighQuality.TabIndex = 19;
            this.labelUseHighQuality.Text = "Use high quality graphics for path (key Q):";
            this.labelUseHighQuality.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkUseHighQuality
            // 
            this.checkUseHighQuality.AutoSize = true;
            this.checkUseHighQuality.Checked = true;
            this.checkUseHighQuality.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkUseHighQuality.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkUseHighQuality.Location = new System.Drawing.Point(220, 5);
            this.checkUseHighQuality.Name = "checkUseHighQuality";
            this.checkUseHighQuality.Padding = new System.Windows.Forms.Padding(5);
            this.checkUseHighQuality.Size = new System.Drawing.Size(25, 20);
            this.checkUseHighQuality.TabIndex = 18;
            this.checkUseHighQuality.UseVisualStyleBackColor = true;
            this.checkUseHighQuality.CheckedChanged += new System.EventHandler(this.CheckUseHighQualityCheckedChanged);
            // 
            // panelDrawLargerPivots
            // 
            this.panelDrawLargerPivots.Controls.Add(this.labelDrawLargerPivots);
            this.panelDrawLargerPivots.Controls.Add(this.checkDrawLargerPivots);
            this.panelDrawLargerPivots.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDrawLargerPivots.Location = new System.Drawing.Point(0, 210);
            this.panelDrawLargerPivots.Name = "panelDrawLargerPivots";
            this.panelDrawLargerPivots.Padding = new System.Windows.Forms.Padding(5);
            this.panelDrawLargerPivots.Size = new System.Drawing.Size(250, 30);
            this.panelDrawLargerPivots.TabIndex = 24;
            // 
            // labelDrawLargerPivots
            // 
            this.labelDrawLargerPivots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDrawLargerPivots.Location = new System.Drawing.Point(5, 5);
            this.labelDrawLargerPivots.Name = "labelDrawLargerPivots";
            this.labelDrawLargerPivots.Size = new System.Drawing.Size(215, 20);
            this.labelDrawLargerPivots.TabIndex = 20;
            this.labelDrawLargerPivots.Text = "Draw larger pivot points (key L):";
            this.labelDrawLargerPivots.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkDrawLargerPivots
            // 
            this.checkDrawLargerPivots.AutoSize = true;
            this.checkDrawLargerPivots.Checked = true;
            this.checkDrawLargerPivots.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkDrawLargerPivots.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkDrawLargerPivots.Location = new System.Drawing.Point(220, 5);
            this.checkDrawLargerPivots.Name = "checkDrawLargerPivots";
            this.checkDrawLargerPivots.Padding = new System.Windows.Forms.Padding(5);
            this.checkDrawLargerPivots.Size = new System.Drawing.Size(25, 20);
            this.checkDrawLargerPivots.TabIndex = 18;
            this.checkDrawLargerPivots.UseVisualStyleBackColor = true;
            // 
            // panelDrawPivotPoints
            // 
            this.panelDrawPivotPoints.Controls.Add(this.checkDrawPivotPoints);
            this.panelDrawPivotPoints.Controls.Add(this.labelDrawPivotPoints);
            this.panelDrawPivotPoints.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDrawPivotPoints.Location = new System.Drawing.Point(0, 180);
            this.panelDrawPivotPoints.Name = "panelDrawPivotPoints";
            this.panelDrawPivotPoints.Padding = new System.Windows.Forms.Padding(5);
            this.panelDrawPivotPoints.Size = new System.Drawing.Size(250, 30);
            this.panelDrawPivotPoints.TabIndex = 17;
            // 
            // checkDrawPivotPoints
            // 
            this.checkDrawPivotPoints.AutoSize = true;
            this.checkDrawPivotPoints.Checked = true;
            this.checkDrawPivotPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkDrawPivotPoints.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkDrawPivotPoints.Location = new System.Drawing.Point(220, 5);
            this.checkDrawPivotPoints.Name = "checkDrawPivotPoints";
            this.checkDrawPivotPoints.Padding = new System.Windows.Forms.Padding(5);
            this.checkDrawPivotPoints.Size = new System.Drawing.Size(25, 20);
            this.checkDrawPivotPoints.TabIndex = 18;
            this.checkDrawPivotPoints.UseVisualStyleBackColor = true;
            // 
            // labelDrawPivotPoints
            // 
            this.labelDrawPivotPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDrawPivotPoints.Location = new System.Drawing.Point(5, 5);
            this.labelDrawPivotPoints.Name = "labelDrawPivotPoints";
            this.labelDrawPivotPoints.Size = new System.Drawing.Size(240, 20);
            this.labelDrawPivotPoints.TabIndex = 20;
            this.labelDrawPivotPoints.Text = "Draw pivot points (key P):";
            this.labelDrawPivotPoints.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelShowDistanceMap
            // 
            this.panelShowDistanceMap.Controls.Add(this.labelShowDistanceMap);
            this.panelShowDistanceMap.Controls.Add(this.checkShowDistanceMap);
            this.panelShowDistanceMap.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelShowDistanceMap.Location = new System.Drawing.Point(0, 150);
            this.panelShowDistanceMap.Name = "panelShowDistanceMap";
            this.panelShowDistanceMap.Padding = new System.Windows.Forms.Padding(5);
            this.panelShowDistanceMap.Size = new System.Drawing.Size(250, 30);
            this.panelShowDistanceMap.TabIndex = 27;
            // 
            // labelShowDistanceMap
            // 
            this.labelShowDistanceMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelShowDistanceMap.Location = new System.Drawing.Point(5, 5);
            this.labelShowDistanceMap.Name = "labelShowDistanceMap";
            this.labelShowDistanceMap.Size = new System.Drawing.Size(215, 20);
            this.labelShowDistanceMap.TabIndex = 20;
            this.labelShowDistanceMap.Text = "Show distance map as intensity (key W):";
            this.labelShowDistanceMap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkShowDistanceMap
            // 
            this.checkShowDistanceMap.AutoSize = true;
            this.checkShowDistanceMap.Checked = true;
            this.checkShowDistanceMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkShowDistanceMap.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkShowDistanceMap.Location = new System.Drawing.Point(220, 5);
            this.checkShowDistanceMap.Name = "checkShowDistanceMap";
            this.checkShowDistanceMap.Padding = new System.Windows.Forms.Padding(5);
            this.checkShowDistanceMap.Size = new System.Drawing.Size(25, 20);
            this.checkShowDistanceMap.TabIndex = 18;
            this.checkShowDistanceMap.UseVisualStyleBackColor = true;
            this.checkShowDistanceMap.CheckedChanged += new System.EventHandler(this.CheckShowDistanceMapCheckedChanged);
            // 
            // panelMinimizeHollowAreas
            // 
            this.panelMinimizeHollowAreas.Controls.Add(this.labelMinimizeHollowAreas);
            this.panelMinimizeHollowAreas.Controls.Add(this.checkMinimizeHollowAreas);
            this.panelMinimizeHollowAreas.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMinimizeHollowAreas.Location = new System.Drawing.Point(0, 120);
            this.panelMinimizeHollowAreas.Name = "panelMinimizeHollowAreas";
            this.panelMinimizeHollowAreas.Padding = new System.Windows.Forms.Padding(5);
            this.panelMinimizeHollowAreas.Size = new System.Drawing.Size(250, 30);
            this.panelMinimizeHollowAreas.TabIndex = 21;
            // 
            // labelMinimizeHollowAreas
            // 
            this.labelMinimizeHollowAreas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMinimizeHollowAreas.Location = new System.Drawing.Point(5, 5);
            this.labelMinimizeHollowAreas.Name = "labelMinimizeHollowAreas";
            this.labelMinimizeHollowAreas.Size = new System.Drawing.Size(215, 20);
            this.labelMinimizeHollowAreas.TabIndex = 20;
            this.labelMinimizeHollowAreas.Text = "Minimize hollow areas (key H):";
            this.labelMinimizeHollowAreas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkMinimizeHollowAreas
            // 
            this.checkMinimizeHollowAreas.AutoSize = true;
            this.checkMinimizeHollowAreas.Checked = true;
            this.checkMinimizeHollowAreas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMinimizeHollowAreas.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkMinimizeHollowAreas.Location = new System.Drawing.Point(220, 5);
            this.checkMinimizeHollowAreas.Name = "checkMinimizeHollowAreas";
            this.checkMinimizeHollowAreas.Padding = new System.Windows.Forms.Padding(5);
            this.checkMinimizeHollowAreas.Size = new System.Drawing.Size(25, 20);
            this.checkMinimizeHollowAreas.TabIndex = 18;
            this.checkMinimizeHollowAreas.UseVisualStyleBackColor = true;
            this.checkMinimizeHollowAreas.CheckedChanged += new System.EventHandler(this.CheckMinimizeHollowAreasCheckedChanged);
            // 
            // panelPerformOptimization
            // 
            this.panelPerformOptimization.Controls.Add(this.labelPerformOptimization);
            this.panelPerformOptimization.Controls.Add(this.checkPerformOptimization);
            this.panelPerformOptimization.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPerformOptimization.Location = new System.Drawing.Point(0, 90);
            this.panelPerformOptimization.Name = "panelPerformOptimization";
            this.panelPerformOptimization.Padding = new System.Windows.Forms.Padding(5);
            this.panelPerformOptimization.Size = new System.Drawing.Size(250, 30);
            this.panelPerformOptimization.TabIndex = 16;
            // 
            // labelPerformOptimization
            // 
            this.labelPerformOptimization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPerformOptimization.Location = new System.Drawing.Point(5, 5);
            this.labelPerformOptimization.Name = "labelPerformOptimization";
            this.labelPerformOptimization.Size = new System.Drawing.Size(215, 20);
            this.labelPerformOptimization.TabIndex = 19;
            this.labelPerformOptimization.Text = "Perform the optimizations (key O):";
            this.labelPerformOptimization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkPerformOptimization
            // 
            this.checkPerformOptimization.AutoSize = true;
            this.checkPerformOptimization.Checked = true;
            this.checkPerformOptimization.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkPerformOptimization.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkPerformOptimization.Location = new System.Drawing.Point(220, 5);
            this.checkPerformOptimization.Name = "checkPerformOptimization";
            this.checkPerformOptimization.Padding = new System.Windows.Forms.Padding(5);
            this.checkPerformOptimization.Size = new System.Drawing.Size(25, 20);
            this.checkPerformOptimization.TabIndex = 18;
            this.checkPerformOptimization.UseVisualStyleBackColor = true;
            // 
            // panelBlockCheckMethod
            // 
            this.panelBlockCheckMethod.Controls.Add(this.labelBlockCheckMethod);
            this.panelBlockCheckMethod.Controls.Add(this.radioMethodFast);
            this.panelBlockCheckMethod.Controls.Add(this.radioMethodPrecise);
            this.panelBlockCheckMethod.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBlockCheckMethod.Location = new System.Drawing.Point(0, 60);
            this.panelBlockCheckMethod.Name = "panelBlockCheckMethod";
            this.panelBlockCheckMethod.Padding = new System.Windows.Forms.Padding(5);
            this.panelBlockCheckMethod.Size = new System.Drawing.Size(250, 30);
            this.panelBlockCheckMethod.TabIndex = 28;
            // 
            // labelBlockCheckMethod
            // 
            this.labelBlockCheckMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBlockCheckMethod.Location = new System.Drawing.Point(5, 5);
            this.labelBlockCheckMethod.Name = "labelBlockCheckMethod";
            this.labelBlockCheckMethod.Size = new System.Drawing.Size(103, 20);
            this.labelBlockCheckMethod.TabIndex = 22;
            this.labelBlockCheckMethod.Text = "Obstacle detection:";
            this.labelBlockCheckMethod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radioMethodFast
            // 
            this.radioMethodFast.AutoSize = true;
            this.radioMethodFast.Dock = System.Windows.Forms.DockStyle.Right;
            this.radioMethodFast.Location = new System.Drawing.Point(108, 5);
            this.radioMethodFast.Name = "radioMethodFast";
            this.radioMethodFast.Size = new System.Drawing.Size(60, 20);
            this.radioMethodFast.TabIndex = 21;
            this.radioMethodFast.Text = "Fast (F)";
            this.radioMethodFast.UseVisualStyleBackColor = true;
            // 
            // radioMethodPrecise
            // 
            this.radioMethodPrecise.AutoSize = true;
            this.radioMethodPrecise.Checked = true;
            this.radioMethodPrecise.Dock = System.Windows.Forms.DockStyle.Right;
            this.radioMethodPrecise.Location = new System.Drawing.Point(168, 5);
            this.radioMethodPrecise.Name = "radioMethodPrecise";
            this.radioMethodPrecise.Size = new System.Drawing.Size(77, 20);
            this.radioMethodPrecise.TabIndex = 20;
            this.radioMethodPrecise.TabStop = true;
            this.radioMethodPrecise.Text = "Precise (R)";
            this.radioMethodPrecise.UseVisualStyleBackColor = true;
            this.radioMethodPrecise.CheckedChanged += new System.EventHandler(this.RadioMethodPreciseCheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.listScenarios);
            this.panel3.Controls.Add(this.labelScenario);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 30);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(250, 30);
            this.panel3.TabIndex = 14;
            // 
            // listScenarios
            // 
            this.listScenarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listScenarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listScenarios.FormattingEnabled = true;
            this.listScenarios.Items.AddRange(new object[] {
            "Random rectangles scenario",
            "Random markers scenario",
            "Random ellipses scenario",
            "Random lines scenario",
            "Black obelisk scenario",
            "Image file scenario"});
            this.listScenarios.Location = new System.Drawing.Point(58, 5);
            this.listScenarios.MinimumSize = new System.Drawing.Size(45, 0);
            this.listScenarios.Name = "listScenarios";
            this.listScenarios.Size = new System.Drawing.Size(187, 21);
            this.listScenarios.TabIndex = 15;
            this.listScenarios.SelectedIndexChanged += new System.EventHandler(this.ListScenariosSelectedIndexChanged);
            this.listScenarios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListBlockKeyPress);
            // 
            // labelScenario
            // 
            this.labelScenario.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelScenario.Location = new System.Drawing.Point(5, 5);
            this.labelScenario.Name = "labelScenario";
            this.labelScenario.Size = new System.Drawing.Size(53, 20);
            this.labelScenario.TabIndex = 14;
            this.labelScenario.Text = "Scenario:";
            this.labelScenario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listMethod);
            this.panel1.Controls.Add(this.labelMethod);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(250, 30);
            this.panel1.TabIndex = 13;
            // 
            // listMethod
            // 
            this.listMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listMethod.FormattingEnabled = true;
            this.listMethod.Items.AddRange(new object[] {
            "A* pathfinder",
            "Best-first pathfinder",
            "Breadth-first pathfinder (SLOW)",
            "Depth-first pathfinder (SLOW)",
            "Dijkstra pathfinder (SLOW)",
            "Evasion pathfinder",
            "Jump point pathfinder"});
            this.listMethod.Location = new System.Drawing.Point(93, 5);
            this.listMethod.Name = "listMethod";
            this.listMethod.Size = new System.Drawing.Size(152, 21);
            this.listMethod.TabIndex = 2;
            this.listMethod.SelectedIndexChanged += new System.EventHandler(this.ListMethodSelectedIndexChanged);
            this.listMethod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListBlockKeyPress);
            // 
            // labelMethod
            // 
            this.labelMethod.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelMethod.Location = new System.Drawing.Point(5, 5);
            this.labelMethod.Name = "labelMethod";
            this.labelMethod.Size = new System.Drawing.Size(88, 20);
            this.labelMethod.TabIndex = 8;
            this.labelMethod.Text = "Select a method:";
            this.labelMethod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 512);
            this.Controls.Add(this.panelSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple pathfinding";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.panelSettings.ResumeLayout(false);
            this.panelHints.ResumeLayout(false);
            this.panelMethodKeysHint.ResumeLayout(false);
            this.panelMiddleMouseHint.ResumeLayout(false);
            this.panelRightMouseHint.ResumeLayout(false);
            this.panelObjectDiameter.ResumeLayout(false);
            this.panelObjectDiameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackObjectDiameter)).EndInit();
            this.panelUseHighQuality.ResumeLayout(false);
            this.panelUseHighQuality.PerformLayout();
            this.panelDrawLargerPivots.ResumeLayout(false);
            this.panelDrawLargerPivots.PerformLayout();
            this.panelDrawPivotPoints.ResumeLayout(false);
            this.panelDrawPivotPoints.PerformLayout();
            this.panelShowDistanceMap.ResumeLayout(false);
            this.panelShowDistanceMap.PerformLayout();
            this.panelMinimizeHollowAreas.ResumeLayout(false);
            this.panelMinimizeHollowAreas.PerformLayout();
            this.panelPerformOptimization.ResumeLayout(false);
            this.panelPerformOptimization.PerformLayout();
            this.panelBlockCheckMethod.ResumeLayout(false);
            this.panelBlockCheckMethod.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox listScenarios;
        private System.Windows.Forms.Label labelScenario;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox listMethod;
        private System.Windows.Forms.Label labelMethod;
        private System.Windows.Forms.Panel panelPerformOptimization;
        private System.Windows.Forms.CheckBox checkPerformOptimization;
        private System.Windows.Forms.Panel panelDrawPivotPoints;
        private System.Windows.Forms.Label labelDrawPivotPoints;
        private System.Windows.Forms.CheckBox checkDrawPivotPoints;
        private System.Windows.Forms.Label labelPerformOptimization;
        private System.Windows.Forms.Panel panelMiddleMouseHint;
        private System.Windows.Forms.Label labelMiddleMouseHint;
        private System.Windows.Forms.Panel panelRightMouseHint;
        private System.Windows.Forms.Label labelRightMouseHint;
        private System.Windows.Forms.Panel panelMinimizeHollowAreas;
        private System.Windows.Forms.Label labelMinimizeHollowAreas;
        private System.Windows.Forms.CheckBox checkMinimizeHollowAreas;
        private System.Windows.Forms.Panel panelHints;
        private System.Windows.Forms.Label labelHints;
        private System.Windows.Forms.Panel panelMethodKeysHint;
        private System.Windows.Forms.Label labelMethodKeysHint;
        private System.Windows.Forms.Panel panelDrawLargerPivots;
        private System.Windows.Forms.Label labelDrawLargerPivots;
        private System.Windows.Forms.CheckBox checkDrawLargerPivots;
        private System.Windows.Forms.Panel panelObjectDiameter;
        private System.Windows.Forms.TrackBar trackObjectDiameter;
        private System.Windows.Forms.Label labelObjectDiameter;
        private System.Windows.Forms.Panel panelUseHighQuality;
        private System.Windows.Forms.Label labelUseHighQuality;
        private System.Windows.Forms.CheckBox checkUseHighQuality;
        private System.Windows.Forms.Panel panelShowDistanceMap;
        private System.Windows.Forms.Label labelShowDistanceMap;
        private System.Windows.Forms.CheckBox checkShowDistanceMap;
        private System.Windows.Forms.Panel panelBlockCheckMethod;
        private System.Windows.Forms.Label labelBlockCheckMethod;
        private System.Windows.Forms.RadioButton radioMethodFast;
        private System.Windows.Forms.RadioButton radioMethodPrecise;


    }
}

