using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace SDCode.VerbalStimuli
{
    [Description("Verbal Stimulus.")]
    public class VerbalStimulusCsvModel
    {
        [Name(nameof(Index))]
        [Description("Stimulus Index.")]
        public string Index { get; set; }

        [Name(nameof(Fragment1))]
        [Description("Sentence Fragment 1.")]
        public string Fragment1 { get; set; }

        [Name(nameof(Fragment2))]
        [Description("Sentence Fragment 1.")]
        public string Fragment2 { get; set; }

        [Name(nameof(Fragment3))]
        [Description("Sentence Fragment 1.")]
        public string Fragment3 { get; set; }

        [Name(nameof(Fragment2AdjustmentX))]
        [Description("Sentence Fragment 2's horizontal adjustment.")]
        public int Fragment2AdjustmentX { get; set; }

        [Name(nameof(Fragment3AdjustmentX))]
        [Description("Sentence Fragment 3's horizontal adjustment.")]
        public int Fragment3AdjustmentX { get; set; }
    }

    public class Stimulus {
        public string Index { get; }
        public IReadOnlyCollection<DisplayText> DisplayTexts { get; }
        public Stimulus(string index, IReadOnlyCollection<DisplayText> displayTexts) {
            Index = index;
            DisplayTexts = displayTexts;
        }
    }
    public class DisplayText {
        static readonly string FontFamilyName = "Verdana";
        static readonly int FontEmSize = 12;
        readonly FontStyle _fontStyle;

        public DisplayText(string text, FontStyle fontStyle, int xAxisAdjustment) {
            Text = text;
            _fontStyle = fontStyle;
            XAxisAdjustment = xAxisAdjustment;
        }
        public string Text { get; }
        public int XAxisAdjustment { get; }

        public Font Font => new Font(FontFamilyName, FontEmSize, _fontStyle);
    }

    static class Program
    {
        static readonly string ImagesPath = Path.Join("..","assets","img","Stimuli", "Verbal");
        static string CsvFilePath = "VerbalStimuli.csv";

        static int GetTotalRenderWidth(Brush brush, StringFormat format, IEnumerable<DisplayText> displayTexts) {
            var result = 0;
            Image image = new Bitmap(1000, 1000);
            using var g = Graphics.FromImage(image);
            using var copy = (StringFormat)format.Clone();
            var rect = new Rectangle(0, 0, 1000, 1000);
            foreach (var displayText in displayTexts) {
                if (!string.IsNullOrWhiteSpace(displayText.Text)) {
                    copy.SetMeasurableCharacterRanges(new[] { new CharacterRange(0, displayText.Text.Length) });
                    var regions = g.MeasureCharacterRanges(displayText.Text, displayText.Font, rect, copy);
                    rect.X += displayText.XAxisAdjustment;
                    g.DrawString(displayText.Text, displayText.Font, brush, rect, format);
                    var bounds = regions[0].GetBounds(g);
                    var width = (int)bounds.Width;
                    result = rect.X + width;
                    rect.X += width;
                    rect.Width -= width;
                }
            }
            return result;
        }


        static void DrawString(Graphics g, Brush brush, ref Rectangle rect, StringFormat format, DisplayText displayText) {
            if (!string.IsNullOrWhiteSpace(displayText.Text)) {
                using var copy = (StringFormat)format.Clone();
                copy.SetMeasurableCharacterRanges(new[] {new CharacterRange(0, displayText.Text.Length)});
                var regions = g.MeasureCharacterRanges(displayText.Text, displayText.Font, rect, copy);
                rect.X += displayText.XAxisAdjustment;
                g.DrawString(displayText.Text, displayText.Font, brush, rect, format);
                var bounds = regions[0].GetBounds(g);
                var width = (int)bounds.Width;
                rect.X += width;
                rect.Width -= width;
            }
        }

        static Image GetImage(IReadOnlyCollection<DisplayText> displayTexts) {
            var imgWidth = 800;
            var imgHeight = 700;

            var maxEmSize = (int)displayTexts.Max(displayText=>displayText.Font.Size);
            using var format = (StringFormat)StringFormat.GenericTypographic.Clone();
            var totalRenderWidth = GetTotalRenderWidth(SystemBrushes.WindowText, format, displayTexts);
            Image image = new Bitmap(imgWidth, imgHeight);
            format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            using var graphics = Graphics.FromImage(image);
            var x = imgWidth / 2 - totalRenderWidth / 2;
            var y = imgHeight/2-maxEmSize;
            var rect = new Rectangle(x, y, imgWidth, imgHeight);
            graphics.Clear(Color.White);
            foreach (var displayText in displayTexts) {
                DrawString(graphics, SystemBrushes.WindowText, ref rect, format, displayText);
            }
            return image;
        }

        static IEnumerable<VerbalStimulusCsvModel> ReadCsvFile()
        {
            IEnumerable<VerbalStimulusCsvModel> result;
            if (File.Exists(CsvFilePath)) {
                using var reader = new StreamReader(CsvFilePath, Encoding.Default);
                using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
                result = csv.GetRecords<VerbalStimulusCsvModel>().ToList();
            } else {
                result = new List<VerbalStimulusCsvModel>();
            }
            return result;
        }

        static void Main() {
            if (Directory.Exists(ImagesPath)) {
                foreach (var filepath in Directory.GetFiles(ImagesPath)) {
                    File.Delete(filepath);
                }
            } else {
                Directory.CreateDirectory(ImagesPath);
            }
            var csvFile = ReadCsvFile();
            var stimuli = csvFile.Select(x => new Stimulus(x.Index, new List<DisplayText> {
                new DisplayText($"{x.Fragment1} ", FontStyle.Regular, 0)
                , new DisplayText($"{x.Fragment2} ", FontStyle.Underline, x.Fragment2AdjustmentX)
                , new DisplayText(x.Fragment3, FontStyle.Regular, x.Fragment3AdjustmentX)
            }));
            foreach (var stimulus in stimuli) {
                var image = GetImage(stimulus.DisplayTexts);
                var filename = Path.Join(ImagesPath, $"{stimulus.Index}.png");
                image.Save(filename, ImageFormat.Png);
            }
        }
    }
}