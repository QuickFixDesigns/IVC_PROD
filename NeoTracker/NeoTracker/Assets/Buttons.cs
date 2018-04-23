﻿using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NeoTracker.Content
{
    public class Buttons
    {
        public void SetButton(ModernButton btn, bool IsLarge, string icon, string content, string tooltip)
        {
            if (IsLarge)
            {
                btn.FontWeight = FontWeights.Bold;
                btn.EllipseDiameter = 40;
                btn.IconWidth = 25;
                btn.IconHeight = 25;
            }

            btn.Margin = new Thickness() { Top = 5, Bottom = 5 };

            switch (icon)
            {
                case "Apply":
                    btn.IconData = Geometry.Parse("F1 M 23.7501,33.25L 34.8334,44.3333L 52.2499,22.1668L 56.9999,26.9168L 34.8334,53.8333L 19.0001,38L 23.7501,33.25 Z ");
                    btn.Content = string.IsNullOrEmpty(content) ? "Apply" : content;
                    btn.ToolTip = string.IsNullOrEmpty(content) ? "Apply changes" : tooltip;
                    break;
                case "Cancel":
                    btn.IconData = Geometry.Parse("F1 M 26.9166,22.1667L 37.9999,33.25L 49.0832,22.1668L 53.8332,26.9168L 42.7499,38L 53.8332,49.0834L 49.0833,53.8334L 37.9999,42.75L 26.9166,53.8334L 22.1666,49.0833L 33.25,38L 22.1667,26.9167L 26.9166,22.1667 Z ");
                    btn.Content = string.IsNullOrEmpty(content) ? "Cancel" : content;
                    btn.ToolTip = string.IsNullOrEmpty(content) ? "Cancel changes" : tooltip;
                    break;

                case "Create":
                    btn.IconData = Geometry.Parse("F1 M 17,21L 25,21L 25,29L 17,29L 17,21 Z M 17,32L 25,32L 25,40L 17,40L 17,32 Z M 29,21L 55,21L 55,29L 29,29L 29,21 Z M 17,43L 25,43L 25,51L 17,51L 17,43 Z M 29,32L 55,32L 55,40L 29,40L 29,32 Z M 29,43L 55,43L 55,51L 29,51L 29,43 Z M 53,62L 50,62L 50,56L 53,56L 53,62 Z M 61.9215,57.4761L 59.8002,59.5974L 55.5576,55.3548L 57.6789,53.2335L 61.9215,57.4761 Z M 65.0313,47.9688L 65.0313,50.9687L 59.0313,50.9687L 59.0313,47.9688L 65.0313,47.9688 Z M 43.4926,58.8904L 41.3713,56.769L 45.614,52.5264L 47.7353,54.6477L 43.4926,58.8904 Z M 60.6792,39.1972L 62.7631,41.3552L 58.4471,45.5232L 56.3631,43.3652L 60.6792,39.1972 Z ");
                    btn.Content = string.IsNullOrEmpty(content) ? "not set" : content;
                    btn.ToolTip = string.IsNullOrEmpty(content) ? "not set" : tooltip;
                    break;
                case "Delete":
                    btn.IconData = Geometry.Parse("F1 M 25.3333,23.75L 50.6667,23.75C 51.5411,23.75 51.8541,27.3125 51.8541,27.3125L 24.1458,27.3125C 24.1458,27.3125 24.4589,23.75 25.3333,23.75 Z M 35.625,19.7917L 40.375,19.7917C 40.8122,19.7917 41.9583,20.9378 41.9583,21.375C 41.9583,21.8122 40.8122,22.9584 40.375,22.9584L 35.625,22.9584C 35.1878,22.9584 34.0416,21.8122 34.0416,21.375C 34.0416,20.9378 35.1878,19.7917 35.625,19.7917 Z M 27.7083,28.5L 48.2916,28.5C 49.1661,28.5 49.875,29.2089 49.875,30.0834L 48.2916,53.8334C 48.2916,54.7078 47.5828,55.4167 46.7083,55.4167L 29.2917,55.4167C 28.4172,55.4167 27.7083,54.7078 27.7083,53.8334L 26.125,30.0834C 26.125,29.2089 26.8339,28.5 27.7083,28.5 Z M 30.0833,31.6667L 30.4792,52.25L 33.25,52.25L 32.8542,31.6667L 30.0833,31.6667 Z M 36.4167,31.6667L 36.4167,52.25L 39.5833,52.25L 39.5833,31.6667L 36.4167,31.6667 Z M 43.1458,31.6667L 42.75,52.25L 45.5208,52.25L 45.9167,31.6667L 43.1458,31.6667 Z ");
                    btn.Content = string.IsNullOrEmpty(content) ? "Delete" : content;
                    btn.ToolTip = string.IsNullOrEmpty(content) ? "not set" : tooltip;
                    break;
                case "SelectAll":
                    btn.IconData = Geometry.Parse("F1 M 32.2209,33.4875L 39.1875,40.0582L 52.9627,24.5415L 56.2877,27.4707L 39.5834,47.5L 28.8959,36.8125L 32.2209,33.4875 Z M 22,25L 50,25L 45.5,30L 27,30L 27,49L 46,49L 46,42.5L 51,36.5L 51,54L 22,54L 22,25 Z  ");
                    btn.ToolTip = "Select/unselect all";
                    break;


                case "Filter":
                    btn.IconData = Geometry.Parse("F1 M 42.5,22C 49.4036,22 55,27.5964 55,34.5C 55,41.4036 49.4036,47 42.5,47C 40.1356,47 37.9245,46.3435 36,45.2426L 26.9749,54.2678C 25.8033,55.4393 23.9038,55.4393 22.7322,54.2678C 21.5607,53.0962 21.5607,51.1967 22.7322,50.0251L 31.7971,40.961C 30.6565,39.0755 30,36.8644 30,34.5C 30,27.5964 35.5964,22 42.5,22 Z M 42.5,26C 37.8056,26 34,29.8056 34,34.5C 34,39.1944 37.8056,43 42.5,43C 47.1944,43 51,39.1944 51,34.5C 51,29.8056 47.1944,26 42.5,26 Z ");
                    break;
                case "Unfilter":
                    btn.IconData = Geometry.Parse("F1 M 42.5,22C 49.4036,22 55,27.5964 55,34.5C 55,41.4036 49.4036,47 42.5,47C 40.1356,47 37.9245,46.3435 36,45.2426L 26.9749,54.2678C 25.8033,55.4393 23.9038,55.4393 22.7322,54.2678C 21.5607,53.0962 21.5607,51.1967 22.7322,50.0251L 31.7971,40.961C 30.6565,39.0755 30,36.8644 30,34.5C 30,27.5964 35.5964,22 42.5,22 Z M 42.5,26C 37.8056,26 34,29.8056 34,34.5C 34,39.1944 37.8056,43 42.5,43C 47.1944,43 51,39.1944 51,34.5C 51,29.8056 47.1944,26 42.5,26 Z M 48,33L 48,36L 37,36L 37,33L 48,33 Z");
                    break;
                case "Remove":
                    btn.IconData = Geometry.Parse("F1 M 19,29L 47,29L 47,57L 19,57L 19,29 Z M 43,33L 23,33.0001L 23,53L 43,53L 43,33 Z M 39,41L 39,45L 27,45L 27,41L 39,41 Z M 24,24L 51.9999,24.0001L 51.9999,52L 48.9999,52.0001L 48.9999,27.0001L 24,27.0001L 24,24 Z M 54,47L 53.9999,22.0001L 29,22L 29,19L 57,19L 57,47L 54,47 Z");
                    break;

                case "Excel":
                    btn.IconData = Geometry.Parse("F1 M 42,24L 57,24L 57,52L 42,52L 42,50L 47,50L 47,46L 42,46L 42,45L 47,45L 47,41L 42,41L 42,40L 47,40L 47,36L 42,36L 42,35L 47,35L 47,31L 42,31L 42,30L 47,30L 47,26L 42,26L 42,24 Z M 54.9995,50.0005L 54.9997,46.0003L 47.9995,46.0003L 47.9995,50.0005L 54.9995,50.0005 Z M 54.9996,41.0004L 47.9995,41.0004L 47.9995,45.0003L 54.9997,45.0003L 54.9996,41.0004 Z M 54.9996,36.0004L 47.9995,36.0004L 47.9995,40.0004L 54.9996,40.0004L 54.9996,36.0004 Z M 54.9996,31.0004L 47.9995,31.0004L 47.9995,35.0004L 54.9996,35.0004L 54.9996,31.0004 Z M 54.9995,26.0005L 47.9995,26.0005L 47.9995,30.0004L 54.9996,30.0004L 54.9995,26.0005 Z M 18.9997,23.7503L 40.9994,19.7506L 40.9994,56.2506L 18.9997,52.2503L 18.9997,23.7503 Z M 34.6404,44.5147L 31.3367,37.4084L 34.5522,30.4699L 31.9399,30.5805L 30.2234,34.6963L 30.0162,35.3903L 29.8872,35.8892L 29.8596,35.8895C 29.4574,34.1248 28.7481,32.4436 28.1318,30.7417L 25.2803,30.8624L 28.2549,37.4637L 24.997,44.0621L 27.7904,44.1932L 29.5296,39.8757L 29.7578,38.9297L 29.7876,38.93C 30.2317,40.8236 31.1236,42.5844 31.861,44.3843L 34.6404,44.5147 Z ");
                    btn.Content = string.IsNullOrEmpty(content) ? string.Empty : content;
                    btn.ToolTip = string.IsNullOrEmpty(content) ? string.Empty : tooltip;
                    break;


                case "Picture":
                    btn.IconData = Geometry.Parse("F1 M 30,27C 30,24.3766 32.3767,22 35,22L 41,22C 43.6234,22 46,24.3766 46,27L 50.9999,27.0001C 53.7613,27.0001 55.9999,29.2387 55.9999,32.0001L 55.9999,46.0001C 55.9999,48.7615 53.7613,51.0001 50.9999,51.0001L 25,51.0001C 22.2385,51.0001 20,48.7615 20,46.0001L 20,32.0001C 20,29.2387 22.2385,27.0001 25,27.0001L 30,27 Z M 25.5,30C 24.6716,30 24,30.8954 24,32C 24,33.1046 24.6716,34 25.5,34C 26.3284,34 27,33.1046 27,32C 27,30.8954 26.3284,30 25.5,30 Z M 38,32C 34.134,32 31,35.134 31,39C 31,42.866 34.134,46 38,46C 41.866,46 45,42.866 45,39C 45,35.134 41.866,32 38,32 Z M 38,34.5C 40.4853,34.5 42.5,36.5147 42.5,39C 42.5,41.4853 40.4853,43.5 38,43.5C 35.5147,43.5 33.5,41.4853 33.5,39C 33.5,36.5147 35.5147,34.5 38,34.5 Z ");
                    break;
                case "NoPicture":
                    btn.IconData = Geometry.Parse("F1 M 19,19L 27,19L 27,24L 19,24L 19,19 Z M 30,19L 38,19L 38,24L 30,24L 30,19 Z M 41,19L 49,19L 49,24L 41,24L 41,19 Z M 52,19L 57,19L 57,27L 52,27L 52,19 Z M 52,30L 57,30L 57,38L 52,38L 52,30 Z M 52,41L 57,41L 57,49L 52,49L 52,41 Z M 27,52L 35,52L 35,57L 27,57L 27,52 Z M 38,52L 46,52L 46,57L 38,57L 38,52 Z M 49,52L 57,52L 57,57L 49,57L 49,52 Z M 19,27L 24,27L 24,35L 19,35L 19,27 Z M 19,38L 24,38L 24,46L 19,46L 19,38 Z M 19,49L 24,49L 24,57L 19,57L 19,49 Z ");
                    break;

                case "Reset":
                    btn.IconData = Geometry.Parse("F1 M 26.9166,22.1667L 37.9999,33.25L 49.0832,22.1668L 53.8332,26.9168L 42.7499,38L 53.8332,49.0834L 49.0833,53.8334L 37.9999,42.75L 26.9166,53.8334L 22.1666,49.0833L 33.25,38L 22.1667,26.9167L 26.9166,22.1667 Z ");
                    btn.Content = string.IsNullOrEmpty(content) ? string.Empty : content;
                    btn.ToolTip = string.IsNullOrEmpty(content) ? string.Empty : tooltip;
                    break;
                    
                case "First":
                    btn.IconData = Geometry.Parse("F1 M 57,27.7083L 57,48.2917L 43.5417,38L 57,27.7083 Z M 39.5833,27.7083L 39.5833,48.2917L 26.125,38L 39.5833,27.7083 Z M 23.75,28.5L 23.75,47.5L 19,47.5L 19,28.5L 23.75,28.5 Z  ");
                    break;
                case "Previous":
                    btn.IconData = Geometry.Parse("F1 M 33.6458,38L 49.4792,53.8333L 38.7917,53.8333L 22.1667,38L 38.7917,22.1667L 49.4792,22.1667L 33.6458,38 Z  ");
                    break;
                case "Next":
                    btn.IconData = Geometry.Parse("F1 M 42.3542,38L 26.5208,53.8333L 37.2083,53.8333L 53.8333,38L 37.2083,22.1667L 26.5208,22.1667L 42.3542,38 Z ");
                    break;
                case "Last":
                    btn.IconData = Geometry.Parse("F1 M 19,27.7083L 32.4583,38L 19,48.2917L 19,27.7083 Z M 36.4167,27.7083L 49.875,38L 36.4167,48.2917L 36.4167,27.7083 Z M 52.25,28.5L 57,28.5L 57,47.5L 52.25,47.5L 52.25,28.5 Z ");
                    break;
            }
        }
    }
}
