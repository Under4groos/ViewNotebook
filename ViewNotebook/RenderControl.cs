using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Size = System.Windows.Size;


public class WPFRenderControl
{

    public FrameworkElement frameworkelement;

    BitmapSource bitmap;
    public string Name
    {
        get; set;
    } = "image";
    public Size SizeControl
    {
        get; set;
    } = Size.Empty;

    public int DPI_X
    {
        get; set;
    } = 96;
    public int DPI_Y
    {
        get; set;
    } = 96;
    public WPFRenderControl()
    {
        // Хачу Жрать!

    }
    public WPFRenderControl(FrameworkElement element)
    {
        frameworkelement = element;
        SizeUpdate();
    }
    public WPFRenderControl(FrameworkElement visual, Size size)
    {
        frameworkelement = visual;
        SizeControl = size;
        SizeUpdate();
    }
    public void SizeUpdate()
    {
        if (frameworkelement.ActualWidth > 0 && frameworkelement.ActualHeight > 0)
            SizeControl = new Size(frameworkelement.ActualWidth, frameworkelement.ActualHeight);
        if (frameworkelement.Width > 0 && frameworkelement.Height > 0)
            SizeControl = new Size(frameworkelement.Width, frameworkelement.Height);
       
        Console.WriteLine(SizeControl);
    }
    public void Render()
    {
        if (frameworkelement == null)
            return;
      
        frameworkelement.Measure(SizeControl);
        frameworkelement.Arrange(new Rect(SizeControl));
        RenderTargetBitmap bmp = new RenderTargetBitmap(
            (int)SizeControl.Width,
            (int)SizeControl.Height,
            DPI_X, DPI_Y, PixelFormats.Pbgra32);
        bmp.Render(frameworkelement);
        bitmap = BitmapFrame.Create(bmp);

        BitmapEncoder encoder = new PngBitmapEncoder();

        encoder.Frames.Add(BitmapFrame.Create(bitmap));

        using (var fileStream = new System.IO.FileStream(Name + ".png", System.IO.FileMode.Create))
        {
            encoder.Save(fileStream);
        }

        // Bitmap theBitmap = new Bitmap("testa.png");

        // theBitmap.Save(@"testa.ico", System.Drawing.Imaging.ImageFormat.Icon);
    }


}
