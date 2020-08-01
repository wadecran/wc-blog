﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace wc_Blog.Helpers
{
    public class ImageUploadValidator
    {
        public static bool IsWebFriendlyImage(HttpPostedFileBase file)
        {
            if(file == null)
            {
                return false;
            }
            if(file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024)
            {
                return false;
            }

            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return ImageFormat.Jpeg.Equals(img.RawFormat) ||
                        ImageFormat.Png.Equals(img.RawFormat) ||
                        ImageFormat.Gif.Equals(img.RawFormat);
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool IsAvatarFriendlyImage(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return false;
            }
            if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024)
            {
                return false;
            }
            if (Image.FromStream(file.InputStream).Width == 700 && Image.FromStream(file.InputStream).Height == 700)
            {
                try
                {
                    using (var img = Image.FromStream(file.InputStream))
                    {
                        return ImageFormat.Jpeg.Equals(img.RawFormat) ||
                            ImageFormat.Png.Equals(img.RawFormat) ||
                            ImageFormat.Gif.Equals(img.RawFormat);
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}