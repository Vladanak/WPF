using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO;

namespace OOP_4_5_TEST
{
    public partial class MainWindow : Window
    {
        private FileStream _fileStream;
        public MainWindow()
        {
            InitializeComponent();
            richtextbox1.AllowDrop = true;
            richtextbox1.AddHandler(DragOverEvent, new DragEventHandler(Drgntr), true);
            richtextbox1.AddHandler(DropEvent, new DragEventHandler(Dropitem), true);
        }
        private static void Drgntr(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
            e.Handled = false;
        }
        private void Dropitem(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var docPath = (string[])e.Data.GetData(DataFormats.FileDrop);
            var dataFormat = DataFormats.Rtf;
            if (docPath == null) return;
            Title = docPath[0];
            if (!File.Exists(docPath[0])) return;
            try
            {
                var range = new TextRange(richtextbox1.Document.ContentStart, richtextbox1.Document.ContentEnd);
                var fStream = new FileStream(docPath[0], FileMode.Open);
                range.Load(fStream, dataFormat);
                fStream.Close();
                LastDocs.Items.Add(docPath[0]);
            }
            catch (Exception)
            {
                MessageBox.Show("File could not be opened. Make sure the file is a text file.");
            }
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog {Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*"};
            if (dlg.ShowDialog() != true) return;
            _fileStream = new FileStream(dlg.FileName, FileMode.Open);
            var range = new TextRange(richtextbox1.Document.ContentStart, richtextbox1.Document.ContentEnd);
            range.Load(_fileStream, DataFormats.Rtf);
            _fileStream.Close();
            LastDocs.Items.Add(dlg.FileName);
            StatusText.Text = "Total count chars: " + (range.Text.Length);
        }

        private void Save_Click_1(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog {Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*"};
            if (dlg.ShowDialog() != true) return;
            _fileStream = new FileStream(dlg.FileName, FileMode.Create);
            var range = new TextRange(richtextbox1.Document.ContentStart, richtextbox1.Document.ContentEnd);
            range.Save(_fileStream, DataFormats.Rtf);
            LastDocs.Items.Add(dlg.FileName);
            _fileStream.Close();
        }
        private void richtextbox1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (richtextbox1.Selection.IsEmpty) return;
            if (richtextbox1.Selection.GetPropertyValue(TextElement.FontWeightProperty) is FontWeight)
                ToggleButtonBold.IsChecked = (((FontWeight)
                                                  richtextbox1.Selection.GetPropertyValue(TextElement.FontWeightProperty))
                                              == FontWeights.Bold);
            if (richtextbox1.Selection.GetPropertyValue(TextElement.FontStyleProperty) is FontStyle)
                ToggleButtonItalic.IsChecked = (((FontStyle)
                                                    richtextbox1.Selection.GetPropertyValue(TextElement.FontStyleProperty))
                                                == FontStyles.Italic);
            if (!(richtextbox1.Selection.GetPropertyValue(TextElement.FontFamilyProperty) is FontFamily)) return;
            for (var i = 0; i < fontNameComBox.Items.Count; i++)
            {
                var res = (FontFamily)richtextbox1.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
                if (res.Source.Equals(((FontFamily)fontNameComBox.Items[i]).Source))
                {
                    fontNameComBox.SelectedIndex = i;
                }
            }
            for (var i = 0; i < fontSizeComBox.Items.Count; i++)
            {
                var res = richtextbox1.Selection.GetPropertyValue(TextElement.FontSizeProperty);
                if(res.Equals(fontSizeComBox.Items[i]))
                {
                    fontSizeComBox.SelectedIndex = i;
                }
              
            }
        }
        private void fontNameComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!richtextbox1.Selection.IsEmpty)
            {
                richtextbox1.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, (FontFamily)fontNameComBox.SelectedItem);
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!richtextbox1.Selection.IsEmpty)
            {
                richtextbox1.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSizeComBox.SelectedItem);
            }
        }
        private void ToggleButtonBold_Checked(object sender, RoutedEventArgs e)
        {
            if (!richtextbox1.Selection.IsEmpty)
            {
                richtextbox1.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void ToggleButtonBold_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!richtextbox1.Selection.IsEmpty)
            {
                richtextbox1.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
        }

        private void ToggleButtonItalic_Checked(object sender, RoutedEventArgs e)
        {
            if (!richtextbox1.Selection.IsEmpty)
            {
                richtextbox1.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
        }

        private void ToggleButtonItalic_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!richtextbox1.Selection.IsEmpty)
            {
                richtextbox1.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
        }

        private void ToggleButtonUnderline_Checked(object sender, RoutedEventArgs e)
        {
            if (!richtextbox1.Selection.IsEmpty)
            {
                richtextbox1.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void ToggleButtonUnderline_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!richtextbox1.Selection.IsEmpty)
            {
                richtextbox1.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, TextDecorations.Baseline);
            }
        }

        private void richtextbox1_KeyUp(object sender, EventArgs e)
        {
                var richText = new TextRange(richtextbox1.Document.ContentStart, richtextbox1.Document.ContentEnd).Text;
                StatusText.Text = "Total count chars: " + (richText.Length-2);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            richtextbox1.KeyUp += richtextbox1_KeyUp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Resources = new ResourceDictionary() { Source = new Uri(@"C:\Users\Silent Hill\Desktop\OOP_4-5_TEST\OOP_4-5_TEST\Dictionary2.xaml") };

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Resources = new ResourceDictionary() { Source = new Uri(@"C:\Users\Silent Hill\Desktop\OOP_4-5_TEST\OOP_4-5_TEST\Dictionary1.xaml") };
        }

        private void UserControl2_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void LastDocs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var and=(ComboBox)sender;
                _fileStream = new FileStream(and.SelectedItem.ToString(), FileMode.Open);
                var range = new TextRange(richtextbox1.Document.ContentStart,
                    richtextbox1.Document.ContentEnd);
                range.Load(_fileStream, DataFormats.Rtf);
                _fileStream.Close();
        }
    }
}