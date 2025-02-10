namespace MauiApp1
{
    public static class PixelToDPI
    {
        public static double PixelsToDpis(double pixels)
        {
            var displayInfo = DeviceDisplay.MainDisplayInfo;
            double dpi = displayInfo.Density;
            return pixels / dpi;
        }
    }
}
