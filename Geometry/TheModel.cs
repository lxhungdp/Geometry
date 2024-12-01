using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Labels;
using devDept.Geometry;

namespace Geometry
{
    public class TheModel : Model
    {
        public TheModel()
            : base()
        {
            Unlock("US20-N6120-WUD4M-ATYN-R68T");
        }

        public bool CreateGridforBarrier = true;
        

        public void SetupModel()
        {
            devDept.Eyeshot.CancelToolBarButton cancelToolBarButton1 = new devDept.Eyeshot.CancelToolBarButton("Cancel", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ProgressBar progressBar1 = new devDept.Eyeshot.ProgressBar(devDept.Eyeshot.ProgressBar.styleType.Circular, 0, "Idle", System.Drawing.Color.Black, System.Drawing.Color.Transparent, System.Drawing.Color.Green, 1D, true, cancelToolBarButton1, false, 0.1D, true);
            devDept.Graphics.BackgroundSettings backgroundSettings1 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.LinearGradient, System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245))))), System.Drawing.Color.DodgerBlue, System.Drawing.Color.WhiteSmoke, 0.75D, null, devDept.Graphics.colorThemeType.Auto, 0.33D);
            devDept.Eyeshot.Camera camera1 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(0D, 0D, 45D), 380D, new devDept.Geometry.Quaternion(0.018434349666532526D, 0.039532590434972079D, 0.42221602280006187D, 0.90544518284475428D), devDept.Graphics.projectionType.Perspective, 40D, 5.6363635063171387D, false, 0.001D);

            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton1 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton1 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);

            List<devDept.Eyeshot.ToolBarButton> toolBarButtons = new List<devDept.Eyeshot.ToolBarButton> { }; // zoomFitToolBarButton1, rotateToolBarButton1 };

            



            devDept.Eyeshot.ToolBar toolBar1 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopLeft, true, toolBarButtons.ToArray());


            //devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton1 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            //devDept.Eyeshot.ToolBar toolBar1 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopLeft, true, new devDept.Eyeshot.ToolBarButton[] {
            //((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton1))});
            devDept.Eyeshot.Grid grid1 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point2D(-100D, -100D), new devDept.Geometry.Point2D(100D, 100D), 10D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), false, false, false, false, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))));

            if (CreateGridforBarrier)
                grid1 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point2D(-100000D, -100000D), new devDept.Geometry.Point2D(100000D, 100000D), 500D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), false, true, false, false, 10, 100, 0, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))));

            OriginSymbol originSymbol1 = new OriginSymbol(2, originSymbolStyleType.CoordinateSystem, Color.Transparent, Color.Red, Color.Red, Color.Red, "", "Y", "X", null, true, false);
            //devDept.Eyeshot.OriginSymbol originSymbol1 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", false, null, false);
            devDept.Eyeshot.RotateSettings rotateSettings1 = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Ctrl), 10D, true, 1D, devDept.Eyeshot.rotationType.Trackball, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0D, 0D, 0D), false);
            devDept.Eyeshot.ZoomSettings zoomSettings1 = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1D, System.Drawing.Color.Empty, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10, true);
            devDept.Eyeshot.PanSettings panSettings1 = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 25, true);
            devDept.Eyeshot.NavigationSettings navigationSettings1 = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000D, -1000D, -1000D), new devDept.Geometry.Point3D(1000D, 1000D, 1000D), 8D, 50D, 50D);
            devDept.Eyeshot.Viewport.SavedViewsManager savedViewsManager1 = new devDept.Eyeshot.Viewport.SavedViewsManager(8);

            //Eyeshot12
            //devDept.Eyeshot.Viewport viewport1 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 0), new System.Drawing.Size(794, 420), backgroundSettings1, camera1, new devDept.Eyeshot.ToolBar[] {
            //toolBar1}, devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] {
            //grid1}, false, rotateSettings1, zoomSettings1, panSettings1, navigationSettings1, savedViewsManager1, devDept.Eyeshot.viewType.Trimetric);
            //originSymbol1.Visible = false;
            //viewport1.OriginSymbol = originSymbol1;

            //Eyeshot 2021
            devDept.Eyeshot.Viewport viewport1 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 0), new System.Drawing.Size(800, 372), backgroundSettings1, camera1, new devDept.Eyeshot.ToolBar[] {
            toolBar1}, devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] {
            grid1}, new devDept.Eyeshot.OriginSymbol[] {
            originSymbol1}, false, rotateSettings1, zoomSettings1, panSettings1, navigationSettings1, savedViewsManager1, devDept.Eyeshot.viewType.Trimetric);



            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon1 = new devDept.Eyeshot.CoordinateSystemIcon(System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", false, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, false);
            devDept.Eyeshot.ViewCubeIcon viewCubeIcon1 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, false, System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(60))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false);




            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            //model2.Location = new System.Drawing.Point(0, 0);
            //model2.Name = "model2";
            //this.ProgressBar = progressBar1;
            //model2.Size = new System.Drawing.Size(800, 372);
            //model2.TabIndex = 0;
            //model2.Text = "model2";

            originSymbol1.LabelFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            coordinateSystemIcon1.LabelFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            viewport1.CoordinateSystemIcon = coordinateSystemIcon1;
            viewport1.Legends = new devDept.Eyeshot.Legend[0];
            viewCubeIcon1.Font = null;
            viewCubeIcon1.InitialRotation = new devDept.Geometry.Quaternion(0D, 0D, 0D, 1D);
            viewport1.ViewCubeIcon = viewCubeIcon1;
            this.Viewports.Add(viewport1);

            this.SelectionColor = Color.Red;
            this.SelectionLineWeightScaleFactor = 4;
            this.SelectionColorDynamic = Color.Red;

        }


        public void CreateLayer()
        {
            Layer Segment = new Layer("Segment", Color.Red);
            Layer Origin = new Layer("Origin", Color.Green);
           

            this.Layers.Add(Segment);
            this.Layers.Add(Origin);
            
        }

        public List<Point> SettingOutPoint
        { get; set; }

        public int MatchSeg
        { get; set; }

        public int CastingSeg
        { get; set; }







        public void ClearAll()
        {
            this.Entities.Clear();
            this.Layers.Clear();
            this.LineTypes.Clear();
            this.Labels.Clear();
        }

        public void ClearEntitybyLayerName(string name)
        {
            foreach (Entity ent in Entities.ToList())
            {
                if (ent.LayerName == name)
                    this.Entities.Remove(ent);
            }
        }

        public void AddSegmentEntity()
        {
            ClearEntitybyLayerName("Segment");
            this.ActiveViewport.Labels.Clear();



            List<Entity> E = new List<Entity>();

            CompositeCurve Seg = Tools.CompCurvebyPLine(new List<Point>() { SettingOutPoint[0], SettingOutPoint[1] , SettingOutPoint[2] , SettingOutPoint[5] , SettingOutPoint[4] , SettingOutPoint[3] });
            devDept.Eyeshot.Entities.Region r1 = new devDept.Eyeshot.Entities.Region(Seg);
            r1.ColorMethod = colorMethodType.byEntity;
            r1.Color = Color.FromArgb(85, 178, 48);
            E.Add(r1);

            var MatchSeg = Tools.CompCurvebyPLine(new List<Point>() { SettingOutPoint[3], SettingOutPoint[4], SettingOutPoint[5], SettingOutPoint[8], SettingOutPoint[7], SettingOutPoint[6] });
            devDept.Eyeshot.Entities.Region r2 = new devDept.Eyeshot.Entities.Region(MatchSeg);
            r2.ColorMethod = colorMethodType.byEntity;
            r2.Color = Color.FromArgb(231, 101, 29);
            E.Add(r2);

            //Transformation transformation1 = new Translation(0, 0, 0);
            //OriginSymbol os1 = new OriginSymbol(2, originSymbolStyleType.CoordinateSystem, Color.Transparent, Color.Red, Color.Red, Color.Red, "", "Y", "X", null, true, false );
            //this.ActiveViewport.OriginSymbols = new[] {  os1 };
            //this.CompileUserInterfaceElements();

            List<TextOnly> Text = new List<TextOnly>();
            for (int i = 0; i < SettingOutPoint.Count; i++)
            {
                var S = SettingOutPoint[i];
                TextOnly T1 = new TextOnly(S.X, S.Y, 0 , S.Label, new Font("Tahoma", 9, FontStyle.Bold), System.Drawing.Color.Blue);
                Text.Add(T1);

                devDept.Eyeshot.Entities.Point p2 = new devDept.Eyeshot.Entities.Point(S.X, S.Y, 0, 6);
                p2.ColorMethod = colorMethodType.byEntity;
                p2.Color = Color.Red;
                E.Add(p2);

            }

            //Add Text
            TextOnly T2 = new TextOnly(0, SettingOutPoint[4].Y * 2 / 3, 0, "Casting Segment: " + CastingSeg.ToString(), new Font("Tahoma", 10, FontStyle.Bold), Color.Yellow, ContentAlignment.MiddleCenter);
            Text.Add(T2);
            T2 = new TextOnly(0, SettingOutPoint[4].Y + ( SettingOutPoint[7].Y - SettingOutPoint[4].Y) / 2, 0, "Match Segment: " + this.MatchSeg.ToString(), new Font("Tahoma", 10, FontStyle.Bold), Color.Yellow, ContentAlignment.MiddleCenter);
            Text.Add(T2);

            //BULK HEAD

            CompositeCurve b = CompositeCurve.CreateRectangle(-4500, -500, 9000, 500);
            devDept.Eyeshot.Entities.Region r3 = new devDept.Eyeshot.Entities.Region(b);
            r3.ColorMethod = colorMethodType.byEntity;
            r3.Color = Color.FromArgb(128, 128, 128);
            E.Add(r3);

            T2 = new TextOnly(0, -500, 0, "HULK HEAD", new Font("Tahoma", 9, FontStyle.Bold), Color.Red, ContentAlignment.BottomCenter);
            Text.Add(T2);


            this.Entities.AddRange(E, "Segment");
            this.ActiveViewport.Labels.AddRange(Text);
            this.Invalidate();

        }



        public void InitializeModel()
        {
            SetupModel();
            CreateLayer();
            AddSegmentEntity();

            this.SetView(viewType.Top);
            this.ZoomFit();
            this.ZoomOut(30);
           
            
        }
        
    }
}
