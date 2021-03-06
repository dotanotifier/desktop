﻿using System.Drawing;
using Tesseract;

namespace Dota_Notifier
{
    public class Screenshot
    {
        public static void Check(Rectangle bounds)
        {
            Bitmap image = TakeScreenshot(bounds);

            TesseractEngine engine = new TesseractEngine("assets/tessdata", "eng", EngineMode.Default);

            Page page = engine.Process(image);

            image.Dispose();

            string text = page.GetText();

            if (text.IndexOf("READY CHECK") >= 0)
            {
                Api.Notify(Api.READY_CHECK);
            }
            else if (text.IndexOf("GAME IS READY") >= 0)
            {
                Api.Notify(Api.MATCH_READY);
            }
        }

        public static Bitmap TakeScreenshot(Rectangle bounds)
        {
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);

            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);

            graphics.Dispose();

            return bitmap;
        }
    }
}
