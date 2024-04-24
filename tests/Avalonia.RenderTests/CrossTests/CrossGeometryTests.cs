using System.Collections.Generic;
using Avalonia.Media;
using CrossUI;
using Xunit;

#if AVALONIA_SKIA
namespace Avalonia.Skia.RenderTests;
#elif AVALONIA_D2D
namespace Avalonia.Direct2D1.RenderTests;
#else
namespace Avalonia.RenderTests.WpfCompare;
#endif


public class CrossGeometryTests : CrossTestBase
{
    public CrossGeometryTests() : base("Media/Geometry")
    {
    }

    [CrossFact]
    public void Should_Render_Stream_Geometry()
    {
        var geometry = new CrossStreamGeometry();

        var context = geometry.GetContext();
        context.BeginFigure(new Point(150, 15), true, true);
        context.LineTo(new Point(258, 77), true);
        context.LineTo(new Point(258, 202), true);
        context.LineTo(new Point(150, 265), true);
        context.LineTo(new Point(42, 202), true);
        context.LineTo(new Point(42, 77), true);
        context.EndFigure();

        var brush = new CrossDrawingBrush()
        {
            TileMode = TileMode.None,
            Drawing = new CrossDrawingGroup()
            {
                Children = new List<CrossDrawing>()
                {
                    new CrossGeometryDrawing(new CrossRectangleGeometry(new(0, 0, 300, 280)))
                    {
                        Brush = new CrossSolidColorBrush(Colors.White)
                    },
                    new CrossGeometryDrawing(geometry)
                    {
                        Pen = new CrossPen()
                        {
                            Brush = new CrossSolidColorBrush(Colors.Black),
                            Thickness = 2
                        }
                    }
                }
            }
        };

        RenderAndCompare(new CrossControl()
        {
            Width = 300,
            Height = 280,
            Background = brush
        });
    }

    [CrossFact]
    public void Should_Render_Geometry_With_Strokeless_Lines()
    {
        var geometry = new CrossStreamGeometry();

        var context = geometry.GetContext();
        context.BeginFigure(new Point(150, 15), true, true);
        context.LineTo(new Point(258, 77), true);
        context.LineTo(new Point(258, 202), false);
        context.LineTo(new Point(150, 265), true);
        context.LineTo(new Point(42, 202), true);
        context.LineTo(new Point(42, 77), false);
        context.EndFigure();

        var brush = new CrossDrawingBrush()
        {
            TileMode = TileMode.None,
            Drawing = new CrossDrawingGroup()
            {
                Children = new List<CrossDrawing>()
                {
                    new CrossGeometryDrawing(new CrossRectangleGeometry(new(0, 0, 300, 280)))
                    {
                        Brush = new CrossSolidColorBrush(Colors.White)
                    },
                    new CrossGeometryDrawing(geometry)
                    {
                        Pen = new CrossPen()
                        {
                            Brush = new CrossSolidColorBrush(Colors.Black),
                            Thickness = 2
                        }
                    }
                }
            }
        };

        RenderAndCompare(new CrossControl()
        {
            Width = 300,
            Height = 280,
            Background = brush
        });
    }
}
