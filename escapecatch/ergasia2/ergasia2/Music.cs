namespace ergasia2
{
    internal static class Music
    {
        public static float SharedVolume = 0.5f;
        public static bool Escape;
        public static void DoubleBuffered(this Control control, bool enable)
        {
            System.Reflection.PropertyInfo prop = typeof(Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            prop.SetValue(control, enable, null);
        }
    }
}
