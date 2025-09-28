using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardHub.Forms
{
    public partial class DeckBuilderScreen : Form
    {
        public DeckBuilderScreen()
        {
            InitializeComponent();
        }

        private void DeckBuilderScreen_Load(object sender, EventArgs e)
        {
            try
            {
                // Create an Image List that will hold thumbnail of all
                // cards related images.
                ImageList cardImageList = new ImageList();
                cardImageList.ImageSize = new Size(80, 120); // Thumbnail size
                cardImageList.ColorDepth = ColorDepth.Depth32Bit;

                cardListView.View = View.SmallIcon;
                cardListView.LargeImageList = cardImageList;
                cardListView.Scrollable = true;
                cardListView.Dock = DockStyle.Fill;
                cardListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                splitContainer1.Panel1.Controls.Add(cardListView);

                //foreach (var file in Directory.GetFiles(@"C:\Users\Stark\source\repos\CardHub\thumbs", "*.jpg"))
                //{
                //    string baseName = Path.GetFileNameWithoutExtension(file);
                //    string padded = baseName.PadLeft(8, '0');

                //    //Image img = Image.FromFile(file);
                //    //cardImageList.Images.Add(padded, img);
                //    using (Image img = Image.FromFile(file))
                //    {
                //        cardImageList.Images.Add(padded, (Image)img.Clone());
                //    }
                //    ListViewItem item = new ListViewItem(padded);
                //    item.ImageKey = padded;
                //    cardListView.Items.Add(item);
                //}

                Task.Run(() =>
                {
                    foreach (var file in Directory.GetFiles(@"C:\Users\Stark\source\repos\CardHub\thumbs\", "*.jpg"))
                    {
                        string baseName = Path.GetFileNameWithoutExtension(file);
                        string padded = baseName.PadLeft(8, '0');

                        using (Image img = Image.FromFile(file))
                        {
                            Image clone = (Image)img.Clone();
                            this.Invoke(new MethodInvoker(() =>
                            {
                                cardImageList.Images.Add(padded, clone);
                                cardListView.Items.Add(new ListViewItem(padded) { ImageKey = padded });
                                //cardListView.Refresh();
                            }));
                        }
                    }
                });

            }
            catch (Exception exc)
            {
                throw;
            }
        }
    }
}
